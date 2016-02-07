using System.IO;

namespace RuleSetPrompt
{
    public class Program
    {
        private static void Main(string[] args)
        {
            File.WriteAllText("RuleSet.proto", ProtoBuf.Serializer.GetProto<RuleSet.RuleSet>());
        }
    }
}
