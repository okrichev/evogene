using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
//using System.Windows.Media.Imaging;

namespace FluorControl
{
    enum SystemStates { Idle, AutoExposing, AcquiringDark, AcquiringDarkOnly, AcquiringSaturating, Live, Snap };

    public class ImplementationClass
    {
        private SystemStates systemState = SystemStates.Idle;

        private SerialPort serialPort = new SerialPort();
        private string rxString = "";
        private SettingsDialogBox settingsDlog;
        private DeviceDialogBox deviceDlog;
        private FluorControl mainForm;
        private SampleIDForm sampleIDdlog;
        private ControlPanelClass controlPanel;
        private AnalyzeForm analyzeDlog;

        private uc480.Camera Camera;
        uc480.Types.SensorInfo sensorInfo;
        //private bool bLive = false;
        int[] MemIDlist;
        Int32 s32MemID;
        private uc480.Defines.Status statusRet = 0;
        private Bitmap LastImage;
        private int[] ImageArray;
        private uc480.Types.Range<int> pixelClockRange;
        private uc480.Types.Range<double> exposureRange;
        private uc480.Types.Range<double> framerateRange;
        private int NframesShot;
        private int grayDepth = 1024;
        private int NofHotPixels = 20;
        private double SatToDarkPower = 21.74; //16.0793; 

        public event EventHandler ImagesSavedEvent;

        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
        private string FolderNameTmplt;
        private string fullFilename;
        private string fpath = "";

        private int defaultDarkLEDpower = 20; //15;
        private int defaultSatLEDpower = 255; //100;
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
        private double SettingsVersion = 2.5;
        public bool arduinoDetected = false;
        public bool cameraDetected = false;

        private Int32  currentPixClock = -1;
        private double currentExpTime = -1;
        private double currentFrameRate = -1;
        private Int32 currentNofFrames = -1;
        private Int32 currentLEDfreq = -1;

        private Int32 PenWidth = 5;
        private string PathToImageForAnalysis = "";
        public MaskParamStruc MaskParams;
        private Int32 DefaultMaskTop = 170;
        private Int32 DefaultMaskBot = 1010;
        private Int32 DefaultMaskLeft = 10;
        private Int32 DefaultMaskRight = 1218;
        private Int32 DefaultMaskNoRows = 8;
        private Int32 DefaultMaskNoCols = 12;
        private double DefaultWellSpacing_mm = 9;
        private double[] DefaultColorMapRange = {0, 0.8 };
        private double[,] FvFm_perWell;
        private double[,] PlantArea_perWell;
        Bitmap FvFmImage;
        


        public ImplementationClass()
        {
        }

        public ImplementationClass(FluorControl mnFrm, SettingsDialogBox SettingsDlog, DeviceDialogBox DeviceDlog, SampleIDForm SampleDlog, ControlPanelClass ControlPanel, AnalyzeForm AnalyzeDlog)
        {
            settingsDlog = SettingsDlog;
            deviceDlog = DeviceDlog;
            mainForm = mnFrm;
            sampleIDdlog = SampleDlog;
            controlPanel = ControlPanel;
            analyzeDlog = AnalyzeDlog;
        }

