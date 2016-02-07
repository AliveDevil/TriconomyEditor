using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using RuleSet.Elements;
using RuleSet.Menus;
using RuleSet.Versions;

namespace RuleSet
{
    public class RuleSet
    {
        private static readonly BinaryFormatter ruleSetSerializer = new BinaryFormatter()
        {
            AssemblyFormat = FormatterAssemblyStyle.Simple,
            FilterLevel = TypeFilterLevel.Low,
            TypeFormat = FormatterTypeStyle.TypesWhenNeeded
        };

        public List<Element> Elements { get; } = new List<Element>();

        public string Name
        {
            get; set;
        }

        public List<Need> Needs { get; } = new List<Need>();

        public List<Research> Research { get; } = new List<Research>();

        public ResourceBar ResourceBar
        {
            get; set;
        }

        public List<ResourcePart> StartResources { get; } = new List<ResourcePart>();

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
