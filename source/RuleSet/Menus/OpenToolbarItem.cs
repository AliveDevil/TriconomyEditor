using System;
using ProtoBuf;

namespace RuleSet.Menus
{
    [Serializable]
    public class OpenToolbarItem : ToolbarItem
    {
        public Toolbar Toolbar
        {
            get; set;
        }
    }
}
