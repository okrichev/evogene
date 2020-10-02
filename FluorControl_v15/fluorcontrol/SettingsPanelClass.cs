using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluorControl
{
    [Serializable]
    public class SettingsPanelClass // describes a set of individual settings entries: replaces a panel
    {
        public string Name = "";
        public string Value = "";
        public string Range = "";
        public bool IsPanel = true;
        public bool CanChange = false;

        public List<SettingsTableEntryClass> Properties;

        public SettingsPanelClass(string name, List<SettingsTableEntryClass> properties)
        {
            this.Name = name;
            this.Properties = properties;
            this.Value = "";
            this.Range = "";
            this.IsPanel = true; 
            this.CanChange = false;
        }

        public SettingsPanelClass(SettingsPanelClass OldPanel)
        {
            this.Name = OldPanel.Name;
            List<SettingsTableEntryClass> list = new List<SettingsTableEntryClass>();
            foreach (var x in OldPanel.Properties)
                list.Add(new SettingsTableEntryClass(x));
            this.Properties = list;
            this.Value = OldPanel.Value;
            this.Range = OldPanel.Range;
            this.IsPanel = OldPanel.IsPanel;
            this.CanChange = OldPanel.CanChange;
        }

        public override string ToString()
        {
            string str;
            str = "Data Version 2" + "\r\n" +
                "Panel Name: " + this.Name + "\r\n";
            foreach (var x in this.Properties)
            {
                str += x.Name + ": " + x.Value.ToString() + "\r\n";
            }
            return str;
        }
    }
}
