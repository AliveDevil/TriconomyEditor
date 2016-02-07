using System;
using System.IO;
using ProtoBuf.Meta;

namespace RuleSet.Versions
{
    public abstract class VersionSerializer
    {
        private RuntimeTypeModel model;

        public RuntimeTypeModel Model
        {
            get
            {
                if (model == null)
                {
                    model = TypeModel.Create();
                    OnModelCreation(model);
                }
                return model;
            }
        }

        public abstract int Version
        {
            get;
        }

        public RuleSet Deserialize(Stream stream)
        {
            return Model.Deserialize(stream, null, typeof(RuleSet)) as RuleSet;
        }

        public void Serialize(RuleSet ruleSet, Stream stream)
        {
            stream.WriteByte((byte)Version);
            Model.Serialize(stream, ruleSet);
        }

        protected void AddField(MetaType meta, int index, string name)
        {
            meta.AddField(index, name);
        }

        protected void AddList(MetaType meta, int index, string name)
        {
            meta.AddField(index, name).With(f =>
            {
                f.AsReference = true;
            });
        }

        protected void AddReferenceField(MetaType meta, int index, string name)
        {
            meta.AddField(index, name).With(f =>
            {
                f.AsReference = true;
            });
        }

        protected void AddReferenceList(MetaType meta, int index, string name)
        {
            meta.AddField(index, name).With(f =>
            {
                f.AsReference = true;
                f.OverwriteList = false;
            });
        }

        protected void AddSubType(MetaType meta, Type type)
        {
            meta.AddSubType(type.Name.GetHashCode() & 0x3FFF, type);
        }

        protected MetaType AddType<T>(RuntimeTypeModel model)
        {
            return model.Add(typeof(T), false);
        }

        protected abstract void OnModelCreation(RuntimeTypeModel model);
    }
}
