using System;
using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Menus
{
    [Serializable]
    public class PlaceBuildingItem : ToolbarItem
    {
        [KeepReference]
        public Building Building
        {
            get; set;
        }
    }
}
