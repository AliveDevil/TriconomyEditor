using System;
using System.Collections.Generic;
using ProtoBuf;

namespace RuleSet.Menus
{
    [Serializable]
    public class Toolbar
    {
        public List<ToolbarItem> Items { get; } = new List<ToolbarItem>();

        public string Name
        {
            get; set;
        }
    }
}
