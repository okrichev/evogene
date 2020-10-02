using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace FluorControl_v5
{
    class ImplementationClass
    {
        private SerialPort serialPort = new SerialPort();
        private string rxString;
        private SettingsDialogBox settingsDlog;
        private DeviceDialogBox deviceDlog;
        private int defaultDarkLEDpower = 15;
        private int defaultSatLEDpower = 100;
        private int defaultLEDfreq = 32000;
        private double defaultExposureTimeDark = 1999.76658892741; //in ms
        private double defaultExposureTimeSat = 84.9994343943692; //in ms
        private double defaultFrameRateDark = 0.50; //fps
        private double defaultFrameRateSat = 5; //fps
        private int defaultNofFramesDark = 2;
        private int defaultNofFramesSat = 5;
        private int defaultPixelClock = 7;
        private string defaultFileModifierDark = "_dark_";
        private string defaultFileModifierSat = "_sat_";
        private string defaultSettingsPath = Path.Combine(Application.StartupPath, "FluorControlSettings.bin");
        private int Version = 1;

        public ImplementationClass()
        {
        }

        public ImplementationClass(SettingsDialogBox SettingsDlog, DeviceDialogBox DeviceDlog)
        {
            settingsDlog = SettingsDlog;
            deviceDlog = DeviceDlog;
        }

        public static List<SettingsPanelClass> InitializePanelList()
        {
            List<SettingsPanelClass> list = new List<SettingsPanelClass>();

            list.Add(new SettingsPanelClass("General", InitializeGeneralSettingsPanel()));
            list.Add(new SettingsPanelClass("Dark Acquisition", InitializeAcquisitionPanelTableEntryList("Dark Acquisition")));
            list.Add(new SettingsPanelClass("Saturating Acquisition", InitializeAcquisitionPanelTableEntryList("Saturating Acquisition")));

            //           list.Add(new SettingsPanelClass("Dark",  InitializeSettingsTableEntryList("Dark")));
 //           list.Add(new SettingsPanelClass("Saturating", InitializeSettingsTableEntryList("Saturating")));
            return list;
        }

        private static List<SettingsTableEntryClass> InitializeGeneralSettingsPanel()
        {
            List<SettingsTableEntryClass> list = new List<SettingsTableEntryClass>();
            list.Add(new SettingsTableEntryClass("Version", 1, "[1 1]", "General", false, false));
            list.Add(new SettingsTableEntryClass("Default Settings Path", 0, "", "General", false, true));
            return list;
        }

    /*    private static List<SettingsTableEntryClass> InitializeSettingsTableEntryList(string parentName)
        {
            List<SettingsTableEntryClass> list = new List<SettingsTableEntryClass>();
            list.Add(new SettingsTableEntryClass("Frame Rate", 15, "[0 100]", parentName, false, true));
            list.Add(new SettingsTableEntryClass("Exposure Time", 0.2, "", parentName, false, true));
            return list;
        }
     */

        private static List<SettingsTableEntryClass> InitializeAcquisitionPanelTableEntryList(string PanelName)
        {
            List<SettingsTableEntryClass> list = new List<SettingsTableEntryClass>();
            list.Add(new SettingsTableEntryClass("LED freq (Hz)", 32000 , "[1 2000000]", PanelName, false, false));
            list.Add(new SettingsTableEntryClass("LED power", 15, "[0 255]", PanelName, false, false));
            list.Add(new SettingsTableEntryClass("Pixel Clock", 7, "[7 15]", PanelName, false, false));
            list.Add(new SettingsTableEntryClass("LED power", 15, "[0 255]", PanelName, false, false));
            list.Add(new SettingsTableEntryClass("Exposure Time (ms)", 2, "", PanelName, false, true));
            list.Add(new SettingsTableEntryClass("Frame Rate (fps)", 10, "", PanelName, false, true));
            list.Add(new SettingsTableEntryClass("No of Frames", 2, "", PanelName, false, true));
            list.Add(new SettingsTableEntryClass("File Extension", 0, "_sat_", PanelName, false, true));
            return list;
        }

        public void SaveSettings()
        {
            string fpath = Path.Combine(Application.StartupPath, "FluorControlSettings.bin");
            BinarySerialization.WriteToBinaryFile<List<SettingsPanelClass>>(fpath, settingsDlog.PanelList);
        }

        public void LoadSettings()
        {
            string fpath = Path.Combine(Application.StartupPath, "FluorControlSettings.bin");
            List<SettingsPanelClass> panelList = BinarySerialization.ReadFromBinaryFile<List<SettingsPanelClass>>(fpath);
            settingsDlog.PanelList = panelList;
        } //LoadSettings

        private void InitSerial()
        {
            //get available serial port names
            string[] ArrayComPortsNames = null;
            int index = -1;
            string ComPortName = null;

            ArrayComPortsNames = SerialPort.GetPortNames();
            if (ArrayComPortsNames.GetLength(0) == 0)
            {
                MessageBox.Show("No command ports found!");
                return;
            }

            do
            {
                index += 1;
                deviceDlog.AddItemsToSerialPortComboBox(ArrayComPortsNames[index]);
            }
            while (!((ArrayComPortsNames[index] == ComPortName)
                          || (index == ArrayComPortsNames.GetUpperBound(0))));

            Array.Sort(ArrayComPortsNames);

            // poll ports for Arduino
            index = -1;
            ComPortName = null;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
            do
            {
                index += 1;
                serialPort.PortName = ArrayComPortsNames[index]; //"COM3";
                serialPort.BaudRate = 9600;
                serialPort.Open();
                if (!serialPort.IsOpen) return;
                serialPort.Write("who");
                System.Threading.Thread.Sleep(200);
                if (rxString.Contains("ArduCFI"))
                {
                    ComPortName = ArrayComPortsNames[index];
                    deviceDlog.SetSerialPortComboBoxText(ComPortName);
                }
                else
                    serialPort.Close();
            }
            while ((ComPortName == null) && (index < ArrayComPortsNames.GetUpperBound(0)));

            // if Arduino not found open the first com port in the list
            if (ComPortName == null)
            {
                ComPortName = ArrayComPortsNames[0];
                deviceDlog.SetSerialPortComboBoxText(ArrayComPortsNames[0]);
                serialPort.PortName = ComPortName;
                MessageBox.Show("Arduino CFI controller not found!");
                serialPort.Open();
                return;
            }
        } // InitSerial

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            rxString = serialPort.ReadLine();
     //       ArduinoDlog1.RxString = RxString;
            deviceDlog.DisplayText(rxString);
        }
    }
}
