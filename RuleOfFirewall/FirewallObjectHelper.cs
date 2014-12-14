using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetFwTypeLib;
using System.Windows.Forms;
using RuleOfFirewall.Enums;

namespace RuleOfFirewall
{
	internal static class FirewallObjectHelper
	{//Firewall scripting reference: http://msdn.microsoft.com/en-us/library/aa366415.aspx
		internal static INetFwAuthorizedApplication NewAuthorizedApplication(string RuleName, string ProcessPath, bool Enabled, string RemoteAddresses)
		{
			INetFwAuthorizedApplication app = Extensions.InstantiateTypeFromGUID("{EC9846B3-2762-4A6B-A214-6ACB603462D2}");//HNetCfg.FwAuthorizedApplication ?
			app.Name = RuleName;
			app.ProcessImageFileName = ProcessPath;
			app.Enabled = Enabled;
			if (RemoteAddresses != "Any")//Should be set to "*", but that's the default.
				app.RemoteAddresses = RemoteAddresses;
			return app;
		}
		
		internal static INetFwOpenPort NewOpenPortObject(string ExceptionName, bool Enabled, string PortDetails, string remoteAddresses, ProtocolTypeEnum protocol)//Requires a single Port.
		{
			INetFwOpenPort portException = Extensions.InstantiateTypeFromGUID("{0CA545C6-37AD-4A6C-BF92-9F7610067EF5}");//HNetCfg.FwOpenPort ?
			portException.Name = ExceptionName;
			portException.Port = int.Parse(PortDetails);
			portException.Protocol = protocol.GetCOMEnum();
			portException.RemoteAddresses = remoteAddresses;
			portException.Enabled = Enabled;
			return portException;
		}