        public void Init_Everything()
        {
            InitSerial();
            try
            {
                InitCamera();
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't connect to camera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            settingsDlog.SetSettingsValue("General", "Version", SettingsVersion);

            if (cameraDetected)
            {
                settingsDlog.SetSettingsValue("Dark Acquisition", "LED freq (Hz)", defaultLEDfreq);
                settingsDlog.SetSettingsValue("Dark Acquisition", "LED power", defaultDarkLEDpower);
                settingsDlog.SetSettingsValue("Dark Acquisition", "Pixel Clock MHz", defaultPixelClock);
                settingsDlog.SetSettingsValue("Dark Acquisition", "Frame Rate (fps)", defaultFrameRateDark);
                settingsDlog.SetSettingsValue("Dark Acquisition", "Exposure Time (ms)", defaultExposureTimeDark);
                settingsDlog.SetSettingsValue("Dark Acquisition", "No of Frames", defaultNofFramesDark);
                settingsDlog.SetSettingsRange("Dark Acquisition", "File Extension", defaultFileModifierDark);

                settingsDlog.SetSettingsValue("Saturating Acquisition", "LED freq (Hz)", defaultLEDfreq);
                settingsDlog.SetSettingsValue("Saturating Acquisition", "LED power", defaultSatLEDpower);
                settingsDlog.SetSettingsValue("Saturating Acquisition", "Pixel Clock MHz", defaultPixelClock);
                settingsDlog.SetSettingsValue("Saturating Acquisition", "Frame Rate (fps)", defaultFrameRateSat);
                settingsDlog.SetSettingsValue("Saturating Acquisition", "Exposure Time (ms)", defaultExposureTimeSat);
                settingsDlog.SetSettingsValue("Saturating Acquisition", "No of Frames", defaultNofFramesSat);
                settingsDlog.SetSettingsRange("Saturating Acquisition", "File Extension", defaultFileModifierSat);

                settingsDlog.SetSettingsValue("Autoexposure", "LED freq (Hz)", defaultLEDfreq);
                settingsDlog.SetSettingsValue("Autoexposure", "LED power", defaultDarkLEDpower);
                settingsDlog.SetSettingsValue("Autoexposure", "Pixel Clock MHz", defaultPixelClock);
                settingsDlog.SetSettingsValue("Autoexposure", "Frame Rate (fps)", 5);
                settingsDlog.SetSettingsValue("Autoexposure", "Exposure Time (ms)", 200);
                settingsDlog.SetSettingsValue("Autoexposure", "No of Frames", 2);
                settingsDlog.SetSettingsRange("Autoexposure", "File Extension", "_test_");
                settingsDlog.SetSettingsValue("Autoexposure", "Hot Pixels No", NofHotPixels);
                settingsDlog.SetSettingsValue("Autoexposure", "Sat to Dark power ratio", SatToDarkPower);
            }

            settingsDlog.SetSettingsValue("Analysis", "Mask Top", DefaultMaskTop);
            settingsDlog.SetSettingsValue("Analysis", "Mask Bottom", DefaultMaskBot);
            settingsDlog.SetSettingsValue("Analysis", "Mask Left", DefaultMaskLeft);
            settingsDlog.SetSettingsValue("Analysis", "Mask Right", DefaultMaskRight);
            settingsDlog.SetSettingsValue("Analysis", "Mask #Rows", DefaultMaskNoRows);
            settingsDlog.SetSettingsValue("Analysis", "Mask #Cols", DefaultMaskNoCols);
            settingsDlog.SetSettingsValue("Analysis", "Well spacing (mm)", DefaultWellSpacing_mm);
            settingsDlog.SetSettingsValue("Analysis", "ColorMap min", DefaultColorMapRange[0]);
            settingsDlog.SetSettingsValue("Analysis", "ColorMap max", DefaultColorMapRange[1]);
           

            LoadDefaultSettings();
            InitAnalyzeForm();

            systemState = SystemStates.Idle;
           // settingsDlog.SetSettingsValue("General", "Version", 10);
        }

        public static List<SettingsPanelClass> InitializePanelList()
        {
            List<SettingsPanelClass> list = new List<SettingsPanelClass>();

            list.Add(new SettingsPanelClass("General", InitializeGeneralSettingsPanel()));
            list.Add(new SettingsPanelClass("Dark Acquisition", InitializeAcquisitionPanelTableEntryList("Dark Acquisition")));
            list.Add(new SettingsPanelClass("Saturating Acquisition", InitializeAcquisitionPanelTableEntryList("Saturating Acquisition")));
            //list.Add(new SettingsPanelClass("Autoexposure", InitializeAcquisitionPanelTableEntryList("Autoexposure")));
            SettingsPanelClass AutoExposureItem = new SettingsPanelClass("Autoexposure", InitializeAcquisitionPanelTableEntryList("Autoexposure"));
            AutoExposureItem.Properties.Add(new SettingsTableEntryClass("Hot Pixels No", 20, "[0 1300000]", "Autoexposure", false, true));
            AutoExposureItem.Properties.Add(new SettingsTableEntryClass("Sat to Dark power ratio", 16.0793, "", "Autoexposure", false, true));
            AutoExposureItem.Properties.Add(new SettingsTableEntryClass("Max Fv/Fm estimate", 0.9, "[0 1]", "Autoexposure", false, true));
            AutoExposureItem.Properties.Add(new SettingsTableEntryClass("Do autoexposure", 1, "0 or 1", "Autoexposure", false, true));
            list.Add(AutoExposureItem);
            list.Add(new SettingsPanelClass("Analysis", InitializeAnalysisSettingsPanel()));
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


        private static List<SettingsTableEntryClass> InitializeAcquisitionPanelTableEntryList(string PanelName)
        {
            List<SettingsTableEntryClass> list = new List<SettingsTableEntryClass>();
            list.Add(new SettingsTableEntryClass("LED freq (Hz)", 32000 , "[1 2000000]", PanelName, false, true));
            list.Add(new SettingsTableEntryClass("LED power", 15, "[0 255]", PanelName, false, true));
            list.Add(new SettingsTableEntryClass("Pixel Clock MHz", 7, "[7 15]", PanelName, false, true));
          //  list.Add(new SettingsTableEntryClass("LED power", 15, "[0 255]", PanelName, false, false));
            list.Add(new SettingsTableEntryClass("Frame Rate (fps)", 0.5, "", PanelName, false, true));
            list.Add(new SettingsTableEntryClass("Exposure Time (ms)", 2000, "", PanelName, false, true));
            list.Add(new SettingsTableEntryClass("No of Frames", 2, "", PanelName, false, true));
            list.Add(new SettingsTableEntryClass("File Extension", 0, "_dark_", PanelName, false, false));
            return list;
        }

        private static List<SettingsTableEntryClass> InitializeAnalysisSettingsPanel()
        {
            List<SettingsTableEntryClass> list = new List<SettingsTableEntryClass>();
            list.Add(new SettingsTableEntryClass("Mask Top", 10, "[0 2000]", "Analysis", false, true));
            list.Add(new SettingsTableEntryClass("Mask Bottom", 1014, "[0 2000]", "Analysis", false, true));
            list.Add(new SettingsTableEntryClass("Mask Left", 10, "[0 2000]", "Analysis", false, true));
            list.Add(new SettingsTableEntryClass("Mask Right", 1200, "[0 2000]", "Analysis", false, true));
            list.Add(new SettingsTableEntryClass("Mask #Rows", 8, "[0 2000]", "Analysis ", false, true));
            list.Add(new SettingsTableEntryClass("Mask #Cols", 12, "[0 2000]", "Analysis", false, true));
            list.Add(new SettingsTableEntryClass("Min Thresh part of Global", 0.5, "[0 1]", "Analysis", false, true));
            list.Add(new SettingsTableEntryClass("Well spacing (mm)", 9, "[0 100]", "Analysis", false, true));
            list.Add(new SettingsTableEntryClass("ColorMap min", 0, "[-100 100]", "Analysis", false, true));
            list.Add(new SettingsTableEntryClass("ColorMap max", 0.8, "[-100 100]", "Analysis", false, true));
            return list;
        }

        public void InitAnalyzeForm()
        {
           // analyzeDlog.DarkImage =  new Bitmap(sensorInfo.MaxSize.Width, sensorInfo.MaxSize.Height);
            SettingsPanelClass panel = settingsDlog.PanelList.FirstOrDefault(item => item.Name == "Analysis");
            analyzeDlog.Set_MaskNumups("Top", panel.Properties.FirstOrDefault(item => item.Name == "Mask Top").Value);
            analyzeDlog.Set_MaskNumups("Bottom", panel.Properties.FirstOrDefault(item => item.Name == "Mask Bottom").Value);
            analyzeDlog.Set_MaskNumups("Left", panel.Properties.FirstOrDefault(item => item.Name == "Mask Left").Value);
            analyzeDlog.Set_MaskNumups ("Right", panel.Properties.FirstOrDefault(item => item.Name == "Mask Right").Value);
            analyzeDlog.Set_MaskNumups("NoRows", panel.Properties.FirstOrDefault(item => item.Name == "Mask #Rows").Value);
            analyzeDlog.Set_MaskNumups("NoCols", panel.Properties.FirstOrDefault(item => item.Name == "Mask #Cols").Value);
            analyzeDlog.Set_MaskNumups("ColorMapMin", settingsDlog.GetSettingsValue("Analysis", "ColorMap min"));
            analyzeDlog.Set_MaskNumups("ColorMapMax", settingsDlog.GetSettingsValue("Analysis", "ColorMap max"));
        }


        public void SaveSettingsAs()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fpath1 = saveFileDialog1.FileName;
                SaveSettingsTo(fpath1);
            }
        }

        private void SaveSettingsTo(string fpath1)
        {
            BinarySerialization.WriteToBinaryFile<List<SettingsPanelClass>>(fpath1, settingsDlog.PanelList);
        }

        public void SaveSettings()
        {
            SaveSettingsTo(defaultSettingsPath);
        }

        public void LoadDefaultSettings()
        {
            LoadSettingsFrom(defaultSettingsPath);
        }

