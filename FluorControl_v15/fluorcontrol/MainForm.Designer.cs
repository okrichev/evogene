namespace FluorControl
{
    partial class FluorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FluorControl));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acquisitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSettingsFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLogAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picbxImage = new System.Windows.Forms.PictureBox();
            this.rtxb_LogBook = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBox_FolderPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_SaveFolder = new System.Windows.Forms.Button();
            this.btn_DoFvFm = new System.Windows.Forms.Button();
            this.btn_Analyze = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.acquisitionToolStripMenuItem,
            this.panelToolStripMenuItem,
            this.logToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1284, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 25);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(110, 26);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // acquisitionToolStripMenuItem
            // 
            this.acquisitionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.loadSettingsToolStripMenuItem,
            this.saveSettingsToolStripMenuItem,
            this.loadSettingsFromToolStripMenuItem,
            this.saveSettingsAsToolStripMenuItem});
            this.acquisitionToolStripMenuItem.Name = "acquisitionToolStripMenuItem";
            this.acquisitionToolStripMenuItem.Size = new System.Drawing.Size(99, 25);
            this.acquisitionToolStripMenuItem.Text = "Acquisition";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // loadSettingsToolStripMenuItem
            // 
            this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
            this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.loadSettingsToolStripMenuItem.Text = "Load Settings";
            this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.saveSettingsToolStripMenuItem.Text = "Save Settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // loadSettingsFromToolStripMenuItem
            // 
            this.loadSettingsFromToolStripMenuItem.Name = "loadSettingsFromToolStripMenuItem";
            this.loadSettingsFromToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.loadSettingsFromToolStripMenuItem.Text = "Load Settings from...";
            this.loadSettingsFromToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsFromToolStripMenuItem_Click);
            // 
            // saveSettingsAsToolStripMenuItem
            // 
            this.saveSettingsAsToolStripMenuItem.Name = "saveSettingsAsToolStripMenuItem";
            this.saveSettingsAsToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.saveSettingsAsToolStripMenuItem.Text = "Save Settings as ...";
            this.saveSettingsAsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsAsToolStripMenuItem_Click);
            // 
            // panelToolStripMenuItem
            // 
            this.panelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.devicesToolStripMenuItem,
            this.controlPanelToolStripMenuItem});
            this.panelToolStripMenuItem.Name = "panelToolStripMenuItem";
            this.panelToolStripMenuItem.Size = new System.Drawing.Size(59, 25);
            this.panelToolStripMenuItem.Text = "Panel";
            // 
            // devicesToolStripMenuItem
            // 
            this.devicesToolStripMenuItem.Name = "devicesToolStripMenuItem";
            this.devicesToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.devicesToolStripMenuItem.Text = "Devices";
            this.devicesToolStripMenuItem.Click += new System.EventHandler(this.devicesToolStripMenuItem_Click);
            // 
            // controlPanelToolStripMenuItem
            // 
            this.controlPanelToolStripMenuItem.Name = "controlPanelToolStripMenuItem";
            this.controlPanelToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.controlPanelToolStripMenuItem.Text = "Control Panel";
            this.controlPanelToolStripMenuItem.Click += new System.EventHandler(this.controlPanelToolStripMenuItem_Click);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogToolStripMenuItem,
            this.saveLogAsToolStripMenuItem});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(48, 25);
            this.logToolStripMenuItem.Text = "Log";
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.clearLogToolStripMenuItem.Text = "Clear Log";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // saveLogAsToolStripMenuItem
            // 
            this.saveLogAsToolStripMenuItem.Name = "saveLogAsToolStripMenuItem";
            this.saveLogAsToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.saveLogAsToolStripMenuItem.Text = "Save Log As...";
            this.saveLogAsToolStripMenuItem.Click += new System.EventHandler(this.saveLogAsToolStripMenuItem_Click);
            // 
            // picbxImage
            // 
            this.picbxImage.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.picbxImage.Location = new System.Drawing.Point(12, 43);
            this.picbxImage.Name = "picbxImage";
            this.picbxImage.Size = new System.Drawing.Size(640, 512);
            this.picbxImage.TabIndex = 1;
            this.picbxImage.TabStop = false;
            // 
            // rtxb_LogBook
            // 
            this.rtxb_LogBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rtxb_LogBook.Location = new System.Drawing.Point(683, 65);
            this.rtxb_LogBook.Name = "rtxb_LogBook";
            this.rtxb_LogBook.Size = new System.Drawing.Size(577, 157);
            this.rtxb_LogBook.TabIndex = 2;
            this.rtxb_LogBook.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(683, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Log";
            // 
            // txtBox_FolderPath
            // 
            this.txtBox_FolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtBox_FolderPath.Location = new System.Drawing.Point(683, 346);
            this.txtBox_FolderPath.Name = "txtBox_FolderPath";
            this.txtBox_FolderPath.Size = new System.Drawing.Size(524, 26);
            this.txtBox_FolderPath.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(683, 314);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Folder Path:";
            // 
            // btn_SaveFolder
            // 
            this.btn_SaveFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_SaveFolder.BackgroundImage")));
            this.btn_SaveFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_SaveFolder.Location = new System.Drawing.Point(1214, 342);
            this.btn_SaveFolder.Name = "btn_SaveFolder";
            this.btn_SaveFolder.Size = new System.Drawing.Size(46, 35);
            this.btn_SaveFolder.TabIndex = 6;
            this.btn_SaveFolder.UseVisualStyleBackColor = true;
            this.btn_SaveFolder.Click += new System.EventHandler(this.btn_SaveFolder_Click);
            // 
            // btn_DoFvFm
            // 
            this.btn_DoFvFm.BackColor = System.Drawing.Color.Red;
            this.btn_DoFvFm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_DoFvFm.ForeColor = System.Drawing.Color.Aqua;
            this.btn_DoFvFm.Location = new System.Drawing.Point(683, 404);
            this.btn_DoFvFm.Name = "btn_DoFvFm";
            this.btn_DoFvFm.Size = new System.Drawing.Size(75, 46);
            this.btn_DoFvFm.TabIndex = 7;
            this.btn_DoFvFm.Text = "FvFm";
            this.btn_DoFvFm.UseVisualStyleBackColor = false;
            this.btn_DoFvFm.Click += new System.EventHandler(this.btn_DoFvFm_Click);
            // 
            // btn_Analyze
            // 
            this.btn_Analyze.BackColor = System.Drawing.Color.Green;
            this.btn_Analyze.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_Analyze.ForeColor = System.Drawing.Color.White;
            this.btn_Analyze.Location = new System.Drawing.Point(1132, 404);
            this.btn_Analyze.Name = "btn_Analyze";
            this.btn_Analyze.Size = new System.Drawing.Size(75, 46);
            this.btn_Analyze.TabIndex = 8;
            this.btn_Analyze.Text = "Analyze";
            this.btn_Analyze.UseVisualStyleBackColor = false;
            this.btn_Analyze.Click += new System.EventHandler(this.btn_Analyze_Click);
            // 
            // FluorControl_v5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 561);
            this.Controls.Add(this.btn_Analyze);
            this.Controls.Add(this.btn_DoFvFm);
            this.Controls.Add(this.btn_SaveFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBox_FolderPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtxb_LogBook);
            this.Controls.Add(this.picbxImage);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FluorControl_v5";
            this.Text = "FluorControl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acquisitionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSettingsFromToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem devicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlPanelToolStripMenuItem;
        private System.Windows.Forms.PictureBox picbxImage;
        private System.Windows.Forms.RichTextBox rtxb_LogBook;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBox_FolderPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_SaveFolder;
        private System.Windows.Forms.Button btn_DoFvFm;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLogAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Button btn_Analyze;
    }
}

