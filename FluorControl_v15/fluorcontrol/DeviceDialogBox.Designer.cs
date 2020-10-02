namespace FluorControl
{
    partial class DeviceDialogBox
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
            this.btnCheckArduino = new System.Windows.Forms.Button();
            this.btnCheckCamera = new System.Windows.Forms.Button();
            this.cboSerialPort = new System.Windows.Forms.ComboBox();
            this.rtxbArduinoName = new System.Windows.Forms.RichTextBox();
            this.rtxb_SerialTerminal = new System.Windows.Forms.RichTextBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rtxb_SendCommand = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lstbx_Camera = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnCheckArduino
            // 
            this.btnCheckArduino.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnCheckArduino.Location = new System.Drawing.Point(28, 30);
            this.btnCheckArduino.Name = "btnCheckArduino";
            this.btnCheckArduino.Size = new System.Drawing.Size(146, 47);
            this.btnCheckArduino.TabIndex = 0;
            this.btnCheckArduino.Text = "Check Arduino:";
            this.btnCheckArduino.UseVisualStyleBackColor = true;
            this.btnCheckArduino.Click += new System.EventHandler(this.btnCheckArduino_Click);
            // 
            // btnCheckCamera
            // 
            this.btnCheckCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnCheckCamera.Location = new System.Drawing.Point(28, 262);
            this.btnCheckCamera.Name = "btnCheckCamera";
            this.btnCheckCamera.Size = new System.Drawing.Size(146, 47);
            this.btnCheckCamera.TabIndex = 1;
            this.btnCheckCamera.Text = "Check Camera:";
            this.btnCheckCamera.UseVisualStyleBackColor = true;
            this.btnCheckCamera.Click += new System.EventHandler(this.btnCheckCamera_Click);
            // 
            // cboSerialPort
            // 
            this.cboSerialPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cboSerialPort.FormattingEnabled = true;
            this.cboSerialPort.Location = new System.Drawing.Point(212, 40);
            this.cboSerialPort.Name = "cboSerialPort";
            this.cboSerialPort.Size = new System.Drawing.Size(121, 28);
            this.cboSerialPort.TabIndex = 2;
            // 
            // rtxbArduinoName
            // 
            this.rtxbArduinoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rtxbArduinoName.Location = new System.Drawing.Point(395, 40);
            this.rtxbArduinoName.Name = "rtxbArduinoName";
            this.rtxbArduinoName.Size = new System.Drawing.Size(117, 28);
            this.rtxbArduinoName.TabIndex = 3;
            this.rtxbArduinoName.Text = "";
            // 
            // rtxb_SerialTerminal
            // 
            this.rtxb_SerialTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rtxb_SerialTerminal.Location = new System.Drawing.Point(212, 103);
            this.rtxb_SerialTerminal.Name = "rtxb_SerialTerminal";
            this.rtxb_SerialTerminal.Size = new System.Drawing.Size(300, 133);
            this.rtxb_SerialTerminal.TabIndex = 4;
            this.rtxb_SerialTerminal.Text = "";
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_OK.Location = new System.Drawing.Point(441, 345);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(71, 51);
            this.btn_OK.TabIndex = 5;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(212, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Arduino Output";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(212, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Serial Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(392, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Arduino Name:";
            // 
            // rtxb_SendCommand
            // 
            this.rtxb_SendCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rtxb_SendCommand.Location = new System.Drawing.Point(28, 103);
            this.rtxb_SendCommand.Name = "rtxb_SendCommand";
            this.rtxb_SendCommand.Size = new System.Drawing.Size(146, 53);
            this.rtxb_SendCommand.TabIndex = 9;
            this.rtxb_SendCommand.Text = "";
            this.rtxb_SendCommand.TextChanged += new System.EventHandler(this.rtxb_SendCommand_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(24, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Send Command:";
            // 
            // lstbx_Camera
            // 
            this.lstbx_Camera.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstbx_Camera.FormattingEnabled = true;
            this.lstbx_Camera.ItemHeight = 20;
            this.lstbx_Camera.Location = new System.Drawing.Point(212, 262);
            this.lstbx_Camera.Name = "lstbx_Camera";
            this.lstbx_Camera.Size = new System.Drawing.Size(172, 124);
            this.lstbx_Camera.TabIndex = 11;
            // 
            // DeviceDialogBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 416);
            this.ControlBox = false;
            this.Controls.Add(this.lstbx_Camera);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rtxb_SendCommand);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.rtxb_SerialTerminal);
            this.Controls.Add(this.rtxbArduinoName);
            this.Controls.Add(this.cboSerialPort);
            this.Controls.Add(this.btnCheckCamera);
            this.Controls.Add(this.btnCheckArduino);
            this.Name = "DeviceDialogBox";
            this.Text = "DeviceDialogBox";
            this.Load += new System.EventHandler(this.DeviceDialogBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCheckArduino;
        private System.Windows.Forms.Button btnCheckCamera;
        private System.Windows.Forms.ComboBox cboSerialPort;
        private System.Windows.Forms.RichTextBox rtxbArduinoName;
        private System.Windows.Forms.RichTextBox rtxb_SerialTerminal;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtxb_SendCommand;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstbx_Camera;
    }
}