        public void OpenSettings()
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
         //   openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Filter = "binary (*.bin)|*.bin|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fpath1 = openFileDialog1.FileName;
                    LoadSettingsFrom(fpath1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void  LoadSettingsFrom(string fpath1)
        {
            //string fpath = Path.Combine(Application.StartupPath, "FluorControlSettings.bin");
            if (!File.Exists(fpath1))
                return;

            List<SettingsPanelClass> panelList = BinarySerialization.ReadFromBinaryFile<List<SettingsPanelClass>>(fpath1);
           // panelList.Find(item => item.Name.Equals("General"))
            double savedSettingsVersion = 0;
            SettingsTableEntryClass versItem = null;
            SettingsPanelClass genPanel = panelList.FirstOrDefault(o => o.Name == "General");
            if (genPanel != null)
                versItem = genPanel.Properties.FirstOrDefault(item => item.Name == "Version");
            if (versItem != null)
                savedSettingsVersion = versItem.Value;

            mainForm.OutputToLogBook("Settings loading from " + fpath1);
            if (savedSettingsVersion == SettingsVersion)
            {
                settingsDlog.PanelList = panelList;
            }
            else // doing our best to match to a newer version
            {
                mainForm.OutputToLogBook("Matching version " + savedSettingsVersion.ToString() + " to new settings version " + SettingsVersion.ToString());
                if (versItem != null)
                    versItem.Value = SettingsVersion;
                foreach (var Panel in panelList)
                {
                    foreach (var item in Panel.Properties)
                    {
                        if (settingsDlog.SetSettingsValue(Panel.Name, item.Name, item.Value))
                            mainForm.OutputToLogBook("Have not found a match for Value of " + item.Name + " of " + Panel.Name);
                        if (settingsDlog.SetSettingsRange(Panel.Name, item.Name, item.Range))
                            mainForm.OutputToLogBook("Have not found a match for Range of " + item.Name + " of " + Panel.Name);
                    }
                }
            }

        } //LoadSettings




        public void InitSerial()
        {
            //get available serial port names
            string[] ArrayComPortsNames = null;
            int index = -1;
            string ComPortName = null;
             
            deviceDlog.ClearSerialPortComboBox();
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
            serialPort.DataReceived -= serialPort_DataReceived; // to avoid adding two handlers
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
            do
            {
                index += 1;
                if (serialPort.IsOpen)
                    serialPort.Close();
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
                    deviceDlog.SetArduinoName(rxString);
                    arduinoDetected = true;
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
        } // serialPort_DataReceived

        public void SendCommandArduino(string command)
        {
            if (arduinoDetected)
                serialPort.Write(command);
            else
                MessageBox.Show("No Arduino detected!");
        } //SendCommandArduino

        public void InitCamera()
        {
            Camera = new uc480.Camera();//Use only the empty constructor, the one with cameraID has a bug
            // Open Camera
            statusRet = Camera.Init();//You can specify a particular cameraId here if you want to open a specific camera

            if (statusRet != uc480.Defines.Status.SUCCESS)
            {
                MessageBox.Show("Camera initializing failed");
                return;
            }


            Camera.Information.GetSensorInfo(out sensorInfo);
            string[] cameraPropList = new string[4];
            cameraPropList[0] = sensorInfo.SensorName;
            cameraPropList[1] = sensorInfo.SensorColorMode.ToString();
            cameraPropList[2] = sensorInfo.MaxSize.Height.ToString();
            cameraPropList[3] = sensorInfo.MaxSize.Width.ToString();
           // grayDepth = sensorInfo.SensorColorMode;

            deviceDlog.setCameraListBox(cameraPropList);

            // LastImage = new Bitmap(sensorInfo.MaxSize.Width, sensorInfo.MaxSize.Height, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);

            LastImage = new Bitmap(sensorInfo.MaxSize.Width, sensorInfo.MaxSize.Height);

            statusRet = Camera.PixelFormat.Set(uc480.Defines.ColorMode.Mono10);
            if (statusRet != uc480.Defines.Status.SUCCESS)
            {
                MessageBox.Show("Pixel format set failed");
                return;
            }

            bool proceedOK = false;
            if (SetPixelClock(7))
                if (SetFrameRate(10))
                    if (SetExposureTime(1))
                        proceedOK = true;
            if (!proceedOK)
                return;


            // Allocate Memory


            statusRet = Camera.Memory.Allocate(out s32MemID, true);
            if (statusRet != uc480.Defines.Status.SUCCESS)
            {
                MessageBox.Show("Allocate Memory failed");
                return;
            }

            // Start Live Video
            /*           statusRet = Camera.Acquisition.Capture(uc480.Defines.DeviceParameter.Wait);
                       if (statusRet != uc480.Defines.Status.SUCCESS)
                       {
                           MessageBox.Show("Start Live Video failed");
                           return;
                       }
                       else
                       {
                           bLive = true;
                           SystemState = SystemStates.Live;
                       }
           */
            // Connect Event
            //     Camera.EventFrame += onFrameEvent;
            Camera.Display.AutoRender.SetEnable(true);
            Camera.Display.AutoRender.SetMode(uc480.Defines.DisplayRenderMode.FitToWindow);
            Camera.Display.AutoRender.SetWindow(mainForm.GetPicBoxImageHandle());
            Camera.EventSequence += OnSequenceEvent;

            Camera.Timing.PixelClock.GetRange(out pixelClockRange);
            string pixClockRangeString = "[" + pixelClockRange.Minimum.ToString() + "   " + pixelClockRange.Maximum.ToString() + "]";
            settingsDlog.SetSettingsRange("Dark Acquisition", "Pixel Clock MHz", pixClockRangeString);
            settingsDlog.SetSettingsRange("Saturating Acquisition", "Pixel Clock MHz", pixClockRangeString);
            controlPanel.set_PixelClockLabelAndLimits("Pixel Clock\r\n" +pixClockRangeString + " MHz", pixelClockRange);
            controlPanel.SetFrameRate(10);
            controlPanel.SetExposureTime(200);

            Camera.EventFrame += OnFrameEvent;
            cameraDetected = true;

            ImagesSavedEvent += OnImageSavedEvent;

        } //InitCamera

        public bool SetPixelClock(Int32 PixClockIn)
        {
            if (currentPixClock != PixClockIn)
            {
                  statusRet = Camera.Timing.PixelClock.Set(PixClockIn);
                if (statusRet != uc480.Defines.Status.SUCCESS)
                {
                    MessageBox.Show("Pixel clock set failed");
                    return false;
                }
                else
                {
                    Camera.Timing.PixelClock.Get(out currentPixClock);
                    GetFramerateRange(out framerateRange);
                    return true;
                }
            }
            else 
                return true;
        }

        public string GetFramerateRange(out uc480.Types.Range<double> frRateRange)
        {
            decimal FramerateMin;
            decimal FramerateMax;
     
            Camera.Timing.Framerate.GetFrameRateRange(out frRateRange);
            FramerateMin = Convert.ToDecimal(frRateRange.Minimum);
            FramerateMax = Convert.ToDecimal(frRateRange.Maximum);

            return "[" + FramerateMin.ToString("F3") +
                "   " + FramerateMax.ToString("F3") + "]";
        }

        public string GetExposureRange(out uc480.Types.Range<double> expRange)
        {
            decimal ExposureMin;
            decimal ExposureMax;

            Camera.Timing.Exposure.GetRange(out expRange);
            ExposureMin = Convert.ToDecimal(expRange.Minimum);
            ExposureMax = Convert.ToDecimal(expRange.Maximum);
            return "[" + ExposureMin.ToString("F3") +
                "   " + ExposureMax.ToString("F3") + "]";
        }

        public bool SetFrameRate(double frRateIn)
        {
            if (currentFrameRate != frRateIn)
            {
                statusRet = Camera.Timing.Framerate.Set(frRateIn);
                if (statusRet != uc480.Defines.Status.SUCCESS)
                {
                    MessageBox.Show("Frame rate set failed");
                    return false;
                }
                else
                {
                    Camera.Timing.Framerate.Get(out currentFrameRate); // the exact frame rate might not correspond to the value. Checking and updating the value
                    GetExposureRange(out exposureRange);
                    return true;
                }
            }
            else
                return true;
        }

        public bool SetExposureTime(double expTimeIn)
        {
            if (currentExpTime != expTimeIn)
            {
                statusRet = Camera.Timing.Exposure.Set(expTimeIn);
                if (statusRet != uc480.Defines.Status.SUCCESS)
                {
                    MessageBox.Show("Exposure time set failed");
                    return false;
                }
                else
                {
                    Camera.Timing.Exposure.Get(out currentExpTime); // the exact exp time might not correspond to the value. Checking and updating the value
                    return true;
                }
            }
            else
                return true;
        }

        public bool SetLEDfreq(Int32 LEDfreqIn)
        {
            if (arduinoDetected)
            {
                if (currentLEDfreq != LEDfreqIn)
                {
                    serialPort.Write("SetPWMfreq " + LEDfreqIn.ToString());
                    currentLEDfreq = LEDfreqIn;
                }
                return true;
            }
            else return false;
        }

        private void SetAcquisitionParams(SettingsPanelClass panel)
        {
            Int32 LEDfreqIn = Convert.ToInt32(panel.Properties.FirstOrDefault(item => item.Name == "LED freq (Hz)").Value);
            if (!SetLEDfreq(LEDfreqIn))
            {
                MessageBox.Show("LED frequency not set");
                return;
            }

            Int32 pixClockIn = Convert.ToInt32(panel.Properties.FirstOrDefault(item => item.Name =="Pixel Clock MHz").Value);
            if (!SetPixelClock(pixClockIn))
                return;

            double frameRateIn = panel.Properties.FirstOrDefault(item => item.Name == "Frame Rate (fps)").Value;
            if (!SetFrameRate(frameRateIn))
                return;

            double expTimeIn = panel.Properties.FirstOrDefault(item => item.Name == "Exposure Time (ms)").Value;
            if (!SetExposureTime(expTimeIn))
                return;
        }

        public void TreatSettingsChange(SettingsTableEntryClass item)
        {
            if (item.Name.Equals("Pixel Clock MHz"))
            {
                if (SetPixelClock(Convert.ToInt32(item.Value)))
                {
                   // var panel = settingsDlog.PanelList.FirstOrDefault(o => o.Name == item.ParentName);
                    settingsDlog.SetSettingsRange(item.ParentName, "Frame Rate (fps)", GetFramerateRange(out framerateRange));
                }
                item.Value = currentPixClock;
            }
            
            if (item.Name.Equals("Frame Rate (fps)"))
            {
                if (SetFrameRate(item.Value))
                {
                    // var panel = settingsDlog.PanelList.FirstOrDefault(o => o.Name == item.ParentName);
                    settingsDlog.SetSettingsRange(item.ParentName, "Exposure Time (ms)", GetExposureRange(out exposureRange));
                }
                item.Value = currentFrameRate;
            }

            if (item.Name.Equals("Exposure Time (ms)"))
            {
                SetExposureTime(item.Value);
                item.Value = currentExpTime;
            }
        } // TreatSettingsChange

        public void GetSaveFolder()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                FolderNameTmplt = folderBrowserDialog1.SelectedPath;
                mainForm.setFolderPathString(FolderNameTmplt);
            }
        }


