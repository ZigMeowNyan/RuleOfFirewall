namespace RuleOfFirewall
{
	partial class FormRulePreview
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.buttonOpenFirewallRules = new System.Windows.Forms.Button();
			this.textBoxFirewallFilePath = new System.Windows.Forms.TextBox();
			this.buttonImportRules = new System.Windows.Forms.Button();
			this.dataGridViewFirewallRules = new System.Windows.Forms.DataGridView();
			this.comboBoxTrafficDirection = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewFirewallRules)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonOpenFirewallRules
			// 
			this.buttonOpenFirewallRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOpenFirewallRules.Location = new System.Drawing.Point(1025, 196);
			this.buttonOpenFirewallRules.Name = "buttonOpenFirewallRules";
			this.buttonOpenFirewallRules.Size = new System.Drawing.Size(62, 23);
			this.buttonOpenFirewallRules.TabIndex = 0;
			this.buttonOpenFirewallRules.Text = "Load ...";
			this.buttonOpenFirewallRules.UseVisualStyleBackColor = true;
			this.buttonOpenFirewallRules.Click += new System.EventHandler(this.buttonLoadRules_Click);
			// 
			// textBoxFirewallFilePath
			// 
			this.textBoxFirewallFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFirewallFilePath.Location = new System.Drawing.Point(6, 198);
			this.textBoxFirewallFilePath.Name = "textBoxFirewallFilePath";
			this.textBoxFirewallFilePath.Size = new System.Drawing.Size(714, 20);
			this.textBoxFirewallFilePath.TabIndex = 1;
			// 
			// buttonImportRules
			// 
			this.buttonImportRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonImportRules.Location = new System.Drawing.Point(1093, 196);
			this.buttonImportRules.Name = "buttonImportRules";
			this.buttonImportRules.Size = new System.Drawing.Size(75, 23);
			this.buttonImportRules.TabIndex = 2;
			this.buttonImportRules.Text = "Import";
			this.buttonImportRules.UseVisualStyleBackColor = true;
			this.buttonImportRules.Click += new System.EventHandler(this.buttonImportRules_Click);
			// 
			// dataGridViewFirewallRules
			// 
			this.dataGridViewFirewallRules.AllowUserToAddRows = false;
			this.dataGridViewFirewallRules.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightBlue;
			this.dataGridViewFirewallRules.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewFirewallRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridViewFirewallRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.Cornsilk;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewFirewallRules.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewFirewallRules.Dock = System.Windows.Forms.DockStyle.Top;
			this.dataGridViewFirewallRules.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewFirewallRules.MultiSelect = false;
			this.dataGridViewFirewallRules.Name = "dataGridViewFirewallRules";
			this.dataGridViewFirewallRules.RowHeadersVisible = false;
			this.dataGridViewFirewallRules.Size = new System.Drawing.Size(1173, 190);
			this.dataGridViewFirewallRules.TabIndex = 3;
			// 
			// comboBoxTrafficDirection
			// 
			this.comboBoxTrafficDirection.FormattingEnabled = true;
			this.comboBoxTrafficDirection.Location = new System.Drawing.Point(738, 198);
			this.comboBoxTrafficDirection.Name = "comboBoxTrafficDirection";
			this.comboBoxTrafficDirection.Size = new System.Drawing.Size(113, 21);
			this.comboBoxTrafficDirection.TabIndex = 5;
			// 
			// FormRulePreview
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1173, 225);
			this.Controls.Add(this.comboBoxTrafficDirection);
			this.Controls.Add(this.dataGridViewFirewallRules);
			this.Controls.Add(this.buttonImportRules);
			this.Controls.Add(this.textBoxFirewallFilePath);
			this.Controls.Add(this.buttonOpenFirewallRules);
			this.Name = "FormRulePreview";
			this.Text = "Firewall Importer";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormFirewallImporter_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormFirewallImporter_DragEnter);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewFirewallRules)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOpenFirewallRules;
		private System.Windows.Forms.TextBox textBoxFirewallFilePath;
		private System.Windows.Forms.Button buttonImportRules;
		private System.Windows.Forms.DataGridView dataGridViewFirewallRules;
		private System.Windows.Forms.ComboBox comboBoxTrafficDirection;
	}
}

