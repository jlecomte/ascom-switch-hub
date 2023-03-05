namespace ASCOM.DarkSkyGeek
{
    partial class SetupDialogForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDialogForm));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.DSGLogo = new System.Windows.Forms.PictureBox();
            this.switchDevicesDataGridView = new System.Windows.Forms.DataGridView();
            this.SwitchDeviceSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SwitchDeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.prependDeviceNameChk = new System.Windows.Forms.CheckBox();
            this.prependDeviceNameChkToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DSGLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchDevicesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.icon_ok_24;
            this.cmdOK.Location = new System.Drawing.Point(370, 310);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(59, 36);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.icon_cancel_24;
            this.cmdCancel.Location = new System.Drawing.Point(435, 310);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 36);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // chkTrace
            // 
            this.chkTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(12, 326);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // DSGLogo
            // 
            this.DSGLogo.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.darkskygeek;
            this.DSGLogo.Location = new System.Drawing.Point(12, 12);
            this.DSGLogo.Name = "DSGLogo";
            this.DSGLogo.Size = new System.Drawing.Size(88, 88);
            this.DSGLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DSGLogo.TabIndex = 7;
            this.DSGLogo.TabStop = false;
            this.DSGLogo.Click += new System.EventHandler(this.BrowseToHomepage);
            this.DSGLogo.DoubleClick += new System.EventHandler(this.BrowseToHomepage);
            // 
            // switchDevicesDataGridView
            // 
            this.switchDevicesDataGridView.AllowUserToAddRows = false;
            this.switchDevicesDataGridView.AllowUserToDeleteRows = false;
            this.switchDevicesDataGridView.AllowUserToResizeColumns = false;
            this.switchDevicesDataGridView.AllowUserToResizeRows = false;
            this.switchDevicesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.switchDevicesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.switchDevicesDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.switchDevicesDataGridView.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.switchDevicesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.switchDevicesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.switchDevicesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SwitchDeviceSelected,
            this.SwitchDeviceName});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.switchDevicesDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.switchDevicesDataGridView.Location = new System.Drawing.Point(12, 106);
            this.switchDevicesDataGridView.MultiSelect = false;
            this.switchDevicesDataGridView.Name = "switchDevicesDataGridView";
            this.switchDevicesDataGridView.RowHeadersVisible = false;
            this.switchDevicesDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.switchDevicesDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.switchDevicesDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.switchDevicesDataGridView.Size = new System.Drawing.Size(482, 182);
            this.switchDevicesDataGridView.TabIndex = 8;
            this.switchDevicesDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.switchDevicesDataGridView_CurrentCellDirtyStateChanged);
            // 
            // SwitchDeviceSelected
            // 
            this.SwitchDeviceSelected.DataPropertyName = "Selected";
            this.SwitchDeviceSelected.FalseValue = "false";
            this.SwitchDeviceSelected.FillWeight = 1F;
            this.SwitchDeviceSelected.HeaderText = "";
            this.SwitchDeviceSelected.IndeterminateValue = "false";
            this.SwitchDeviceSelected.MinimumWidth = 25;
            this.SwitchDeviceSelected.Name = "SwitchDeviceSelected";
            this.SwitchDeviceSelected.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SwitchDeviceSelected.TrueValue = "true";
            // 
            // SwitchDeviceName
            // 
            this.SwitchDeviceName.DataPropertyName = "Name";
            this.SwitchDeviceName.HeaderText = "Name";
            this.SwitchDeviceName.Name = "SwitchDeviceName";
            this.SwitchDeviceName.ReadOnly = true;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.Location = new System.Drawing.Point(107, 13);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(387, 87);
            this.descriptionLabel.TabIndex = 9;
            this.descriptionLabel.Text = resources.GetString("descriptionLabel.Text");
            // 
            // prependDeviceNameChk
            // 
            this.prependDeviceNameChk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prependDeviceNameChk.Location = new System.Drawing.Point(12, 303);
            this.prependDeviceNameChk.Name = "prependDeviceNameChk";
            this.prependDeviceNameChk.Size = new System.Drawing.Size(202, 17);
            this.prependDeviceNameChk.TabIndex = 10;
            this.prependDeviceNameChk.Text = "Prepend device name to each switch";
            this.prependDeviceNameChkToolTip.SetToolTip(this.prependDeviceNameChk, "This option is especially useful if you have\r\nswitches or gauges with the same na" +
        "me\r\nacross several devices. It is disabled by default\r\nbecause it can make the s" +
        "witch UI a little busy.");
            this.prependDeviceNameChk.UseVisualStyleBackColor = true;
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 354);
            this.Controls.Add(this.prependDeviceNameChk);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.switchDevicesDataGridView);
            this.Controls.Add(this.DSGLogo);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DarkSkyGeek’s ASCOM Switch Hub";
            this.Load += new System.EventHandler(this.SetupDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DSGLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchDevicesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.PictureBox DSGLogo;
        private System.Windows.Forms.DataGridView switchDevicesDataGridView;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SwitchDeviceSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn SwitchDeviceName;
        private System.Windows.Forms.CheckBox prependDeviceNameChk;
        private System.Windows.Forms.ToolTip prependDeviceNameChkToolTip;
    }
}