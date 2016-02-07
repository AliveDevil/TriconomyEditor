using System;
using System.Collections.Generic;

namespace RuleSet.Elements
{
    [Serializable]
    public class Building : Element
    {
        public List<Upgrade> Upgrades { get; } = new List<Upgrade>();

        public int Variants { get; set; } = 0;
    }
}
