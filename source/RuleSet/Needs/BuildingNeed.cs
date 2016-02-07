using System;
using de.alivedevil.Attributes;
using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Needs
{
    [Serializable]
    public class BuildingNeed : Need
    {
        [KeepReference]
        public Building Building
        {
            get; set;
        }

        public int Radius
        {
            get; set;
        }
    }
}