		internal static INetFwMgr GetFwManagerInstance()//INetFwMgr, for Win XP
		{
			return (INetFwMgr)Extensions.InstantiateTypeFromGUID("{304CE942-6E39-40D8-943A-B913C40C9CD4}");//HNetCfg.FwMgr ?
		}
		//http://www.tech-archive.net/Archive/DotNet/microsoft.public.dotnet.framework/2007-06/msg00458.html - Discusses grouping on the firewall rules.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="RuleName">
		/// Note: Must not contain the '|' character.</param>
		/// <param name="actionType"></param>
		/// <param name="profileType"></param>
		/// <param name="protocol">Export rules.  Only doing UDP and TCP show in the referenced enum.  Additional protocols that are possibly supported:
		/// http://go.microsoft.com/fwlink/p/?linkid=89889 </param>
		/// <param name="localAddresses">
		/// "*" indicates any local address. If present, this must be the only token included.
		/// "Defaultgateway"
		/// "DHCP"
		/// "WINS"
		/// "LocalSubnet" indicates any local address on the local subnet. This token is not case-sensitive.
		/// A subnet can be specified using either the subnet mask or network prefix notation. If neither a subnet mask not a network prefix is specified, the subnet mask defaults to 255.255.255.255.
		/// A valid IPv6 address.
		/// An IPv4 address range in the format of "start address - end address" with no spaces included.
		/// An IPv6 address range in the format of "start address - end address" with no spaces included.
		/// </param>
		/// <param name="remoteAddresses">
		/// "*" indicates any remote address. If present, this must be the only token included.
		/// "Defaultgateway"
		/// "DHCP"
		/// "DNS"
		/// "WINS"
		/// "LocalSubnet" indicates any local address on the local subnet. This token is not case-sensitive.
		/// A subnet can be specified using either the subnet mask or network prefix notation. If neither a subnet mask not a network prefix is specified, the subnet mask defaults to 255.255.255.255.
		/// A valid IPv6 address.
		/// An IPv4 address range in the format of "start address - end address" with no spaces included.
		/// An IPv6 address range in the format of "start address - end address" with no spaces included.
		/// </param>
		/// <param name="localPorts"></param>
		/// <param name="remotePorts">
		/// </param>
		/// <param name="Program">The path to the program for which an exception is being added.
		/// Note: If there are environment variables, they will be expanded when the rule is saved.</param>
		/// <param name="AllowedUsers"></param>
		/// <param name="AllowedComputers"></param>
		/// <param name="Group">
		/// Using the Grouping property is highly recommended, as it groups multiple rules into a single line in the
		/// Windows Firewall control panel. This allows the user to enable or disable multiple rules with a single click.
		/// The Grouping property can also be specified using indirect strings. In this case, a group description can
		/// also be specified that will appear in the rule group properties in the Windows Firewall control panel. For
		/// example, if the group string is specified by an indirect string at index 1005 ("@yourresources.dll,-1005"),
		/// the group description can be specified at a resource string higher by 10000 "@youresources.dll,-11005."
		/// 
		/// When indirect strings in the form of "h" are passed as parameters to the Windows Firewall with Advanced
		/// Security APIs, they should either be placed under the System32 Windows directory or specified by a full
		/// path. Further, the file should have a secure access that permits the Local Service account read acces
		/// to allow the Windows Firewall Service to read the strings. To avoid non-privileged security principals
		/// from modifying the strings, the DLLs should only allow write access to the Administrator account.</param>
		/// <param name="Enabled"></param>
		/// <param name="Override"></param>
		/// <param name="TrafficDirection"></param>
		/// //Description: Describes the rule.  Must not contiain the '|' character.
		/// //Interfaces: Represented by their friendly names.  http://msdn.microsoft.com/en-us/library/windows/desktop/dd339603(v=vs.85).aspx
		/// //InterfaceTypes: Acceptable values for this property are "RemoteAccess", "Wireless", "Lan", and "All". If more than one interface type is specified, the strings must be separated by a comma.
		/// //ServiceName property: A serviceName value of "*" indicates that a service, not an application, must be sending or receiving traffic.
		/// <returns></returns>
		internal static INetFwRule NewFirewallRule(string RuleName, FirewallActionTypeEnum actionType, ProfileTypeEnum profileType, ProtocolTypeEnum protocol,
			string localAddresses, string remoteAddresses, string localPorts, string remotePorts,
			string Program,
			string AllowedUsers, string AllowedComputers,//Not yet supported.
			string Group,
			bool Enabled,
			bool Override,
			TrafficDirectionTypeEnum TrafficDirection = TrafficDirectionTypeEnum.Incoming)
		{
			INetFwRule firewallRule = Extensions.InstantiateTypeFromProgID("HNetCfg.FWRule");
			firewallRule.Name = RuleName;
			firewallRule.Direction = TrafficDirection.GetCOMEnum();
			firewallRule.Action = actionType.GetCOMEnum();
			firewallRule.Profiles = (int)profileType;
			#region Protocol
			firewallRule.Protocol = (int)protocol;
			#endregion
			if (Program.Matches("Any"))
			{
			#region Addresses and ports
				if (localAddresses.Matches("Any"))
					firewallRule.LocalAddresses = "*";
				else
					firewallRule.LocalAddresses = localAddresses.Replace(" ", "");
				if (remoteAddresses.Matches("Any"))
					firewallRule.RemoteAddresses = "*";
				else
					firewallRule.RemoteAddresses = remoteAddresses.Replace(" ", "");

				if (localPorts.Matches("Any"))
					firewallRule.LocalPorts = "*";
				else
					firewallRule.LocalPorts = localPorts.Replace(" ", "");
				if (remotePorts.Matches("Any"))
					firewallRule.RemotePorts = "*";
				else
					firewallRule.RemotePorts = remotePorts.Replace(" ", "");
			#endregion
			}
			else
				firewallRule.ApplicationName = Program;//I don't think we set IP addresses and ports when we're setting the program.
			//Set either scope or remote addresses, but not both.  - http://msdn.microsoft.com/en-us/library/aa366436(v=vs.85).aspx
			#region AllowedSettings
			//TODO: Find settings for Allowed Users and Allowed Computers
			#endregion
			//firewallRule.InterfaceTypes = "All";
			firewallRule.Grouping = Group;
			firewallRule.Enabled = Enabled;
			return firewallRule;
			//Additional data: http://msdn.microsoft.com/en-us/library/aa366447(v=vs.85).aspx
			//Interfaces: http://msdn.microsoft.com/en-us/library/aa366449(v=vs.85).aspx
		}
	}
}
