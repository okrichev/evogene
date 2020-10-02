using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; 
using System.IO.Ports;

namespace FluorControl
{
    public struct MaskParamStruc
    {
        public Int32 Top;
        public Int32 Bottom;
        public Int32 Left;
        public Int32 Right;
        public Int32 NoRows;
        public Int32 NoColumns;
        public int[] rows;
        public int[] columns;
    }


    public partial class FluorControl : Form
    {
        private SettingsDialogBox SettingsDlog;
        private DeviceDialogBox DeviceDlog;
        private ImplementationClass Implementation;
        private SampleIDForm SampleIDdlog;
        private ControlPanelClass ControlPanel;
        private AnalyzeForm AnalyzeDlog;

        public FluorControl()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            Text = Application.ProductName + " v" + Application.ProductVersion.Split('.')[0] + "." + Application.ProductVersion.Split('.')[1];

            SettingsDlog = new SettingsDialogBox();
            SettingsDlog.Show();
            SettingsDlog.Hide();
            DeviceDlog = new DeviceDialogBox();
            DeviceDlog.Show();
            DeviceDlog.Hide();
            SampleIDdlog = new SampleIDForm();
            ControlPanel = new ControlPanelClass();
            AnalyzeDlog = new AnalyzeForm();
            Implementation = new ImplementationClass(this, SettingsDlog, DeviceDlog, SampleIDdlog, ControlPanel, AnalyzeDlog);
            DeviceDlog.Implementation = Implementation;
            SettingsDlog.Implementation = Implementation;
            SampleIDdlog.Implementation = Implementation;
            ControlPanel.Implementation = Implementation;
            AnalyzeDlog.Implementation = Implementation;
            Implementation.Init_Everything();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsDlog.KeepSettingsForCancel();
            SettingsDlog.ShowDialog();
            //SettingsDlog.BringToFront();
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Implementation.SaveSettings();
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Implementation.LoadDefaultSettings();
        }

        private void devicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeviceDlog.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Implementation.QuitProc();
        }

        public IntPtr GetPicBoxImageHandle()
        {
            return picbxImage.Handle;
        }

        public delegate void OutputToLogBookDelegate(string txt);

        public void OutputToLogBook(string txt)
        {

            if (rtxb_LogBook.InvokeRequired)
            {
                OutputToLogBookDelegate DispTxt = new OutputToLogBookDelegate(OutputToLogBook);
                this.Invoke(DispTxt,
                  new object[] { txt });
            }
            else
            {
                rtxb_LogBook.Focus();
                rtxb_LogBook.AppendText(txt + "\r\n");
            }
        }

        private void saveSettingsAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Implementation.SaveSettingsAs();
        }

        private void loadSettingsFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Implementation.OpenSettings();
        }

        private void btn_SaveFolder_Click(object sender, EventArgs e)
        {
            Implementation.GetSaveFolder();
        }

        public delegate void setFolderPathStringDelegate(string txt);

        public void setFolderPathString(string txt)
        {

            if (txtBox_FolderPath.InvokeRequired)
            {
                setFolderPathStringDelegate DispTxt = new setFolderPathStringDelegate(setFolderPathString);
                this.Invoke(DispTxt,
                  new object[] { txt });
            }
            else
            {
                txtBox_FolderPath.Text = txt;
            }
        }

        private void btn_DoFvFm_Click(object sender, EventArgs e)
        {
            if (Implementation.arduinoDetected)
                if (Implementation.cameraDetected)
                    SampleIDdlog.ShowDialog();
                else
                    MessageBox.Show("No camera detected!");
            else
                MessageBox.Show("No Arduino controller detected!");
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxb_LogBook.Clear();
        }

        private void saveLogAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Implementation.SaveLog(rtxb_LogBook);
        }

        public string GetFolderPath()
        {
            return txtBox_FolderPath.Text; 
        }

        public void EnableDisableFvFmButton(bool enabled)
        {
            btn_DoFvFm.Enabled = enabled;
        }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Implementation.QuitProc();
            Application.Exit();
        }

        private void controlPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
             ControlPanel.Show();   
        }

        private void btn_Analyze_Click(object sender, EventArgs e)
        {
            AnalyzeDlog.ShowDialog();
        }
    }
}