        private void DoSequence(SettingsPanelClass panel)
        {
            FolderNameTmplt = mainForm.GetFolderPath();
            if (FolderNameTmplt.Equals(""))
            {
                MessageBox.Show("You have to choose folder name template first!");
                return;
            }

            mainForm.OutputToLogBook("Starting " + panel.Name);
            string folderName = CoerceValidFileName(sampleIDdlog.GetSampleID());
            fpath = FolderNameTmplt +  "\\" + folderName;
            if (!Directory.Exists(fpath))
            {
               // MessageBox.Show("Creating new directory");
                System.IO.Directory.CreateDirectory(fpath);
            }


            SetAcquisitionParams(panel);

           // Camera.EventSequence -= OnSequenceEvent; // detach the event handler otherwise it gets repeated
            string fileModifier = panel.Properties.FirstOrDefault(item => item.Name == "File Extension").Range;
            fullFilename = fpath + "\\Plant" + fileModifier;
           // int i;

            currentNofFrames = Convert.ToInt32(panel.Properties.FirstOrDefault(item => item.Name == "No of Frames").Value);
            MemIDlist = new int[currentNofFrames];
            Camera.Acquisition.Stop();

            for (int i = 0; i < MemIDlist.Length; i++)
            {
                statusRet = Camera.Memory.Allocate(out MemIDlist[i], true);
                if (statusRet != uc480.Defines.Status.SUCCESS)
                {
                    MessageBox.Show("Allocate Memory failed");
                }
            }

            statusRet = Camera.Memory.Sequence.Add(MemIDlist);
            if (statusRet != uc480.Defines.Status.SUCCESS)
            {
                MessageBox.Show("Add Memory to sequence failed");
            }
           // Camera.EventSequence += OnSequenceEvent;

          //  System.Threading.Thread.Sleep(1000); // need to wait a bit for camera to change settings
            // switch on light
//            serialPort1.Write("LON " + AcqParams.LEDpower.ToString());
            
            // switch on light
            Int32 LEDpower = Convert.ToInt32(panel.Properties.FirstOrDefault(item => item.Name == "LED power").Value);
            serialPort.Write("LON " + LEDpower.ToString());

             // start capture
            NframesShot = 0;
            statusRet = Camera.Acquisition.Capture(uc480.Defines.DeviceParameter.Wait);
            if (statusRet != uc480.Defines.Status.SUCCESS)
            {
                MessageBox.Show("Capture has not started: error " + statusRet.ToString());
                return;
            }

            // save settings to text file
            System.IO.StreamWriter settingsTxtfile = new System.IO.StreamWriter(fpath + "\\AcqusitionSettings"+ fileModifier + ".txt");
            settingsTxtfile.WriteLine(panel.ToString());
            settingsTxtfile.Close();

            SaveSettingsTo(Path.Combine(fpath, "FluorControlSettings.bin"));
        }

        private void OnFrameEvent(object sender, EventArgs e)
        {
   //         if (SystemState == SystemStates.Live)
    //            return;
            NframesShot += 1;
          //  mainForm.OutputToLogBook("Shot " + NframesShot.ToString());
        }

        private void OnSequenceEvent(object sender, EventArgs e)
        {
            if (systemState == SystemStates.Live)
                return;
            if (systemState == SystemStates.Snap)
            {
                systemState = SystemStates.Idle;
                return;
            }
  
            // sender is our camera object
            //uc480.Camera Cam = sender as uc480.Camera;
            Camera.Acquisition.Stop();

            // stop light
            serialPort.Write("LOF");
            mainForm.OutputToLogBook("Saving " + NframesShot.ToString() + " images...");
           // bLive = false;
            for (int i = 0; i < MemIDlist.Length; i++)
            {
                // MessageBox.Show(fname + i.ToString());
                statusRet = Camera.Image.Save(fullFilename + i.ToString() + ".png", MemIDlist[i], System.Drawing.Imaging.ImageFormat.Png, 100);
                if (statusRet != uc480.Defines.Status.SUCCESS)
                {
                    MessageBox.Show("Not saved: error " + statusRet.ToString());
                }
             //   mainForm.OutputToLogBook("Saved " + i.ToString());
            }
            statusRet = Camera.Memory.ToBitmap(MemIDlist[MemIDlist.Length-1], out LastImage); // copy image for processing

            statusRet = Camera.Memory.CopyToArray(MemIDlist[MemIDlist.Length - 1], out ImageArray);
            if (statusRet != uc480.Defines.Status.SUCCESS)
            {
                MessageBox.Show("Image was not copied well!");
                return;
            }

            Camera.Memory.Sequence.Clear();
            Camera.Memory.Free(MemIDlist);
            mainForm.OutputToLogBook("Acquisition done \r\n");
            //if ((systemState == SystemStates.AcquiringDark) | (systemState == SystemStates.AutoExposing))
            ImagesSavedRaiseEvent(EventArgs.Empty);
            //else
             //   systemState = SystemStates.Idle;
        //    if (systemState == SystemStates.AcquiringSaturating)
        //        mainForm.EnableDisableFvFmButton(true);

            
        }

