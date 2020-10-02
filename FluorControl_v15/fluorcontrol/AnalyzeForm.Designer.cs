namespace FluorControl
{
    partial class AnalyzeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picbxImageAnalyze = new System.Windows.Forms.PictureBox();
            this.btn_Done = new System.Windows.Forms.Button();
            this.numup_Top = new System.Windows.Forms.NumericUpDown();
            this.numup_Bottom = new System.Windows.Forms.NumericUpDown();
            this.numup_Left = new System.Windows.Forms.NumericUpDown();
            this.numup_Right = new System.Windows.Forms.NumericUpDown();
            this.numup_Rows = new System.Windows.Forms.NumericUpDown();
            this.numup_Columns = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_getFvFm = new System.Windows.Forms.Button();
            this.numup_ColorMapMin = new System.Windows.Forms.NumericUpDown();
            this.numup_ColorMapMax = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picbxImageAnalyze)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_Top)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_Bottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_Left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_Rows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_Columns)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numup_ColorMapMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_ColorMapMax)).BeginInit();
            this.SuspendLayout();
            // 
            // picbxImageAnalyze
            // 
            this.picbxImageAnalyze.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.picbxImageAnalyze.Location = new System.Drawing.Point(12, 30);
            this.picbxImageAnalyze.Name = "picbxImageAnalyze";
            this.picbxImageAnalyze.Size = new System.Drawing.Size(640, 525);
            this.picbxImageAnalyze.TabIndex = 2;
            this.picbxImageAnalyze.TabStop = false;
            // 
            // btn_Done
            // 
            this.btn_Done.BackColor = System.Drawing.Color.Red;
            this.btn_Done.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_Done.ForeColor = System.Drawing.Color.White;
            this.btn_Done.Location = new System.Drawing.Point(751, 508);
            this.btn_Done.Name = "btn_Done";
            this.btn_Done.Size = new System.Drawing.Size(90, 47);
            this.btn_Done.TabIndex = 3;
            this.btn_Done.Text = "Done";
            this.btn_Done.UseVisualStyleBackColor = false;
            this.btn_Done.Click += new System.EventHandler(this.btn_Done_Click);
            // 
            // numup_Top
            // 
            this.numup_Top.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_Top.Location = new System.Drawing.Point(766, 48);
            this.numup_Top.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numup_Top.Name = "numup_Top";
            this.numup_Top.Size = new System.Drawing.Size(75, 26);
            this.numup_Top.TabIndex = 4;
            this.numup_Top.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numup_Top.ValueChanged += new System.EventHandler(this.numup_Top_ValueChanged);
            // 
            // numup_Bottom
            // 
            this.numup_Bottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_Bottom.Location = new System.Drawing.Point(766, 93);
            this.numup_Bottom.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numup_Bottom.Name = "numup_Bottom";
            this.numup_Bottom.Size = new System.Drawing.Size(75, 26);
            this.numup_Bottom.TabIndex = 5;
            this.numup_Bottom.Value = new decimal(new int[] {
            1014,
            0,
            0,
            0});
            this.numup_Bottom.ValueChanged += new System.EventHandler(this.numup_Bottom_ValueChanged);
            // 
            // numup_Left
            // 
            this.numup_Left.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_Left.Location = new System.Drawing.Point(766, 138);
            this.numup_Left.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numup_Left.Name = "numup_Left";
            this.numup_Left.Size = new System.Drawing.Size(75, 26);
            this.numup_Left.TabIndex = 6;
            this.numup_Left.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numup_Left.ValueChanged += new System.EventHandler(this.numup_Left_ValueChanged);
            // 
            // numup_Right
            // 
            this.numup_Right.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_Right.Location = new System.Drawing.Point(766, 186);
            this.numup_Right.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numup_Right.Name = "numup_Right";
            this.numup_Right.Size = new System.Drawing.Size(75, 26);
            this.numup_Right.TabIndex = 7;
            this.numup_Right.Value = new decimal(new int[] {
            1270,
            0,
            0,
            0});
            this.numup_Right.ValueChanged += new System.EventHandler(this.numup_Right_ValueChanged);
            // 
            // numup_Rows
            // 
            this.numup_Rows.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_Rows.Location = new System.Drawing.Point(766, 234);
            this.numup_Rows.Name = "numup_Rows";
            this.numup_Rows.Size = new System.Drawing.Size(75, 26);
            this.numup_Rows.TabIndex = 8;
            this.numup_Rows.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // numup_Columns
            // 
            this.numup_Columns.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_Columns.Location = new System.Drawing.Point(766, 282);
            this.numup_Columns.Name = "numup_Columns";
            this.numup_Columns.Size = new System.Drawing.Size(75, 26);
            this.numup_Columns.TabIndex = 9;
            this.numup_Columns.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(685, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Top:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(685, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Bottom:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(685, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Left:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(685, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Right:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(685, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "#Rows:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(685, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "#Columns:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label7.Location = new System.Drawing.Point(685, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Mask parameters:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(853, 29);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 25);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.loadImageToolStripMenuItem.Text = "Load dark_1 Image";
            this.loadImageToolStripMenuItem.Click += new System.EventHandler(this.loadImageToolStripMenuItem_Click);
            // 
            // btn_getFvFm
            // 
            this.btn_getFvFm.BackColor = System.Drawing.Color.ForestGreen;
            this.btn_getFvFm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_getFvFm.ForeColor = System.Drawing.Color.White;
            this.btn_getFvFm.Location = new System.Drawing.Point(751, 446);
            this.btn_getFvFm.Name = "btn_getFvFm";
            this.btn_getFvFm.Size = new System.Drawing.Size(90, 47);
            this.btn_getFvFm.TabIndex = 18;
            this.btn_getFvFm.Text = "GetFvFm";
            this.btn_getFvFm.UseVisualStyleBackColor = false;
            this.btn_getFvFm.Click += new System.EventHandler(this.btn_getFvFm_Click);
            // 
            // numup_ColorMapMin
            // 
            this.numup_ColorMapMin.DecimalPlaces = 3;
            this.numup_ColorMapMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_ColorMapMin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numup_ColorMapMin.Location = new System.Drawing.Point(766, 364);
            this.numup_ColorMapMin.Name = "numup_ColorMapMin";
            this.numup_ColorMapMin.Size = new System.Drawing.Size(75, 26);
            this.numup_ColorMapMin.TabIndex = 19;
            // 
            // numup_ColorMapMax
            // 
            this.numup_ColorMapMax.DecimalPlaces = 3;
            this.numup_ColorMapMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_ColorMapMax.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numup_ColorMapMax.Location = new System.Drawing.Point(766, 396);
            this.numup_ColorMapMax.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numup_ColorMapMax.Name = "numup_ColorMapMax";
            this.numup_ColorMapMax.Size = new System.Drawing.Size(75, 26);
            this.numup_ColorMapMax.TabIndex = 20;
            this.numup_ColorMapMax.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label8.Location = new System.Drawing.Point(685, 341);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 20);
            this.label8.TabIndex = 21;
            this.label8.Text = "ColorMap:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label9.Location = new System.Drawing.Point(726, 370);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 20);
            this.label9.TabIndex = 22;
            this.label9.Text = "min";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label10.Location = new System.Drawing.Point(722, 396);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 20);
            this.label10.TabIndex = 23;
            this.label10.Text = "max";
            // 
            // AnalyzeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 561);
            this.ControlBox = false;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numup_ColorMapMax);
            this.Controls.Add(this.numup_ColorMapMin);
            this.Controls.Add(this.btn_getFvFm);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numup_Columns);
            this.Controls.Add(this.numup_Rows);
            this.Controls.Add(this.numup_Right);
            this.Controls.Add(this.numup_Left);
            this.Controls.Add(this.numup_Bottom);
            this.Controls.Add(this.numup_Top);
            this.Controls.Add(this.btn_Done);
            this.Controls.Add(this.picbxImageAnalyze);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AnalyzeForm";
            this.Text = "AnalyzeForm";
            this.Load += new System.EventHandler(this.AnalyzeForm_Load);
            this.VisibleChanged += new System.EventHandler(this.AnalyzeForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picbxImageAnalyze)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_Top)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_Bottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_Left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_Rows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_Columns)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numup_ColorMapMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_ColorMapMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picbxImageAnalyze;
        private System.Windows.Forms.Button btn_Done;
        private System.Windows.Forms.NumericUpDown numup_Top;
        private System.Windows.Forms.NumericUpDown numup_Bottom;
        private System.Windows.Forms.NumericUpDown numup_Left;
        private System.Windows.Forms.NumericUpDown numup_Right;
        private System.Windows.Forms.NumericUpDown numup_Rows;
        private System.Windows.Forms.NumericUpDown numup_Columns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
        private System.Windows.Forms.Button btn_getFvFm;
        private System.Windows.Forms.NumericUpDown numup_ColorMapMin;
        private System.Windows.Forms.NumericUpDown numup_ColorMapMax;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}