using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RuleSet.Menus;

namespace RuleSet
{
    public class RuleSet : SerializedObject
    {
        private static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            Culture = CultureInfo.InvariantCulture,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateParseHandling = DateParseHandling.DateTimeOffset,
            DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            FloatFormatHandling = FloatFormatHandling.Symbol,
            FloatParseHandling = FloatParseHandling.Double,
            Formatting = Formatting.None,
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            ObjectCreationHandling = ObjectCreationHandling.Auto,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
            TypeNameHandling = TypeNameHandling.Auto
        };

        public List<Element> Elements { get; } = new List<Element>();

        public string Name { get; set; }

        public Toolbar Toolbar { get; set; }

        public static RuleSet Load(TextReader textReader)
        {
            DeferredJsonSerializer serializer = new DeferredJsonSerializer();
            using (var jsonReader = new JsonTextReader(textReader))
                return (RuleSet)serializer.Deserialize(JToken.Load(jsonReader));
        }

        public static void Save(RuleSet ruleSet, TextWriter textWriter)
        {
            DeferredJsonSerializer serializer = new DeferredJsonSerializer();
            using (var jsonWriter = new JsonTextWriter(textWriter) { Formatting = Formatting.Indented })
                serializer.Serialize(ruleSet, null).WriteTo(jsonWriter);
        }
    }
}