        public virtual void ImagesSavedRaiseEvent(EventArgs e)
        {
            EventHandler handler = ImagesSavedEvent;
           // mainForm.OutputToLogBook("ImagesSavedRaiseEvent");
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnImageSavedEvent(object sender, EventArgs e)
        {
            switch (systemState)
            {
                case SystemStates.AutoExposing:
                    SettingsPanelClass darkpanel = settingsDlog.PanelList.FirstOrDefault(item => item.Name == "Dark Acquisition");
                    GetAutoExposure();
                    systemState = SystemStates.AcquiringDark;
                    DoSequence(darkpanel);
                    break;
                case SystemStates.AcquiringDark:
                    SettingsPanelClass satpanel = settingsDlog.PanelList.FirstOrDefault(item => item.Name == "Saturating Acquisition");
                    systemState = SystemStates.AcquiringSaturating;
                    DoSequence(satpanel);
                    break;
                case SystemStates.AcquiringSaturating:
                    systemState = SystemStates.Idle;
                    break;
            }
        }

        public void DoAllAcquisitionSafe() // not so safe it appears
        {
            mainForm.BringToFront();
            SettingsPanelClass panel = settingsDlog.PanelList.FirstOrDefault(item => item.Name == "Dark Acquisition");
            DoSequence(panel);
            double estimatedTime_ms = (Convert.ToDouble(currentNofFrames - 1)/currentFrameRate*1000 + currentExpTime)*1.2; // 1.2 safety measure
            mainForm.OutputToLogBook(estimatedTime_ms.ToString()+ "\r\n");
            System.Threading.Thread.Sleep(Convert.ToInt32(estimatedTime_ms));
            OnSequenceEvent(null, null);

            panel = settingsDlog.PanelList.FirstOrDefault(item => item.Name == "Saturating Acquisition");
            DoSequence(panel);
            estimatedTime_ms = (Convert.ToDouble(currentNofFrames -1)/currentFrameRate*1000 + currentExpTime)*1.2; // 1.2 safety measure
            mainForm.OutputToLogBook(estimatedTime_ms.ToString());
            System.Threading.Thread.Sleep(Convert.ToInt32(estimatedTime_ms));
            OnSequenceEvent(null, null);

            SaveSettingsTo(Path.Combine(fpath, "FluorControlSettings.bin"));
        }

        public void DoAllAcquisition()
        {
            mainForm.Activate();
          //  mainForm.EnableDisableFvFmButton(false);
            SettingsPanelClass panel = settingsDlog.PanelList.FirstOrDefault(item => item.Name == "Autoexposure");
            if (panel.Properties.FirstOrDefault(item => item.Name == "Do autoexposure").Value.Equals(1))
            {
                systemState = SystemStates.AutoExposing;
            }
            else
            {
                panel = settingsDlog.PanelList.FirstOrDefault(item => item.Name == "Dark Acquisition");
                systemState = SystemStates.AcquiringDark;
            }
            DoSequence(panel);
           // SaveSettingsTo(Path.Combine(fpath, "FluorControlSettings.bin"));
        }

        public void SaveLog(RichTextBox logBox)
        {
            saveFileDialog1.InitialDirectory = fpath;
            saveFileDialog1.FileName = "Log.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fpath1 = saveFileDialog1.FileName;
                logBox.SaveFile(fpath1+ ".txt", RichTextBoxStreamType.PlainText);
            }
        }

 
        public static string CoerceValidFileName(string filename)
        {
            /// <summary>
            /// Strip illegal chars and reserved words from a candidate filename (should not include the directory path)
            /// </summary>
            /// <remarks>
            /// http://stackoverflow.com/questions/309485/c-sharp-sanitize-file-name
            /// </remarks>
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidReStr = string.Format(@"[{0}]+", invalidChars);

            var reservedWords = new[]
                {
                    "CON", "PRN", "AUX", "CLOCK$", "NUL", "COM0", "COM1", "COM2", "COM3", "COM4",
                    "COM5", "COM6", "COM7", "COM8", "COM9", "LPT0", "LPT1", "LPT2", "LPT3", "LPT4",
                    "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
                };

            var sanitisedNamePart = Regex.Replace(filename, invalidReStr, "_");
            foreach (var reservedWord in reservedWords)
            {
                var reservedWordPattern = string.Format("^{0}\\.", reservedWord);
                sanitisedNamePart = Regex.Replace(sanitisedNamePart, reservedWordPattern, "_reservedWord_.", RegexOptions.IgnoreCase);
            }

            return sanitisedNamePart;
        }


        public void switchLED(Int32 LEDpower, Int32 LEDfreq)
        {
            if (SetLEDfreq(LEDfreq))
                if (LEDpower.Equals(0))
                    serialPort.Write("LOF ");
                else
                    serialPort.Write("LON " + LEDpower.ToString());
        }

        public void switchLEDoff()
        {
            switchLED(0, currentLEDfreq);
        }

        public bool StartLive(Int32 pixClockLive, double frameRateLive, double expTimeLive)
        {
            if (!cameraDetected)
            {
                MessageBox.Show("No camera detected!");
                return false;
            }
            
            bool proceedOK = false;
            if (SetPixelClock(pixClockLive))
                if (SetFrameRate(frameRateLive))
                    if (SetExposureTime(expTimeLive))
                        proceedOK = true;
            if (!proceedOK)
            {
                MessageBox.Show("Failed to set camera parameters!");
                return false;
            }

            statusRet = Camera.Memory.SetActive(s32MemID);
            if (statusRet != uc480.Defines.Status.SUCCESS)
            {
                MessageBox.Show("Failed to set active memory!");
                return  false;
            }

            systemState = SystemStates.Live;
            statusRet = Camera.Acquisition.Capture(uc480.Defines.DeviceParameter.Wait);
            if (statusRet != uc480.Defines.Status.SUCCESS)
            {
                MessageBox.Show("Failed to start capture!");
                systemState = SystemStates.Idle;
                return false;
            }

            
            return true;
        }
 
        public bool FreezeCamera()
        {
            if (!cameraDetected)
            {
                MessageBox.Show("No camera detected!");
                return false;
            }

            statusRet = Camera.Acquisition.Freeze(uc480.Defines.DeviceParameter.Wait);
            if (statusRet == uc480.Defines.Status.SUCCESS)
            {
                systemState = SystemStates.Snap;
                statusRet = Camera.Memory.ToBitmap(s32MemID, out LastImage); // copy image for processing

   /*             statusRet = Camera.Memory.CopyToArray(s32MemID, out ImageArray);
                if (statusRet != uc480.Defines.Status.SUCCESS)
                {
                    MessageBox.Show("Image was not copied well!");
                    return false;
                }
    */
                return true;
            }
            else
            {
                MessageBox.Show("Failed to freeze!");
                return false;
            }
        }

