using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using ProtoBuf;
using RuleSet.Elements;
using RuleSet.Menus;
using RuleSet.Versions;

namespace RuleSet
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class RuleSet
    {
        private static readonly BinaryFormatter ruleSetSerializer = new BinaryFormatter()
        {
            AssemblyFormat = FormatterAssemblyStyle.Simple,
            FilterLevel = TypeFilterLevel.Low,
            TypeFormat = FormatterTypeStyle.TypesWhenNeeded
        };

        [ProtoMember(1, AsReference = true, OverwriteList = false)]
        public List<Element> Elements { get; } = new List<Element>();

        [ProtoMember(2)]
        public string Name
        {
            get; set;
        }

        [ProtoMember(3, OverwriteList = false)]
        public List<Need> Needs { get; } = new List<Need>();

        [ProtoMember(4, OverwriteList = false)]
        public List<Research> Research { get; } = new List<Research>();

        [ProtoMember(5)]
        public ResourceBar ResourceBar
        {
            get; set;
        }

        [ProtoMember(6, OverwriteList = false)]
        public List<ResourcePart> StartResources { get; } = new List<ResourcePart>();

        [ProtoMember(7)]
        public Toolbar Toolbar
        {
            get; set;
        }

        public static RuleSet Load(Stream stream)
        {
            return VersionSelector.AutoLoad(stream);
        }

        public static void Save(RuleSet ruleSet, Stream stream)
        {
            VersionSelector.Save(ruleSet, stream);
        }
    }
}
