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
    public partial class AnalyzeForm : Form
    {
        public ImplementationClass Implementation { get; set; }
        public Image DarkImage { get; set; }
        public Image SatImage { get; set; }

        public AnalyzeForm()
        {
            InitializeComponent();
        }

        private void AnalyzeForm_Load(object sender, EventArgs e)
        {
            picbxImageAnalyze.SizeMode = PictureBoxSizeMode.Zoom;
        }

  
        private void btn_Done_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AnalyzeForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                Implementation.InitAnalyzeForm();
                Implementation.LoadLastDarkImageForAnalysis(); 
            }
        }

        public void SetImage(Image Img)
        {
            picbxImageAnalyze.Image = Img;
        }

        public void GetMaskParams()
        {
            MaskParamStruc MaskParams;
            MaskParams.Top = Convert.ToInt32(numup_Top.Value);
            MaskParams.Bottom = Convert.ToInt32(numup_Bottom.Value);
            MaskParams.Left = Convert.ToInt32(numup_Left.Value);
            MaskParams.Right = Convert.ToInt32(numup_Right.Value);
            MaskParams.NoRows = Convert.ToInt32(numup_Rows.Value);
            MaskParams.NoColumns = Convert.ToInt32(numup_Columns.Value);
            MaskParams.rows = new int[MaskParams.NoRows + 1];
            MaskParams.columns = new int[MaskParams.NoColumns + 1];

            for (int i = 0; i < MaskParams.rows.Length; i++)
            {
                MaskParams.rows[i] = Convert.ToInt32(Convert.ToDouble(MaskParams.Top) + Convert.ToDouble(i) * (Convert.ToDouble(MaskParams.Bottom) - Convert.ToDouble(MaskParams.Top)) / Convert.ToDouble(MaskParams.NoRows));
            }

            for (int i = 0; i < MaskParams.columns.Length; i++)
            {
                MaskParams.columns[i] = Convert.ToInt32(Convert.ToDouble(MaskParams.Left) + Convert.ToDouble(i) * (Convert.ToDouble(MaskParams.Right) - Convert.ToDouble(MaskParams.Left)) / Convert.ToDouble(MaskParams.NoColumns));
            }

            Implementation.MaskParams = MaskParams;
        }

        private void numup_Top_ValueChanged(object sender, EventArgs e)
        {
            Implementation.DrawMask();
            picbxImageAnalyze.Refresh();
        }

        private void numup_Bottom_ValueChanged(object sender, EventArgs e)
        {
            Implementation.DrawMask();
            picbxImageAnalyze.Refresh();
        }

        private void numup_Left_ValueChanged(object sender, EventArgs e)
        {
            Implementation.DrawMask();
            picbxImageAnalyze.Refresh();
        }

        private void numup_Right_ValueChanged(object sender, EventArgs e)
        {
            Implementation.DrawMask();
            picbxImageAnalyze.Refresh();
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Implementation.OpenImageFileDlog();
        }

        public void Set_MaskNumups(string name, double Value1)
        {
            Int32 Value = Convert.ToInt32(Value1);
            switch (name)
            {
                case "Top" :
                    numup_Top.Value = Value;
                    break;
                case "Bottom":
                    numup_Bottom.Value = Value;
                    break;
                case "Left":
                    numup_Left.Value = Value;
                    break;
                case "Right":
                    numup_Right.Value = Value;
                    break;
                case "NoRows":
                    numup_Rows.Value = Value;
                    break;
                case "NoCols":
                    numup_Columns.Value = Value;
                    break;
                case "ColorMapMin":
                    numup_ColorMapMin.Value = Convert.ToDecimal(Value1);
                    break;
                case "ColorMapMax":
                    numup_ColorMapMax.Value = Convert.ToDecimal(Value1);
                    break;

                default:
                    break;
            }
        }

        public double[] GetColorMapMinMax()
        {
            double[] CM = {Convert.ToDouble(numup_ColorMapMin.Value), Convert.ToDouble(numup_ColorMapMax.Value)};
            return CM;
        }

        private void btn_getFvFm_Click(object sender, EventArgs e)
        {
            Implementation.DoAnalysis();
        }
    }
}
