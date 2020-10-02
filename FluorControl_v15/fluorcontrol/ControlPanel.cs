using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace FluorControl
{
    public partial class ControlPanelClass : Form
    {
        public ImplementationClass Implementation { get; set; }

        public ControlPanelClass()
        {
            InitializeComponent();
        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {
            ImageList imageList1 = new ImageList();
            //imageList1.Images.Add(Image.FromFile("C:\\Users\\olegk\\Documents\\Visual Studio 2012\\Projects\\FluorControl_v5\\FluorControl_v5\\video_start.png"));
            //imageList1.Images.Add(Image.FromFile("C:\\Users\\olegk\\Documents\\Visual Studio 2012\\Projects\\FluorControl_v5\\FluorControl_v5\\video_pause.png"));

            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream("FluorControl_v5.video_start.png");
            Bitmap image = new Bitmap(myStream);
            imageList1.Images.Add(image);
            myStream = myAssembly.GetManifestResourceStream("FluorControl_v5.video_pause.png");
            image = new Bitmap(myStream);
            imageList1.Images.Add(image);
            imageList1.ImageSize = btn_LiveFreeze.Size;
            btn_LiveFreeze.ImageList = imageList1;
            btn_LiveFreeze.ImageIndex = 0;
            btn_LiveFreeze.Tag = "freeze";

            ImageList imageList2 = new ImageList();
            myStream = myAssembly.GetManifestResourceStream("FluorControl_v5.SwitchOff.bmp");
            image = new Bitmap(myStream);
            imageList2.Images.Add(image);
            myStream = myAssembly.GetManifestResourceStream("FluorControl_v5.SwitchOn.bmp");
            image = new Bitmap(myStream);
            imageList2.Images.Add(image);
            imageList2.ImageSize = btn_LEDonoff.Size;
            btn_LEDonoff.ImageList = imageList2;
            btn_LEDonoff.ImageIndex = 0;
            btn_LEDonoff.Tag = "off";

            ImageList imageList3 = new ImageList();
            myStream = myAssembly.GetManifestResourceStream("FluorControl_v5.snap_image.png");
            image = new Bitmap(myStream);
            imageList3.Images.Add(image);
            imageList3.ImageSize = btn_Snap.Size;
            btn_Snap.ImageList = imageList3;
            btn_Snap.ImageIndex = 0;
        }

        private void ControlPanelClass_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void numup_PixelClock_ValueChanged(object sender, EventArgs e)
        {
            Implementation.SetPixelClock(Convert.ToInt32(numup_PixelClock.Value));
            string frameRateString;
            uc480.Types.Range<double> fpsRange;
            frameRateString = Implementation.GetFramerateRange(out fpsRange);
            lbl_FrameRate.Text = "Frame Rate\r\n" + frameRateString + " fps";
            numup_FrameRate.Maximum = Convert.ToDecimal(fpsRange.Maximum);
            numup_FrameRate.Minimum = Convert.ToDecimal(fpsRange.Minimum);
            numup_FrameRate.Increment = Convert.ToDecimal(fpsRange.Increment);
        }

        public void set_PixelClockLabelAndLimits(string labelText, uc480.Types.Range<int> pixelClockRange)
        {
            lbl_PIxClock.Text = labelText;
            numup_PixelClock.Maximum = pixelClockRange.Maximum;
            numup_PixelClock.Minimum = pixelClockRange.Minimum;
            numup_PixelClock.Increment = pixelClockRange.Increment;
            numup_FrameRate.Value = 5;
        }

        private void numup_FrameRate_ValueChanged(object sender, EventArgs e)
        {
            Implementation.SetFrameRate(Convert.ToDouble(numup_FrameRate.Value));
            string ExpRangeString;
            uc480.Types.Range<double> ExpRange;
            ExpRangeString = Implementation.GetExposureRange(out ExpRange);
            lbl_ExpTime.Text = "Exposure Time\r\n" + ExpRangeString + " ms";
            numup_ExpTime.Maximum = Convert.ToDecimal(ExpRange.Maximum);
            numup_ExpTime.Minimum = Convert.ToDecimal(ExpRange.Minimum);
            numup_ExpTime.Increment = Convert.ToDecimal(ExpRange.Increment);
        }

        public void SetFrameRate(double frRateIn)
        {
            decimal frRate = Convert.ToDecimal(frRateIn);
            if (frRate > numup_FrameRate.Maximum)
                frRate = numup_FrameRate.Maximum;
            if (frRate < numup_FrameRate.Minimum)
                frRate = numup_FrameRate.Minimum;
            numup_FrameRate.Value = frRate; 
            numup_FrameRate_ValueChanged(null, null);
        }

        public void SetExposureTime(double ExpTimeIn)
        {
            decimal ExpTime = Convert.ToDecimal(ExpTimeIn);
            if (ExpTime > numup_ExpTime.Maximum)
                ExpTime = numup_ExpTime.Maximum;
            if (ExpTime < numup_ExpTime.Minimum)
                ExpTime = numup_ExpTime.Minimum;
 
            numup_ExpTime.Value = ExpTime;
            numup_ExpTime_ValueChanged(null, null);
        }

        private void numup_ExpTime_ValueChanged(object sender, EventArgs e)
        {
            Implementation.SetExposureTime(Convert.ToDouble(numup_ExpTime.Value));
        }

        private void btn_LEDonoff_Click(object sender, EventArgs e)
        {
            if (btn_LEDonoff.Tag.Equals("off"))
            {
                btn_LEDonoff.Tag = "on";
                btn_LEDonoff.ImageIndex = 1;

                Int32 ledPower = Convert.ToInt32(numup_LEDpower.Value);
                Int32 ledFreq = Convert.ToInt32(numup_LEDfreq.Value);
                Implementation.switchLED(ledPower, ledFreq);
            }
            else
            {
                btn_LEDonoff.Tag = "off";
                btn_LEDonoff.ImageIndex = 0;
                Implementation.switchLEDoff();
            }

        }

        private void btn_LiveFreeze_Click(object sender, EventArgs e)
        {
            if (btn_LiveFreeze.Tag.Equals("freeze"))
            {
                if (Implementation.StartLive(Convert.ToInt32(numup_PixelClock.Value),
                                             Convert.ToDouble(numup_FrameRate.Value),
                                             Convert.ToDouble(numup_ExpTime.Value)))
                {
                    btn_LiveFreeze.ImageIndex = 1;
                    btn_LiveFreeze.Tag = "live"; 
                }
            }
            else
            {
                if (Implementation.FreezeCamera())
                {
                    btn_LiveFreeze.ImageIndex = 0;
                    btn_LiveFreeze.Tag = "freeze";
                }
            }
        }

        private void btn_Snap_Click(object sender, EventArgs e)
        {
            Implementation.SnapImage(Convert.ToInt32(numup_PixelClock.Value),
                                             Convert.ToDouble(numup_FrameRate.Value),
                                             Convert.ToDouble(numup_ExpTime.Value));
        }

        private void numup_LEDpower_ValueChanged(object sender, EventArgs e)
        {
            //change power if LED is on
            if (btn_LEDonoff.Tag.Equals("on"))
            {
                Int32 ledPower = Convert.ToInt32(numup_LEDpower.Value);
                Int32 ledFreq = Convert.ToInt32(numup_LEDfreq.Value);
                Implementation.switchLED(ledPower, ledFreq);
            }
        }

    }
}
