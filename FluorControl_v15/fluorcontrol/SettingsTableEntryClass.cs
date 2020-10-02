using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluorControl
{
    [Serializable]
    public class SettingsTableEntryClass
    {
        public string Name {get; set;}
        public double Value {get; set;}
        public string Range {get; set;}
        public bool IsPanel {get; set;}
        public string ParentName {get; set; }
        public bool CanChange { get; set; } 

        public SettingsTableEntryClass(string name, double num, string range, string parentName, bool expandable, bool canChange)
        {
            this.Name = name;
            this.Value = num;
            this.Range = range;
            this.ParentName = parentName;
            this.IsPanel = expandable;
            this.CanChange = canChange;
        }

        public SettingsTableEntryClass(SettingsTableEntryClass Old)
        {
            this.Name = Old.Name;
            this.Value = Old.Value;
            this.Range = Old.Range;
            this.ParentName = Old.ParentName;
            this.IsPanel = Old.IsPanel;
            this.CanChange = Old.CanChange;
        }
    }
}
