using System.Collections.Generic;
using System.IO;

namespace RuleSet.Versions
{
    public static class VersionSelector
    {
        private static Dictionary<int, VersionSerializer> LoadedSerializers
        {
            get;
        } = new Dictionary<int, VersionSerializer>();

        public static RuleSet AutoLoad(Stream stream)
        {
            return GetSerializer(stream.ReadByte()).Deserialize(stream);
        }

        public static VersionSerializer GetSerializer(int value)
        {
            VersionSerializer serializer;
            if (!LoadedSerializers.TryGetValue(value, out serializer))
            {
                switch (value)
                {
                    case 1:
                        serializer = LoadedSerializers[value] = new V1Serializer();
                        break;
                }
            }
            return serializer;
        }

        public static void Save(RuleSet ruleSet, Stream stream)
        {
            GetSerializer(1).Serialize(ruleSet, stream);
        }
    }
}
