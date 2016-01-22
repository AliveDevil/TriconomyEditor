using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using RuleSet.Elements;
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

        public List<Need> Needs { get; } = new List<Need>();

        public ResourceBar ResourceBar { get; set; }

        public List<ResourcePart> StartResources { get; } = new List<ResourcePart>();

        public Toolbar Toolbar { get; set; }

        public static RuleSet Load(Stream stream)
        {
            de.alivedevil.DeferredJsonSerializer ser = new de.alivedevil.DeferredJsonSerializer();
            return ser.Deserialize<RuleSet>(stream);
        }

        public static void Save(RuleSet ruleSet, Stream stream)
        {
            de.alivedevil.DeferredJsonSerializer ser = new de.alivedevil.DeferredJsonSerializer();
            ser.Serialize(ruleSet, stream);
        }
    }
}
