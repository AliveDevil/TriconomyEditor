using ProtoBuf.Meta;

namespace RuleSet.Versions
{
    public class V4Serializer : VersionSerializer
    {
        public override int Version
        {
            get
            {
                return 4;
            }
        }

        protected override TypeModel Factory()
        {
            return new RuleSetV4();
        }
    }
}
