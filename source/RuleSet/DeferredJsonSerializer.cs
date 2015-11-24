using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace RuleSet
{
    public class DeferredJsonSerializer
    {
        private object sourceObject;

        public Dictionary<object, object> Graph { get; } = new Dictionary<object, object>();

        public Dictionary<Guid, object> GuidToObjectMap { get; } = new Dictionary<Guid, object>();

        public Dictionary<object, Guid> ObjectToGuidMap { get; } = new Dictionary<object, Guid>();

        public Dictionary<Guid, List<object>> ReferencedGuids { get; } = new Dictionary<Guid, List<object>>();

        public HashSet<Guid> UsedGuids { get; } = new HashSet<Guid>();

        public object Deserialize(JToken token)
        {
            DeserializeFirstPass(token);
            DeserializeSecondPass();
            Clear();
            return sourceObject;
        }

        public JToken Serialize(object obj, object parent)
        {
            if (obj == null)
                return null;
            JToken token = SerializeObjectType(obj.GetType());
            PrepareObject(token, obj);

            SerializeObject(token, obj, parent);

            return token;
        }

        private void Clear()
        {
            Graph.Clear();
            GuidToObjectMap.Clear();
            ObjectToGuidMap.Clear();
            ReferencedGuids.Clear();
            UsedGuids.Clear();
        }

        private void DeserializeFirstPass(JToken token)
        {
            sourceObject = DeserializeFirstPassToken(token, null);
        }

        private object DeserializeFirstPassArray(JArray jsonArray, object parent)
        {
            bool foundValid = false;
            ArrayList arrayList = new ArrayList();
            foreach (var item in jsonArray)
            {
                object deserializedItem = DeserializeFirstPassToken(item, parent);
                if (deserializedItem != null)
                {
                    foundValid = true;
                    arrayList.Add(deserializedItem);
                }
            }
            return foundValid ? arrayList : null;
        }

        private object DeserializeFirstPassObject(JObject jsonObject, object parent)
        {
            if (jsonObject["$type"] != null)
            {
                object createdObject = Activator.CreateInstance(Type.GetType(jsonObject["$type"].Value<string>()));
                Graph[jsonObject] = createdObject;
                if (jsonObject["$id"] != null)
                {
                    Guid objectGuid = jsonObject["$id"].ToObject<Guid>();
                    ObjectToGuidMap[createdObject] = objectGuid;
                    GuidToObjectMap[objectGuid] = createdObject;
                }
                foreach (var token in jsonObject)
                {
                    DeserializeFirstPassProperty(token.Key, token.Value, createdObject);
                }
                return createdObject;
            }
            return null;
        }

        private void DeserializeFirstPassProperty(string name, JToken token, object parent)
        {
            PropertyInfo property = parent.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            object deserializedToken = DeserializeFirstPassToken(token, parent);
            if (property != null && deserializedToken != null)
            {
                if (token is JArray)
                {
                    ArrayList deserializedArray = (ArrayList)deserializedToken;
                    if (property.PropertyType.IsArray && property.CanWrite)
                    {
                        var setMethod = property.PropertyType.GetMethod("Set");
                        var array = Activator.CreateInstance(property.PropertyType, new object[] { deserializedArray.Count });
                        for (int i = 0; i < deserializedArray.Count; i++)
                        {
                            setMethod.Invoke(array, new object[] { i, deserializedArray[i] });
                        }
                        property.SetValue(parent, array, null);
                    }
                    else if (typeof(IList).IsAssignableFrom(property.PropertyType))
                    {
                        IList list = (IList)property.GetValue(parent, null);
                        if (list.IsReadOnly) return;

                        for (int i = 0; i < deserializedArray.Count; i++)
                        {
                            list.Add(deserializedArray[i]);
                        }
                    }
                }
                else if (token is JObject)
                {
                    if (property.PropertyType.IsAssignableFrom(deserializedToken.GetType()) && property.CanWrite)
                    {
                        property.SetValue(parent, deserializedToken, null);
                    }
                }
            }
        }

        private object DeserializeFirstPassToken(JToken token, object parent)
        {
            if (token is JArray)
            {
                return DeserializeFirstPassArray((JArray)token, parent);
            }
            else if (token is JObject)
            {
                return DeserializeFirstPassObject((JObject)token, parent);
            }
            return null;
        }

        private void DeserializeSecondPass()
        {
            foreach (var item in Graph)
            {
                DeserializeSecondPassToken((JToken)item.Key, item.Value);
            }
        }

        private object DeserializeSecondPassArray(JArray jsonArray, object parent)
        {
            bool foundValid = false;
            ArrayList arrayList = new ArrayList();
            foreach (var item in jsonArray)
            {
                object deserializedItem = DeserializeSecondPassToken(item, parent);
                if (deserializedItem != null)
                {
                    foundValid = true;
                    arrayList.Add(deserializedItem);
                }
            }
            return foundValid ? arrayList : null;
        }

        private object DeserializeSecondPassObject(JObject token, object value)
        {
            if (token["$ref"] != null)
                return GuidToObjectMap[token["$ref"].ToObject<Guid>()];

            foreach (var item in token)
            {
                if (ShouldDeserializeSecondPass(item.Value))
                {
                    DeserializeSecondPassProperty(item.Key, item.Value, value);
                }
            }
            return value;
        }

        private void DeserializeSecondPassProperty(string name, JToken token, object parent)
        {
            PropertyInfo property = parent.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            object deserializedToken = DeserializeSecondPassToken(token, parent);
            if (property != null && deserializedToken != null)
            {
                if (deserializedToken is ArrayList)
                {
                    ArrayList deserializedArray = (ArrayList)deserializedToken;
                    if (property.PropertyType.IsArray && property.CanWrite)
                    {
                        var setMethod = property.PropertyType.GetMethod("Set");
                        var array = Activator.CreateInstance(property.PropertyType, new object[] { deserializedArray.Count });
                        for (int i = 0; i < deserializedArray.Count; i++)
                        {
                            setMethod.Invoke(array, new object[] { i, deserializedArray[i] });
                        }
                        property.SetValue(parent, array, null);
                    }
                    else if (typeof(IList).IsAssignableFrom(property.PropertyType))
                    {
                        IList list = (IList)property.GetValue(parent, null);
                        if (list.IsReadOnly) return;

                        for (int i = 0; i < deserializedArray.Count; i++)
                        {
                            list.Add(deserializedArray[i]);
                        }
                    }
                }
                else if (!property.PropertyType.IsArray && property.CanWrite)
                {
                    if (deserializedToken is JValue)
                    {
                        property.SetValue(parent, ((JValue)deserializedToken).ToObject(property.PropertyType), null);
                    }
                    else
                    {
                        property.SetValue(parent, deserializedToken, null);
                    }
                }
            }
        }

        private object DeserializeSecondPassToken(JToken token, object value)
        {
            if (token is JArray)
            {
                return DeserializeSecondPassArray((JArray)token, value);
            }
            else if (token is JValue)
            {
                return token;
            }
            else if (token is JObject)
            {
                return DeserializeSecondPassObject((JObject)token, value);
            }
            return null;
        }

        private Guid GetGuid()
        {
            Guid guid;
            while (UsedGuids.Contains(guid = Guid.NewGuid())) ;
            UsedGuids.Add(guid);
            return guid;
        }

        private void PrepareObject(JToken token, object obj)
        {
            if (token is JObject)
            {
                if (!Graph.ContainsKey(obj))
                {
                    Graph[obj] = token;
                    Type objType = obj.GetType();
                    Guid objectGuid = GetGuid();
                    ((JObject)token)["$id"] = new JValue(objectGuid);
                    ((JObject)token)["$type"] = new JValue($"{objType.FullName}, {objType.Assembly.GetName().Name}");

                    ObjectToGuidMap[obj] = objectGuid;
                    GuidToObjectMap[objectGuid] = obj;
                    ReferencedGuids[objectGuid] = new List<object>();
                }
            }
        }

        private void SerializeArray(JArray array, IEnumerable ienumerable, object parent)
        {
            Dictionary<object, JToken> serializedObjects = new Dictionary<object, JToken>();
            foreach (var item in ienumerable)
            {
                JToken token = SerializeObjectType(item.GetType());
                if (token != null)
                {
                    array.Add(token);
                    serializedObjects[item] = token;
                    PrepareObject(token, item);
                }
            }
            foreach (var item in serializedObjects)
            {
                SerializeObject(item.Value, item.Key, parent);
            }
        }

        private void SerializeObject(JToken token, object obj, object parent)
        {
            if (token is JArray)
            {
                SerializeArray((JArray)token, (IEnumerable)obj, parent);
            }
            else if (token is JValue)
            {
                ((JValue)token).Value = obj;
            }
            else if (token is JObject)
            {
                if (token["$id"] == null)
                {
                    ReferencedGuids[ObjectToGuidMap[obj]].Add(parent);
                    ((JObject)token)["$ref"] = ObjectToGuidMap[obj];
                }
                else
                {
                    foreach (var item in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        SerializeProperty(item.Name, (JObject)token, item.PropertyType, item.GetValue(obj, null), obj);
                    }
                }
            }
        }

        private JToken SerializeObjectType(Type type)
        {
            if (type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type == typeof(TimeSpan)) return JValue.CreateUndefined();
            else if (type.IsArray && SerializeObjectType(type.GetElementType()) != null) return new JArray();
            else if (typeof(IEnumerable).IsAssignableFrom(type) && SerializeObjectType(type.GetGenericArguments()[0]) != null) return new JArray();
            else if (type.IsClass) return new JObject();
            else return null;
        }

        private void SerializeProperty(string name, JObject parentToken, Type propertyType, object obj, object parent)
        {
            JToken token = Serialize(obj, parent);
            if (token != null)
            {
                parentToken[name] = token;
            }
        }

        private bool ShouldDeserializeSecondPass(JToken token)
        {
            return
                token is JValue ||
                (token is JObject && token["$ref"] != null) ||
                (token is JArray && token.All(jToken => ShouldDeserializeSecondPass(jToken)));
        }
    }
}
