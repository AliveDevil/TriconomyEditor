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
                        serializer = new V1Serializer();
                        break;
                    case 2:
                        serializer = new V2Serializer();
                        break;
                }
                LoadedSerializers[value] = serializer;
            }
            return serializer;
        }

        public static void Save(RuleSet ruleSet, Stream stream)
        {
            GetSerializer(2).Serialize(ruleSet, stream);
        }
    }
}