        public bool SnapImage(Int32 pixClockLive, double frameRateLive, double expTimeLive)
        {
            if (!cameraDetected)
            {
                MessageBox.Show("No camera detected!");
                return false;
            }

            bool proceedOK = false;
            if (SetPixelClock(pixClockLive))
                if (SetFrameRate(frameRateLive))
                    if (SetExposureTime(expTimeLive))
                        proceedOK = true;
            if (!proceedOK)
            {
                MessageBox.Show("Failed to set camera parameters!");
                return false;
            }

            statusRet = Camera.Memory.SetActive(s32MemID);
            if (statusRet != uc480.Defines.Status.SUCCESS)
            {
                MessageBox.Show("Failed to set active memory!");
                return false;
            }

            systemState = SystemStates.Snap;
            statusRet = Camera.Acquisition.Freeze(uc480.Defines.DeviceParameter.Wait);
            if (statusRet != uc480.Defines.Status.SUCCESS)
            {
                MessageBox.Show("Failed to start capture!");
                systemState = SystemStates.Idle;
                return false;
            }

            
            //LastImage.Dispose();
         //   LastImage = new Bitmap(sensorInfo.MaxSize.Width, sensorInfo.MaxSize.Height, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
            statusRet = Camera.Memory.ToBitmap(s32MemID, out LastImage); // copy image for processing

 /*           if (statusRet == uc480.Defines.Status.SUCCESS)
                MessageBox.Show(LastImage.GetPixel(1, 1).GetBrightness().ToString());
            else
                MessageBox.Show("Did not get image to bitmap!");
*/
/*            statusRet = Camera.Memory.CopyToArray(s32MemID, out ImageArray);
            if (statusRet != uc480.Defines.Status.SUCCESS)
            {
                MessageBox.Show("Image was not copied well!");
                return false;
            }
 */ 


            return true;
        }

        private void GetAutoExposure()
        {
            //int[] hist = DoHistogram(ImageArray);
            Bitmap TestImage = new Bitmap(Image.FromFile(fpath + "\\Plant_test_1.png"));
            Int32[] hist = DoHistogram8bit(TestImage);

            int Nhot = (int)settingsDlog.GetSettingsValue("Autoexposure", "Hot Pixels No");

            int HotCount = 0;
            int i = hist.Length - 1;
            while (HotCount < Nhot)
            {
                HotCount += hist[i];
                i--;
            }

            double TestExp = settingsDlog.GetSettingsValue("Autoexposure", "Exposure Time (ms)");

            //double DarkExp = TestExp * (double)grayDepth / (double)i;
            double DarkExp = TestExp * 256.0 / (double)i*0.9; // take 10% shorter for confidence
            mainForm.OutputToLogBook("Setting Exp time of " + DarkExp.ToString());
            settingsDlog.SetSettingsValue("Dark Acquisition", "Frame Rate (fps)", 990 / DarkExp); // 1% smaller rate
            settingsDlog.SetSettingsValue("Dark Acquisition", "Exposure Time (ms)", DarkExp);
            

            double SatExp = DarkExp / (SatToDarkPower * (1 + settingsDlog.GetSettingsValue("Autoexposure", "Max Fv/Fm estimate")));
            settingsDlog.SetSettingsValue("Saturating Acquisition", "Frame Rate (fps)", 990 / SatExp); // 1% smaller rate
            settingsDlog.SetSettingsValue("Saturating Acquisition", "Exposure Time (ms)", SatExp);

            //mainForm.OutputToLogBook
             //   MessageBox.Show("Max value = " + ImageArray.Max().ToString());
            //DoHistogram(LastImage);

        //    int i = 1;
         //   LastImage = new Bitmap(fullFilename + i.ToString() + ".png");
            //MessageBox.Show(LastImage.GetPixel(500, 500).GetBrightness().ToString());
            
        }

        private void DoHistogram(Bitmap bmp)
        {
            // modification of https://stackoverflow.com/questions/20055024/draw-histogram-from-points-array
            //Bitmap bmp = new Bitmap(@"C:\Users\Public\Pictures\Sample Pictures\Desert.jpg");
            int[] hist = new int[grayDepth];
            float max = 0;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int grayLevel = (int)Math.Round(bmp.GetPixel(i, j).GetBrightness() * (grayDepth-1));
                    hist[grayLevel]++;
                    if (max < hist[grayLevel])
                        max = hist[grayLevel];
                }
            }

