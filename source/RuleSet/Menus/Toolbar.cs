﻿using System.Collections.Generic;

namespace RuleSet.Menus
{
    public class Toolbar : SerializedObject
    {
        public List<ToolbarItem> Items { get; } = new List<ToolbarItem>();

        public string Name { get; set; }
    }
}