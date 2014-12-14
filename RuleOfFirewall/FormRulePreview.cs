using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using RuleOfFirewall.Enums;
using NetFwTypeLib;
using Microsoft.Win32;

namespace RuleOfFirewall
{
	public partial class FormRulePreview : Form
	{
		public FormRulePreview()
		{
			InitializeComponent();
			comboBoxTrafficDirection.PopulateComboBoxItemsWithEnumVals<Enums.TrafficDirectionTypeEnum>(Enums.TrafficDirectionTypeEnum.Incoming
				,new List<TrafficDirectionTypeEnum>() { Enums.TrafficDirectionTypeEnum.Max });
		}

		private void buttonLoadRules_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.AddExtension = false;
			ofd.AutoUpgradeEnabled = true;
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.DefaultExt = ".txt";
			ofd.DereferenceLinks = true;
			ofd.Filter = "Firewall rule export file (*.txt)|*.txt|All files (*.*)|*.*";
			ofd.Multiselect = false;
			ofd.ReadOnlyChecked = true;
			ofd.SupportMultiDottedExtensions = true;
			ofd.Title = "Firewall Rule File";
			DialogResult dr = ofd.ShowDialog();
			if (dr == System.Windows.Forms.DialogResult.OK)
			{
				FormatDataGridView(ofd.FileName);
			}
		}
		private void FormatDataGridView(string FileName)
		{
			textBoxFirewallFilePath.Text = FileName;
			try
			{
				DataTable dtFirewall = ParseFile(FileName);
				if (dtFirewall != null)
				{
					dataGridViewFirewallRules.DataSource = dtFirewall;
					dataGridViewFirewallRules.Columns[0].Width = 170;//Name
					dataGridViewFirewallRules.Columns[1].Width = 50;//Group
					dataGridViewFirewallRules.Columns[2].Width = 84;//Profile
					dataGridViewFirewallRules.Columns[3].Width = 30;//Enabled
					dataGridViewFirewallRules.Columns[4].Width = 35;//Allow
					dataGridViewFirewallRules.Columns[5].Width = 48;//Override
					dataGridViewFirewallRules.Columns[6].Width = 175;//Program
					dataGridViewFirewallRules.Columns[7].Width = 90;//Local Address
					dataGridViewFirewallRules.Columns[8].Width = 90;//Remote Address
					dataGridViewFirewallRules.Columns[9].Width = 48;//Protocol
					dataGridViewFirewallRules.Columns[10].Width = 105;//Local Port
					dataGridViewFirewallRules.Columns[11].Width = 105;//Remote Port
					dataGridViewFirewallRules.Columns[12].Width = 60;//Allowed Users
					dataGridViewFirewallRules.Columns[13].Width = 60;//Allowed Computers
				}
				else
					MessageBox.Show("Could not parse file.");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not parse file.");
			}
		}
		private DataTable ParseFile(string FileName)
		{
			if (File.Exists(FileName))
			{
				string[] fileLines = File.ReadAllLines(FileName);
				if (fileLines != null && fileLines.Length > 0)
				{
					DataTable dtFWRules = new DataTable();
					dtFWRules.AddColumn<string>("Name");
					dtFWRules.AddColumn<string>("Group");
					dtFWRules.AddColumn<ProfileTypeEnum>("Profile");
					dtFWRules.AddColumn<bool>("On");
					dtFWRules.AddColumn<FirewallActionTypeEnum>("Allow");
					dtFWRules.AddColumn<bool>("Override");
					dtFWRules.AddColumn<string>("Program");
					dtFWRules.AddColumn<string>("Local IP");
					dtFWRules.AddColumn<string>("Remote IP");
					dtFWRules.AddColumn<ProtocolTypeEnum>("Protocol");
					dtFWRules.AddColumn<string>("Local Ports");
					dtFWRules.AddColumn<string>("Remote Ports");
					dtFWRules.AddColumn<string>("Allowed Users");
					dtFWRules.AddColumn<string>("Allowed Computers");
					for (int i = 1; i < fileLines.Length; i++)
					{
						string[] lineFields = fileLines[i].Split('\t');
						DataRow dr = dtFWRules.NewRow();
						dr[0] = lineFields[0];//Name
						dr[1] = lineFields[1] ?? "";//Group
						dr[2] = lineFields[2].ParseEnum<ProfileTypeEnum>();//Profile
						dr[3] = lineFields[3].Matches("yes");//Enabled
						dr[4] = lineFields[4].ParseEnum<FirewallActionTypeEnum>();//Allow
						dr[5] = lineFields[5].Matches("yes");//Override
						dr[6] = lineFields[6];//Program
						dr[7] = lineFields[7];//Local Address
						dr[8] = lineFields[8];//Remote Address						
						dr[9] = lineFields[9].ParseEnum<ProtocolTypeEnum>();////Protocol
						dr[10] = lineFields[10];//Local Port
						dr[11] = lineFields[11];//Remote Port
						dr[12] = lineFields[12];//Allowed Users
						dr[13] = lineFields[13];//Allowed Computers

						dtFWRules.Rows.Add(dr);
					}
					return dtFWRules;
				}
				else
					return null;
			}
			else
				return null;
		}
		private void buttonImportRules_Click(object sender, EventArgs e)
		{
			if (dataGridViewFirewallRules.Rows.Count > 0)
			{
				TrafficDirectionTypeEnum tDirection;
				tDirection = (TrafficDirectionTypeEnum)comboBoxTrafficDirection.SelectedValue;
				DataTable dtRules = (DataTable)dataGridViewFirewallRules.DataSource;
				ApplyFirewallRules(dtRules, tDirection);
			}
		}
		private bool ApplyFirewallRules(DataTable dtRules, TrafficDirectionTypeEnum direction)
		{
			INetFwPolicy2 policyObj = Extensions.InstantiateTypeFromProgID("HNetCfg.FwPolicy2");//Not valid for Windows XP.  Use INetFwMgr (HNetCfg.FwMgr)
			
			for (int i = 0; i < dtRules.Rows.Count; i++)
			{
				INetFwRule potentialNewRule = FirewallObjectHelper.NewFirewallRule(
					dtRules.Cell<string>(i, "Name"),
					dtRules.Cell<FirewallActionTypeEnum>(i, "Allow"),
					dtRules.Cell<ProfileTypeEnum>(i, "Profile"),
					dtRules.Cell<ProtocolTypeEnum>(i, "Protocol"),
					dtRules.Cell<string>(i, "Local IP"),
					dtRules.Cell<string>(i, "Remote IP"),
					dtRules.Cell<string>(i, "Local Ports"),
					dtRules.Cell<string>(i, "Remote Ports"),
					dtRules.Cell<string>(i, "Program"),
					dtRules.Cell<string>(i, "Allowed Users"),
					dtRules.Cell<string>(i, "Allowed Computers"),
					dtRules.Cell<string>(i, "Group"),
					dtRules.Cell<bool>(i, "On"),
					dtRules.Cell<bool>(i, "Override"),
					direction);
				
				bool RuleIsPresent = false;
				foreach (INetFwRule ruleObj in policyObj.Rules)
				{
					if (potentialNewRule.Same(ruleObj))//TODO: Support Interfaces comparison, too.
						RuleIsPresent = true;
				}
				if (!RuleIsPresent)
				{

					policyObj.Rules.Add(potentialNewRule);
					Marshal.FinalReleaseComObject(potentialNewRule);
					potentialNewRule = null;
				}
			}
			return true;
		}
		#region Simple Exception code
		private INetFwAuthorizedApplication MakeApplicationException(DataRow dr)
		{
			return FirewallObjectHelper.NewAuthorizedApplication(dr.Value<string>("Name"), dr.Value<string>("Program"), dr.Value<bool>("On"), dr.Value<string>("Remote IP"));
			//TODO: Scope and IP version
		}
		private INetFwOpenPort MakePortException(DataRow dr)
		{
			return FirewallObjectHelper.NewOpenPortObject(dr.Value<string>("Name"), dr.Value<bool>("On"), dr.Value<string>("Local Ports"), dr.Value<string>("Remote IP"), dr.Value<ProtocolTypeEnum>("Protocol"));		
		}
		#endregion
		#region Drag/Drop handling.  File name passed to FormatDataGridView()
		private void FormFirewallImporter_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
				if (FileList != null && FileList.Length > 0)
				{
					FormatDataGridView(FileList[0]);
				}
			}
		}

		private void FormFirewallImporter_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
			{
				e.Effect = DragDropEffects.All;
			}
		}
		#endregion
	}
}
