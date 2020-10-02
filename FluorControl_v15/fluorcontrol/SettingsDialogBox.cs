using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FluorControl
{
    public partial class SettingsDialogBox : Form
    {
        public ImplementationClass Implementation { get; set; }
        private List<SettingsPanelClass> panelList;
        private List<SettingsPanelClass> savedSettings;

        public List<SettingsPanelClass> PanelList
        {
            get { return this.panelList ?? (this.panelList = ImplementationClass.InitializePanelList()); }
            set { this.panelList = value; }
        }
        

        public SettingsDialogBox()
        {
            InitializeComponent();
        }

        private void SettingsDialogBox_Load(object sender, EventArgs e)
        {
            PropertiesListView.CanExpandGetter = delegate(Object x)
            {
                return (x is SettingsPanelClass);  // ((SettingsTableEntryClass)x).IsPanel;
            };
            PropertiesListView.ChildrenGetter = delegate(Object x)
            {
                return ((SettingsPanelClass)x).Properties;
            };
            PropertiesListView.SetObjects(PanelList);

            ValueColumn.AspectPutter = delegate(Object x, object newValue)
            {
                if (x is SettingsTableEntryClass)
                {
                    SettingsTableEntryClass y = ((SettingsTableEntryClass)x);
                    if (y.CanChange)
                    { 
                        y.Value = (double)newValue;
                        Implementation.TreatSettingsChange(y);

                       // ((SettingsTableEntryClass)x).Range = newValue.ToString();
                   //     var item = PanelList.FirstOrDefault(o => o.Name == ((SettingsTableEntryClass)x).ParentName);
                   //     item.Range = newValue.ToString();
                    }
                }
                else
                {
                    ((SettingsPanelClass)x).Value = "";
                    ((SettingsPanelClass)x).Range = "";
                };
            };

        } // SettingsDialogBox_Load

        private SettingsTableEntryClass GetSettingsItem(string PanelName, string ItemName)
        {
            var PanelItem = PanelList.FirstOrDefault(o => o.Name == PanelName);
            return PanelItem.Properties.FirstOrDefault(item => item.Name == ItemName);       
        } //GetSettingsItem

        public double GetSettingsValue(string PanelName, string ItemName)
        {
            SettingsTableEntryClass item = GetSettingsItem(PanelName, ItemName);
            return ((SettingsTableEntryClass)item).Value;
        }

        public string GetSettingsRange(string PanelName, string ItemName)
        {
            SettingsTableEntryClass item = GetSettingsItem(PanelName, ItemName);
            return ((SettingsTableEntryClass)item).Range;
        }

        public bool GetSettingsCanChange(string PanelName, string ItemName)
        {
            SettingsTableEntryClass item = GetSettingsItem(PanelName, ItemName);
            return ((SettingsTableEntryClass)item).CanChange;
        }

        public bool SetSettingsValue(string PanelName, string ItemName, double value)
        {
            SettingsTableEntryClass item = GetSettingsItem(PanelName, ItemName);
            if (item != null)
            {
                ((SettingsTableEntryClass)item).Value = value;
                Implementation.TreatSettingsChange(item);
            }
            return (item == null);
        }

        public bool SetSettingsRange(string PanelName, string ItemName, string range)
        {
            SettingsTableEntryClass item = GetSettingsItem(PanelName, ItemName);
            if (item != null)
                ((SettingsTableEntryClass)item).Range = range;
            return (item == null);
        }

        public void KeepSettingsForCancel()
        {
           // savedSettings = new List<SettingsPanelClass>(panelList);
            savedSettings = new List<SettingsPanelClass>();
            foreach (var x in panelList)
                savedSettings.Add(new SettingsPanelClass(x));
           //  savedSettings = new List<SettingsPanelClass>(panelList.Select(x => x.Clone()));
        }


        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }

        private void btn_SaveSettings_Click(object sender, EventArgs e)
        {
            Implementation.SaveSettings();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            PanelList = savedSettings; 
   //         foreach (var x in panelList)
   //             savedSettings.Add(new SettingsPanelClass(x));

            PropertiesListView.SetObjects(PanelList); // not sure legitimate but did not update the view otherwise
            this.Hide();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            Implementation.SaveSettingsAs();
        }

        private void btn_LoadSettings_Click(object sender, EventArgs e)
        {
            Implementation.LoadDefaultSettings();
            PropertiesListView.SetObjects(PanelList); // not sure legitimate but did not update the view otherwise
     //       foreach (var panel in PanelList)
      //          PropertiesListView.RefreshObject(panel);
        }

        private void btnLoadFrom_Click(object sender, EventArgs e)
        {
            Implementation.OpenSettings();
            PropertiesListView.SetObjects(PanelList);
     //       foreach (var panel in PanelList)
      //          PropertiesListView.RefreshObject(panel);
        }
    }
}
