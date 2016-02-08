using ProtoBuf.Meta;

namespace RuleSet.Versions
{
    public class V3Serializer : VersionSerializer
    {
        public override int Version
        {
            get
            {
                return 3;
            }
        }

        protected override TypeModel Factory()
        {
            return new RuleSetV3();
        }
    }
}
