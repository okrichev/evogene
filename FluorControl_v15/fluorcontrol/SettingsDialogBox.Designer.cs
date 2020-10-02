namespace FluorControl
{
    partial class SettingsDialogBox
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
            this.components = new System.ComponentModel.Container();
            this.PropertiesListView = new BrightIdeasSoftware.TreeListView();
            this.PropertyColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ValueColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.RangeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_LoadSettings = new System.Windows.Forms.Button();
            this.btn_SaveSettings = new System.Windows.Forms.Button();
            this.btnLoadFrom = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PropertiesListView)).BeginInit();
            this.SuspendLayout();
            // 
            // PropertiesListView
            // 
            this.PropertiesListView.AllColumns.Add(this.PropertyColumn);
            this.PropertiesListView.AllColumns.Add(this.ValueColumn);
            this.PropertiesListView.AllColumns.Add(this.RangeColumn);
            this.PropertiesListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
            this.PropertiesListView.CellEditUseWholeCell = false;
            this.PropertiesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PropertyColumn,
            this.ValueColumn,
            this.RangeColumn});
            this.PropertiesListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.PropertiesListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.PropertiesListView.Location = new System.Drawing.Point(25, 28);
            this.PropertiesListView.Name = "PropertiesListView";
            this.PropertiesListView.ShowGroups = false;
            this.PropertiesListView.Size = new System.Drawing.Size(573, 273);
            this.PropertiesListView.TabIndex = 2;
            this.PropertiesListView.UseCompatibleStateImageBehavior = false;
            this.PropertiesListView.View = System.Windows.Forms.View.Details;
            this.PropertiesListView.VirtualMode = true;
            // 
            // PropertyColumn
            // 
            this.PropertyColumn.AspectName = "Name";
            this.PropertyColumn.IsEditable = false;
            this.PropertyColumn.Text = "Property";
            this.PropertyColumn.Width = 171;
            // 
            // ValueColumn
            // 
            this.ValueColumn.AspectName = "Value";
            this.ValueColumn.Text = "Value";
            this.ValueColumn.Width = 106;
            // 
            // RangeColumn
            // 
            this.RangeColumn.AspectName = "Range";
            this.RangeColumn.IsEditable = false;
            this.RangeColumn.Text = "Range";
            this.RangeColumn.Width = 166;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_Cancel.Location = new System.Drawing.Point(25, 327);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(83, 48);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_OK.Location = new System.Drawing.Point(506, 327);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(92, 48);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_LoadSettings
            // 
            this.btn_LoadSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_LoadSettings.Location = new System.Drawing.Point(636, 28);
            this.btn_LoadSettings.Name = "btn_LoadSettings";
            this.btn_LoadSettings.Size = new System.Drawing.Size(116, 49);
            this.btn_LoadSettings.TabIndex = 5;
            this.btn_LoadSettings.Text = "Load";
            this.btn_LoadSettings.UseVisualStyleBackColor = true;
            this.btn_LoadSettings.Click += new System.EventHandler(this.btn_LoadSettings_Click);
            // 
            // btn_SaveSettings
            // 
            this.btn_SaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_SaveSettings.Location = new System.Drawing.Point(636, 111);
            this.btn_SaveSettings.Name = "btn_SaveSettings";
            this.btn_SaveSettings.Size = new System.Drawing.Size(116, 49);
            this.btn_SaveSettings.TabIndex = 6;
            this.btn_SaveSettings.Text = "Save";
            this.btn_SaveSettings.UseVisualStyleBackColor = true;
            this.btn_SaveSettings.Click += new System.EventHandler(this.btn_SaveSettings_Click);
            // 
            // btnLoadFrom
            // 
            this.btnLoadFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnLoadFrom.Location = new System.Drawing.Point(636, 200);
            this.btnLoadFrom.Name = "btnLoadFrom";
            this.btnLoadFrom.Size = new System.Drawing.Size(116, 49);
            this.btnLoadFrom.TabIndex = 7;
            this.btnLoadFrom.Text = "Load from...";
            this.btnLoadFrom.UseVisualStyleBackColor = true;
            this.btnLoadFrom.Click += new System.EventHandler(this.btnLoadFrom_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnSaveAs.Location = new System.Drawing.Point(636, 286);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(116, 49);
            this.btnSaveAs.TabIndex = 8;
            this.btnSaveAs.Text = " Save as...";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // SettingsDialogBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 392);
            this.ControlBox = false;
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnLoadFrom);
            this.Controls.Add(this.btn_SaveSettings);
            this.Controls.Add(this.btn_LoadSettings);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.PropertiesListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialogBox";
            this.Text = "SettingsDialogBox";
            this.Load += new System.EventHandler(this.SettingsDialogBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PropertiesListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.TreeListView PropertiesListView;
        private BrightIdeasSoftware.OLVColumn PropertyColumn;
        private BrightIdeasSoftware.OLVColumn ValueColumn;
        private BrightIdeasSoftware.OLVColumn RangeColumn;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_LoadSettings;
        private System.Windows.Forms.Button btn_SaveSettings;
        private System.Windows.Forms.Button btnLoadFrom;
        private System.Windows.Forms.Button btnSaveAs;

    }
}