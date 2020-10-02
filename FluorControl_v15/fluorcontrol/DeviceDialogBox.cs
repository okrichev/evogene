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
    public partial class DeviceDialogBox : Form
    {
        public ImplementationClass Implementation { get; set; }

        public DeviceDialogBox()
        {
            InitializeComponent();
        }

  /*      public void getImplementation(ImplementationClass Implementation)
        {
            this.implementation = Implementation;
        } */


        private void DeviceDialogBox_Load(object sender, EventArgs e)
        {

        }

        public void AddItemsToSerialPortComboBox(string SerialPortName)
        {
            cboSerialPort.Items.Add(SerialPortName);
        }

        public void ClearSerialPortComboBox()
        {
            cboSerialPort.Items.Clear();
        }

        public void SetSerialPortComboBoxText(string SerialPortName)
        {
            cboSerialPort.Text = SerialPortName;
        }

        public void SetArduinoName(string ArduinoName)
        {
            rtxbArduinoName.Text = ArduinoName;
        }

        public delegate void DispTxtDelegate(string txt);

        public void DisplayText(string txt)
        {

            if (rtxb_SerialTerminal.InvokeRequired)
            {
                DispTxtDelegate DispTxt = new DispTxtDelegate(DisplayText);
                this.Invoke(DispTxt,
                  new object[] { txt });
            }
            else
            {
                rtxb_SerialTerminal.AppendText(txt);
            }
        }

        private void btnCheckArduino_Click(object sender, EventArgs e)
        {
            Implementation.InitSerial();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void rtxb_SendCommand_TextChanged(object sender, EventArgs e)
        {
            int lastLineNo;             
            string CommandToSend;
            if (rtxb_SendCommand.Text.Last().Equals('\n'))
            {
                lastLineNo = rtxb_SendCommand.Lines.Length - 2;
                CommandToSend = rtxb_SendCommand.Lines[lastLineNo];
                Implementation.SendCommandArduino(CommandToSend);
            }
        } //rtxb_SendCommand_TextChanged

        public void setCameraListBox(string[] CameraDescription)
        {
            lstbx_Camera.Items.AddRange(CameraDescription);
        }

        private void btnCheckCamera_Click(object sender, EventArgs e)
        {
            Implementation.InitCamera();
        }
    }
}
