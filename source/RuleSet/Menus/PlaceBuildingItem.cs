﻿using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Menus
{
    public class PlaceBuildingItem : ToolbarItem
    {
        [KeepReference]
        public Building Building { get; set; }
    }
}
