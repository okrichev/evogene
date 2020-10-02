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
    public partial class SampleIDForm : Form
    {
        public ImplementationClass Implementation { get; set; }
        public SampleIDForm()
        {
            InitializeComponent();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (txtbox_SampleID.Text.Equals(""))
                MessageBox.Show("Enter Sample ID");
            else
            {
                this.Hide();
                Implementation.DoAllAcquisition();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SampleIDForm_Shown(object sender, EventArgs e)
        {
            txtbox_SampleID.Text = "";
            txtbox_SampleID.Focus();
        }

        public string GetSampleID()
        {
            return txtbox_SampleID.Text;
        }
    }
}
