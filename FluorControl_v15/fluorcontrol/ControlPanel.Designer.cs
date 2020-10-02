namespace FluorControl
{
    partial class ControlPanelClass
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
            this.numup_LEDfreq = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numup_LEDpower = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_LEDonoff = new System.Windows.Forms.Button();
            this.numup_PixelClock = new System.Windows.Forms.NumericUpDown();
            this.numup_FrameRate = new System.Windows.Forms.NumericUpDown();
            this.numup_ExpTime = new System.Windows.Forms.NumericUpDown();
            this.lbl_PIxClock = new System.Windows.Forms.Label();
            this.lbl_FrameRate = new System.Windows.Forms.Label();
            this.lbl_ExpTime = new System.Windows.Forms.Label();
            this.btn_LiveFreeze = new System.Windows.Forms.Button();
            this.btn_Snap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numup_LEDfreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_LEDpower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_PixelClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_FrameRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_ExpTime)).BeginInit();
            this.SuspendLayout();
            // 
            // numup_LEDfreq
            // 
            this.numup_LEDfreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_LEDfreq.Location = new System.Drawing.Point(35, 35);
            this.numup_LEDfreq.Maximum = new decimal(new int[] {
            2000000,
            0,
            0,
            0});
            this.numup_LEDfreq.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numup_LEDfreq.Name = "numup_LEDfreq";
            this.numup_LEDfreq.Size = new System.Drawing.Size(120, 26);
            this.numup_LEDfreq.TabIndex = 0;
            this.numup_LEDfreq.Value = new decimal(new int[] {
            32000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(31, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "LED frequency";
            // 
            // numup_LEDpower
            // 
            this.numup_LEDpower.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_LEDpower.Location = new System.Drawing.Point(35, 100);
            this.numup_LEDpower.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numup_LEDpower.Name = "numup_LEDpower";
            this.numup_LEDpower.Size = new System.Drawing.Size(120, 26);
            this.numup_LEDpower.TabIndex = 2;
            this.numup_LEDpower.ValueChanged += new System.EventHandler(this.numup_LEDpower_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(35, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "LED power";
            // 
            // btn_LEDonoff
            // 
            this.btn_LEDonoff.Location = new System.Drawing.Point(186, 35);
            this.btn_LEDonoff.Name = "btn_LEDonoff";
            this.btn_LEDonoff.Size = new System.Drawing.Size(56, 91);
            this.btn_LEDonoff.TabIndex = 4;
            this.btn_LEDonoff.UseVisualStyleBackColor = true;
            this.btn_LEDonoff.Click += new System.EventHandler(this.btn_LEDonoff_Click);
            // 
            // numup_PixelClock
            // 
            this.numup_PixelClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_PixelClock.Location = new System.Drawing.Point(39, 203);
            this.numup_PixelClock.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numup_PixelClock.Name = "numup_PixelClock";
            this.numup_PixelClock.Size = new System.Drawing.Size(120, 26);
            this.numup_PixelClock.TabIndex = 5;
            this.numup_PixelClock.ValueChanged += new System.EventHandler(this.numup_PixelClock_ValueChanged);
            // 
            // numup_FrameRate
            // 
            this.numup_FrameRate.DecimalPlaces = 3;
            this.numup_FrameRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_FrameRate.Location = new System.Drawing.Point(201, 203);
            this.numup_FrameRate.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numup_FrameRate.Name = "numup_FrameRate";
            this.numup_FrameRate.Size = new System.Drawing.Size(120, 26);
            this.numup_FrameRate.TabIndex = 6;
            this.numup_FrameRate.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numup_FrameRate.ValueChanged += new System.EventHandler(this.numup_FrameRate_ValueChanged);
            // 
            // numup_ExpTime
            // 
            this.numup_ExpTime.DecimalPlaces = 3;
            this.numup_ExpTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numup_ExpTime.Location = new System.Drawing.Point(372, 203);
            this.numup_ExpTime.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numup_ExpTime.Name = "numup_ExpTime";
            this.numup_ExpTime.Size = new System.Drawing.Size(120, 26);
            this.numup_ExpTime.TabIndex = 7;
            this.numup_ExpTime.ValueChanged += new System.EventHandler(this.numup_ExpTime_ValueChanged);
            // 
            // lbl_PIxClock
            // 
            this.lbl_PIxClock.AutoSize = true;
            this.lbl_PIxClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lbl_PIxClock.Location = new System.Drawing.Point(53, 157);
            this.lbl_PIxClock.Name = "lbl_PIxClock";
            this.lbl_PIxClock.Size = new System.Drawing.Size(84, 20);
            this.lbl_PIxClock.TabIndex = 8;
            this.lbl_PIxClock.Text = "Pixel Clock";
            // 
            // lbl_FrameRate
            // 
            this.lbl_FrameRate.AutoSize = true;
            this.lbl_FrameRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lbl_FrameRate.Location = new System.Drawing.Point(197, 157);
            this.lbl_FrameRate.Name = "lbl_FrameRate";
            this.lbl_FrameRate.Size = new System.Drawing.Size(94, 20);
            this.lbl_FrameRate.TabIndex = 9;
            this.lbl_FrameRate.Text = "Frame Rate";
            // 
            // lbl_ExpTime
            // 
            this.lbl_ExpTime.AutoSize = true;
            this.lbl_ExpTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lbl_ExpTime.Location = new System.Drawing.Point(368, 157);
            this.lbl_ExpTime.Name = "lbl_ExpTime";
            this.lbl_ExpTime.Size = new System.Drawing.Size(110, 20);
            this.lbl_ExpTime.TabIndex = 10;
            this.lbl_ExpTime.Text = "Exposure time";
            // 
            // btn_LiveFreeze
            // 
            this.btn_LiveFreeze.Location = new System.Drawing.Point(90, 268);
            this.btn_LiveFreeze.Name = "btn_LiveFreeze";
            this.btn_LiveFreeze.Size = new System.Drawing.Size(91, 74);
            this.btn_LiveFreeze.TabIndex = 11;
            this.btn_LiveFreeze.UseVisualStyleBackColor = true;
            this.btn_LiveFreeze.Click += new System.EventHandler(this.btn_LiveFreeze_Click);
            // 
            // btn_Snap
            // 
            this.btn_Snap.Location = new System.Drawing.Point(323, 268);
            this.btn_Snap.Name = "btn_Snap";
            this.btn_Snap.Size = new System.Drawing.Size(91, 74);
            this.btn_Snap.TabIndex = 12;
            this.btn_Snap.UseVisualStyleBackColor = true;
            this.btn_Snap.Click += new System.EventHandler(this.btn_Snap_Click);
            // 
            // ControlPanelClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 363);
            this.Controls.Add(this.btn_Snap);
            this.Controls.Add(this.btn_LiveFreeze);
            this.Controls.Add(this.lbl_ExpTime);
            this.Controls.Add(this.lbl_FrameRate);
            this.Controls.Add(this.lbl_PIxClock);
            this.Controls.Add(this.numup_ExpTime);
            this.Controls.Add(this.numup_FrameRate);
            this.Controls.Add(this.numup_PixelClock);
            this.Controls.Add(this.btn_LEDonoff);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numup_LEDpower);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numup_LEDfreq);
            this.Location = new System.Drawing.Point(700, 200);
            this.Name = "ControlPanelClass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlPanelClass_FormClosing);
            this.Load += new System.EventHandler(this.ControlPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numup_LEDfreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_LEDpower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_PixelClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_FrameRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numup_ExpTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numup_LEDfreq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numup_LEDpower;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_LEDonoff;
        private System.Windows.Forms.NumericUpDown numup_PixelClock;
        private System.Windows.Forms.NumericUpDown numup_FrameRate;
        private System.Windows.Forms.NumericUpDown numup_ExpTime;
        private System.Windows.Forms.Label lbl_PIxClock;
        private System.Windows.Forms.Label lbl_FrameRate;
        private System.Windows.Forms.Label lbl_ExpTime;
        private System.Windows.Forms.Button btn_LiveFreeze;
        private System.Windows.Forms.Button btn_Snap;
    }
}