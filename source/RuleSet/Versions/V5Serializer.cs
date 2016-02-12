using ProtoBuf.Meta;

namespace RuleSet.Versions
{
    public class V5Serializer : VersionSerializer
    {
        public override int Version
        {
            get
            {
                return 5;
            }
        }

        protected override TypeModel Factory()
        {
            return new RuleSetV5();
        }
    }
}