            int histHeight = (int)(grayDepth / 2);
            Bitmap img = new Bitmap(grayDepth, histHeight + 10);
            using (Graphics g = Graphics.FromImage(img))
            {
                for (int i = 0; i < hist.Length; i++)
                {
                    float pct = hist[i] / max;   // What percentage of the max is this value?
                    g.DrawLine(Pens.Black,
                        new Point(i, img.Height - 5),
                        new Point(i, img.Height - 5 - (int)(pct * histHeight))  // Use that percentage of the height
                        );
                }
            }
            img.Save(Path.Combine(fpath, "Histogram.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            System.IO.StreamWriter histTxtFile = new System.IO.StreamWriter(fpath + "\\Histogram.txt");
            histTxtFile.WriteLine(hist.ToString());
            for (int i = 0; i < hist.Length; i++)
            {
                histTxtFile.WriteLine(hist[i].ToString());
            }
            histTxtFile.Close();
        }

        private int[] DoHistogram(int[] ImArray)
        {
            // modification of https://stackoverflow.com/questions/20055024/draw-histogram-from-points-array
            //Bitmap bmp = new Bitmap(@"C:\Users\Public\Pictures\Sample Pictures\Desert.jpg");
            int[] hist = new int[grayDepth];
            float max = 0;

            for (int i = 0; i < ImArray.Length; i++)
            {
                    hist[ImArray[i]]++;
                    if (max < hist[ImArray[i]])
                        max = hist[ImArray[i]];
                
            }

/*            int histHeight = grayDepth;
            Bitmap img = new Bitmap(grayDepth, histHeight + 10);
            using (Graphics g = Graphics.FromImage(img))
            {
                for (int i = 0; i < hist.Length; i++)
                {
                    float pct = hist[i] / max;   // What percentage of the max is this value?
                    g.DrawLine(Pens.Black,
                        new Point(i, img.Height - 5),
                        new Point(i, img.Height - 5 - (int)(pct * histHeight))  // Use that percentage of the height
                        );
                }
            }
            img.Save(Path.Combine(fpath, "Histogram.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            System.IO.StreamWriter histTxtFile = new System.IO.StreamWriter(fpath + "\\Histogram.txt");
            histTxtFile.WriteLine(ImageArray.Length.ToString() + " ImageArray  length ");
            for (int i = 0; i < hist.Length; i++)
            {
                histTxtFile.Write(i.ToString() + "  ");
                histTxtFile.WriteLine(hist[i].ToString());
            }
            histTxtFile.Close();
            */

            return hist;
        }

        public void OpenImageFileDlog()
        {
            openFileDialog1.InitialDirectory = fpath;
            //   openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Filter = "PNG (*.PNG)|*.PNG|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    PathToImageForAnalysis = openFileDialog1.FileName;
                    LoadImageForAnalysis(PathToImageForAnalysis);
                    string SetngsPath = Path.Combine(Path.GetDirectoryName(PathToImageForAnalysis), "FluorControlSettings.bin");
                    if (File.Exists(SetngsPath))
                    {
                        LoadSettingsFrom(SetngsPath);
                    }
                    else
                        MessageBox.Show("No associated settings file found!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        public void LoadImageForAnalysis(string ImagePath)
        {
            if (!File.Exists(ImagePath))
            {
                MessageBox.Show("No valid file path provided!");
                return;
            }

            try
            {
                analyzeDlog.DarkImage = Image.FromFile(ImagePath, true); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }

            try
            {
                // sat image
                string SatImagePath = Path.Combine(Path.GetDirectoryName(ImagePath), "Plant_sat_1.png");
                if (File.Exists(SatImagePath))
                    analyzeDlog.SatImage = Image.FromFile(SatImagePath, true);
                DrawMask();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not find saturated image. Original error: " + ex.Message);
            }      
           
        }

        /* for 16 bit images - does not work
        private System.Drawing.Bitmap _bitmapFromSource(BitmapSource bitmapsource)
        {
            System.Drawing.Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                // from System.Media.BitmapImage to System.Drawing.Bitmap 
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
            }
            return bitmap;
        } 

         */

        public void DrawMask()
        {
            if (analyzeDlog.DarkImage == null)
                return;

            Image Img = new Bitmap(analyzeDlog.DarkImage);
            analyzeDlog.GetMaskParams();


           // Rectangle rect = MakeRectangle(MaskParams.Left, MaskParams.Top, MaskParams.Right, MaskParams.Bottom);
            Graphics MaskGraphics = Graphics.FromImage(Img);
            Point p1 = new Point(MaskParams.Left, 0);
            Point p2 = new Point(MaskParams.Right, 0);

            // Draw the selection rectangle.
            using (Pen select_pen = new Pen(Color.Red))
            {
                select_pen.DashStyle = DashStyle.Dash;
                select_pen.Width = PenWidth;
                //MaskGraphics.DrawRectangle(select_pen, rect);

                foreach (int row in MaskParams.rows)
                {
                    p1.Y = row;
                    p2.Y = row;
                    MaskGraphics.DrawLine(select_pen, p1, p2);
                }

                p1.Y = MaskParams.Top;
                p2.Y = MaskParams.Bottom;
                foreach (int col in MaskParams.columns)
                {
                    p1.X = col;
                    p2.X = col;
                    MaskGraphics.DrawLine(select_pen, p1, p2);
                }
            }
            analyzeDlog.SetImage(Img);
        }

        public void LoadLastDarkImageForAnalysis()
        {
            PathToImageForAnalysis = Path.Combine(fpath, "Plant_dark_1.png");
            LoadImageForAnalysis(PathToImageForAnalysis);
        }

        // from http://csharphelper.com/blog/2014/08/use-a-rubber-band-box-to-let-the-user-select-an-area-in-a-picture-in-c/
        private Rectangle MakeRectangle(int x0, int y0, int x1, int y1) 
        {
            return new Rectangle(
                Math.Min(x0, x1),
                Math.Min(y0, y1),
                Math.Abs(x0 - x1),
                Math.Abs(y0 - y1));
        }

        public void DoAnalysis()
        {
            Int32 selecWidth = MaskParams.columns[1] -MaskParams.columns[0];          
            Int32 selecHeight = MaskParams.rows[1] -MaskParams.rows[0];
            Rectangle selecRect = new Rectangle(0, 0,  selecWidth, selecHeight);
            Bitmap DarkImage = new Bitmap(analyzeDlog.DarkImage);
            Bitmap SatImage = new Bitmap(analyzeDlog.SatImage);
            FvFmImage = new Bitmap(analyzeDlog.SatImage);

            Bitmap croppedDark;
            Bitmap croppedSat;
            Bitmap croppedFvFm;

            Int32 GlobalThresh = 0;
            double MinThresh = settingsDlog.GetSettingsValue("Analysis", "Min Thresh part of Global");
                
            analyzeDlog.GetMaskParams();
            double[] ColorMapRange = analyzeDlog.GetColorMapMinMax();
            settingsDlog.SetSettingsValue("Analysis", "Mask Top", MaskParams.Top);
            settingsDlog.SetSettingsValue("Analysis", "Mask Bottom", MaskParams.Bottom);
            settingsDlog.SetSettingsValue("Analysis", "Mask Left", MaskParams.Left);
            settingsDlog.SetSettingsValue("Analysis", "Mask Right", MaskParams.Right);
            settingsDlog.SetSettingsValue("Analysis", "Mask #Rows", MaskParams.NoRows);
            settingsDlog.SetSettingsValue("Analysis", "Mask #Cols", MaskParams.NoColumns);
            settingsDlog.SetSettingsValue("Analysis", "ColorMap min", ColorMapRange[0]);
            settingsDlog.SetSettingsValue("Analysis", "ColorMap max", ColorMapRange[1]);
            SaveSettingsTo(Path.Combine(Path.GetDirectoryName(PathToImageForAnalysis), "FluorControlSettings.bin"));

            double expTimeDark = settingsDlog.GetSettingsValue("Dark Acquisition", "Exposure Time (ms)");
            double expTimeSat = settingsDlog.GetSettingsValue("Saturating Acquisition", "Exposure Time (ms)");
            double powerRatio = settingsDlog.GetSettingsValue("Autoexposure", "Sat to Dark power ratio");
            double wellSpacing = settingsDlog.GetSettingsValue("Analysis", "Well spacing (mm)");
            double mm_per_pix = wellSpacing / (MaskParams.columns[1] - MaskParams.columns[0]);
            GlobalThresh = OtsuThreshold(DarkImage);
            //MessageBox.Show("Global threshold = " + GlobalThresh.ToString());

            Int32[,] level = new Int32[MaskParams.NoRows, MaskParams.NoColumns];
            FvFm_perWell = new double[MaskParams.NoRows, MaskParams.NoColumns];
            PlantArea_perWell = new double[MaskParams.NoRows, MaskParams.NoColumns];

            for (int i = 0; i < MaskParams.NoRows; i++)
                for (int j = 0; j < MaskParams.NoColumns; j++)
                {
                    // cut a piece of the image
                    selecRect.X = MaskParams.columns[j];
                    selecRect.Y = MaskParams.rows[i];
                    croppedDark =  DarkImage.Clone(selecRect, DarkImage.PixelFormat);
                    int localThresh = OtsuThreshold(croppedDark);
                    if (localThresh > (GlobalThresh * MinThresh))
                        level[i, j] = localThresh;
                    else
                        level[i, j] = Convert.ToInt32(GlobalThresh * MinThresh);

                    croppedSat = SatImage.Clone(selecRect, SatImage.PixelFormat);
                    croppedFvFm = new Bitmap(croppedDark);

                    double sumSatValues = 0;
                    double sumDarkValues = 0;
                    Int32 NoPix = 0;

/*
                    for (int x = 0; x < croppedDark.Width; x++)
                    {
                        for (int y = 0; y < croppedDark.Height; y++)
                        {
                            Color pixelColor = croppedDark.GetPixel(x, y);
                            Int32 pixDark = pixelColor.G;
                            pixelColor = croppedSat.GetPixel(x, y);
                            Int32 pixSat = pixelColor.G;
                            if (pixDark >= level[i, j])
                            {
                                NoPix++;
                                sumSatValues += pixSat;
                                sumDarkValues += pixDark;
                            }                          
                        }
                    }
 */
                    // more efficient from https://msdn.microsoft.com/en-us/library/system.drawing.imaging.bitmapdata(v=vs.110).aspx

                   // Lock the bitmap's bits.  
                    Rectangle darkRect = new Rectangle(0, 0, croppedDark.Width, croppedDark.Height);
                    Rectangle satRect = new Rectangle(0, 0, croppedSat.Width, croppedSat.Height);
                    Rectangle fvfmRect = new Rectangle(0, 0, croppedSat.Width, croppedSat.Height);
                    System.Drawing.Imaging.BitmapData darkData = croppedDark.LockBits(darkRect, System.Drawing.Imaging.ImageLockMode.ReadWrite, croppedDark.PixelFormat);
                    System.Drawing.Imaging.BitmapData satData = croppedSat.LockBits(satRect, System.Drawing.Imaging.ImageLockMode.ReadWrite, croppedSat.PixelFormat);
                    System.Drawing.Imaging.BitmapData fvfmData = croppedFvFm.LockBits(fvfmRect, System.Drawing.Imaging.ImageLockMode.ReadWrite, croppedFvFm.PixelFormat);

                // Get the address of the first line.
                    IntPtr ptrDark = darkData.Scan0;
                    IntPtr ptrSat = satData.Scan0;
                    IntPtr ptrFvFm = fvfmData.Scan0;

                // Declare an array to hold the bytes of the bitmap.
                    int bytes  = Math.Abs(darkData.Stride) * croppedDark.Height;
                    byte[] pixDark = new byte[bytes];
                    byte[] pixSat = new byte[bytes];
                    byte[] pixFvFm = new byte[bytes];

                // Copy the RGB values into the array.
                    System.Runtime.InteropServices.Marshal.Copy(ptrDark, pixDark, 0, bytes);
                    System.Runtime.InteropServices.Marshal.Copy(ptrSat, pixSat, 0, bytes);
                // argb format 32bpp bitmap should look red.  ARGB is written in the reverse byte order, i.e. BGRA
                    for (int counter = 2 ; counter < pixDark.Length; counter += 4)
                    {
                        if (pixDark[counter] >= level[i, j])
                        {
                            NoPix++;
                            sumSatValues += pixSat[counter];
                            sumDarkValues += pixDark[counter];
                            double pixVal = (1 - (pixDark[counter] * powerRatio * expTimeSat) / (pixSat[counter] * expTimeDark));
                            pixVal = (pixVal > ColorMapRange[1]) ? ColorMapRange[1] : (pixVal < ColorMapRange[0]) ? ColorMapRange[0] : pixVal;

                            pixVal = 2 * (pixVal - ColorMapRange[0]) / (ColorMapRange[1] - ColorMapRange[0]) - 1; // rescale pixVal to [-1 to 1]
                            byte[] rgb = GetMatlabRgb(pixVal); // 
                            pixFvFm[counter] = rgb[0]; //Red Convert.ToByte(pixVal);
                            pixFvFm[counter-1] = rgb[1]; // Green
                            pixFvFm[counter-2] = rgb[2]; // Blue
                        }
                        pixFvFm[counter + 1] = 255; // set transparency (A) to nontransparent
                    }

                    // Copy the RGB values back to the bitmap
                    System.Runtime.InteropServices.Marshal.Copy(pixFvFm, 0, ptrFvFm, bytes);

  
                // Unlock the bits.
                    croppedFvFm.UnlockBits(fvfmData);
                    croppedDark.UnlockBits(darkData);
                    croppedSat.UnlockBits(satData);

                    using (Graphics grD = Graphics.FromImage(FvFmImage))
                    {
                        grD.DrawImage(croppedFvFm, selecRect.X, selecRect.Y);
                    }


                    FvFm_perWell[i, j] = 1 - (sumDarkValues*powerRatio*expTimeSat) / (sumSatValues*expTimeDark);
                    PlantArea_perWell[i, j] = NoPix * mm_per_pix * mm_per_pix;
                }
            OutputResults(Path.GetDirectoryName(PathToImageForAnalysis));
            analyzeDlog.SetImage(FvFmImage);
            FvFmImage.Save(Path.GetDirectoryName(PathToImageForAnalysis) + "//FvFmImage.gif", System.Drawing.Imaging.ImageFormat.Gif);
           // MessageBox.Show("Analysis done.");
        }

  
        // jetmap https://stackoverflow.com/questions/7706339/grayscale-to-red-green-blue-matlab-jet-color-scale

        public byte[] GetMatlabRgb(double ordinal)
        {
            byte[] triplet = new byte[3];
            triplet[0] = (ordinal < 0.0) ? (byte)0 : (ordinal >= 0.5) ? (byte)255 : (byte)(ordinal / 0.5 * 255);
            triplet[1] = (ordinal < -0.5) ? (byte)((ordinal + 1) / 0.5 * 255) : (ordinal > 0.5) ? (byte)(255 - ((ordinal - 0.5) / 0.5 * 255)) : (byte)255;
            triplet[2] = (ordinal > 0.0) ? (byte)0 : (ordinal <= -0.5) ? (byte)255 : (byte)(ordinal * -1.0 / 0.5 * 255);
            return triplet;
        }

        public Int32[] DoHistogram8bit(Bitmap bmp)
        {   
            Int32[] hist = new Int32[256];
/*
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    Int32 pixVal = pixelColor.G;
                    hist[pixVal]++;
                }
            }
 */ 
  
            //MessageBox.Show("Max value = " + max.ToString());

            // more efficient looping through the image https://stackoverflow.com/questions/9479331/c-sharp-low-performance-in-for-loop-while-searching-pixels-in-image
            // original at https://msdn.microsoft.com/en-us/library/system.drawing.imaging.bitmapdata(v=vs.110).aspx
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            // This code is specific to an argb bitmap with 32 bits per pixels.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            // Calculate histogram.  
            for (int counter = 2; counter < rgbValues.Length; counter += 4)
            {
                Int32 pixVal = rgbValues[counter];
                hist[pixVal]++;
            }
  
            return hist;
        }

        public Int32 OtsuThreshold(Bitmap bmp)
        {
            Int32[] hist = DoHistogram8bit(bmp);
            Int32 bitDepth = hist.Length;
            Int32 thresh = 0;
            double q1 = 0;
            double q2 = 0;
            double mu1 = 0;
            double mu2 = 0;
            double totsum = 0;
            double cumsum = 0;
            Int32 PixNum = bmp.Width * bmp.Height;
            double maxVariance = 0;
            double sigBsq = 0;

            for (int i = 0; i < bitDepth; i++)
            {
                totsum += i * hist[i];
            }


            for (int t = 0; t < bitDepth; t++)
            {
                q1 += hist[t];
                q2 = PixNum - q1;

                cumsum += t * hist[t];
                mu1 = cumsum / q1;
                mu2 = (totsum - cumsum) / q2;

                sigBsq = q1 * q2 * (mu1 - mu2) * (mu1 - mu2);

                if (sigBsq > maxVariance)
                {
                    thresh = t;
                    maxVariance = sigBsq;
                }
            }

            return thresh;
        }

        public void OutputResults(string pathToOutput)
        {
            // save to text file
            string rowNames = "ABCDEFGHIJKLMNOP";
            System.IO.StreamWriter Txtfile = new System.IO.StreamWriter(pathToOutput + "\\FvFmData.csv ");
            int well_ID = 0;
            Txtfile.WriteLine("Plate_ID,  ROW,  COLUMN, WELL_ID, TP_ID, Area, FvFm, Excluded, Remarks");
            for (int i = 0; i < FvFm_perWell.GetLength(0); i++)
            {
                
                for (int j = 0; j < FvFm_perWell.GetLength(1); j++)
                {
                    string lineToWrite = Path.GetFileName(pathToOutput) + "  ,   " + rowNames[i] + " ,  ";
                    well_ID++;
                    lineToWrite = lineToWrite + (j+1).ToString() + "  ,  "
                          + well_ID.ToString() +  " ,   ,  " + PlantArea_perWell[i, j].ToString() 
                          + "  ,  " + FvFm_perWell[i, j].ToString() + "  ,   ,   ";
                    Txtfile.WriteLine(lineToWrite);
                }
                
            }

            Txtfile.Close();
 
        }

        public void QuitProc()
        {
            Camera.Exit();
            if (serialPort.IsOpen) serialPort.Close();
        }
    }
}
