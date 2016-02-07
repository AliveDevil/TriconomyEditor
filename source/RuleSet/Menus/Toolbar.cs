using System;
using System.Collections.Generic;

namespace RuleSet.Menus
{
    [Serializable]
    public class Toolbar : SerializedObject
    {
        public List<ToolbarItem> Items { get; } = new List<ToolbarItem>();

        public string Name
        {
            get; set;
        }
    }
}
