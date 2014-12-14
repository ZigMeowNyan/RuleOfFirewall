using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RuleOfFirewall
{
	public static class Extensions
	{
		#region DataTable
		public static void AddColumn<T>(this DataTable dt, string ColumnName)
		{
			dt.Columns.Add(ColumnName, typeof(T));
		}
		public static T Cell<T>(this DataTable dt, int RowIndex, string ColumnName)
		{
			if (dt == null)
				throw new ArgumentNullException("Cell<T>: DataTable is null.", "dt");
			if (RowIndex < 0)
				throw new ArgumentOutOfRangeException("Cell<T>: RowIndex {0} is less than 0.", "RowIndex");
			if (dt.Rows.Count <= RowIndex)
				throw new ArgumentOutOfRangeException(string.Format("Cell<T>: RowIndex {0} is greater than DataTable's row count {1}.", RowIndex, dt.Rows.Count), "RowIndex");
			if (string.IsNullOrWhiteSpace(ColumnName))
				throw new ArgumentException("Cell<T>: ColumnName cannot be null or empty.", "ColumnName");
			int ColumnIndex = dt.Columns.IndexOf(ColumnName);
			if (ColumnIndex < 0)
				throw new ArgumentException(string.Format("Cell<T>: ColumnName {0} not in DataTable.", ColumnName), "ColumnName");
			return (T)dt.Rows[RowIndex][ColumnName];
		}
		public static T Cell<T>(this DataTable dt, int RowIndex, int ColumnIndex)
		{
			if (dt == null)
				throw new ArgumentNullException("Cell<T>: DataTable is null.", "dt");
			if (RowIndex < 0)
				throw new ArgumentOutOfRangeException("Cell<T>: RowIndex {0} is less than 0.", "RowIndex");
			if (dt.Rows.Count <= RowIndex)
				throw new ArgumentOutOfRangeException(string.Format("Cell<T>: RowIndex {0} is greater than DataTable's row count {1}", RowIndex, dt.Rows.Count), "RowIndex");
			if (ColumnIndex < 0 || ColumnIndex >= dt.Columns.Count)
				throw new ArgumentOutOfRangeException(string.Format("Cell<T>: ColumnIndex {0} is out of range.", ColumnIndex), "ColumnIndex");
			return (T)dt.Rows[RowIndex][ColumnIndex];
		}
		#endregion
		#region DataRow
		public static T Value<T>(this DataRow dr, string ColumnName)
		{
			if (dr == null)
				throw new ArgumentNullException("Cell<T>: DataRow is null.", "dr");
			if (string.IsNullOrWhiteSpace(ColumnName))
				throw new ArgumentException("Cell<T>: ColumnName cannot be null or empty.", "ColumnName");
			//if (dr.Table == null)
			//	throw new ArgumentNullException("Cell<T>: DataRow.Table is null. Can't lookup the ColumnName.", "dr.Table");
			//int ColumnIndex = dr.Table.Columns.IndexOf(ColumnName);
			//if (ColumnIndex < 0)
			//	throw new ArgumentException(string.Format("Cell<T>: ColumnName {0} not in DataTable.", ColumnName), "ColumnName");
			//return (T)dr[ColumnIndex];
			return (T)dr[ColumnName];
		}
		public static T Value<T>(this DataRow dr, int ColumnIndex)
		{
			if (dr == null)
				throw new ArgumentNullException("Cell<T>: DataRow is null.", "dr");
			if (ColumnIndex < 0 || ColumnIndex >= dr.ItemArray.Length)
				throw new ArgumentOutOfRangeException(string.Format("Cell<T>: ColumnIndex {0} is out of range.", ColumnIndex), "ColumnIndex");
			return (T)dr[ColumnIndex];
		}
		#endregion
		#region Enums
		public static T ParseEnum<T>(this string EnumString) where T : struct, IConvertible, IFormattable
		{
			return (T)EnumString.ParseEnum(typeof(T));
		}
		public static object ParseEnum(this string EnumString, Type tEnumType)
		{
			if (!tEnumType.IsEnum)
				throw new ArgumentException("ParseEnum<T> requires that T be an Enum type.");
			return Enum.Parse(tEnumType, EnumString, true);
		}
		#region Conversion
		internal static NetFwTypeLib.NET_FW_RULE_DIRECTION_ GetCOMEnum(this Enums.TrafficDirectionTypeEnum eVal)
		{
			switch (eVal)
			{
				case Enums.TrafficDirectionTypeEnum.Incoming:
					return NetFwTypeLib.NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
				case Enums.TrafficDirectionTypeEnum.Outgoing:
					return NetFwTypeLib.NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
				case Enums.TrafficDirectionTypeEnum.Max:
					return NetFwTypeLib.NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_MAX;
				default:
					throw new ArgumentException(string.Format("Unknown TrafficDirectionTypeEnum value", eVal.ToString()));
			}
		}
		internal static Enums.TrafficDirectionTypeEnum GetPublicEnum(this NetFwTypeLib.NET_FW_RULE_DIRECTION_ eVal)
		{
			switch (eVal)
			{
				case NetFwTypeLib.NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN:
					return Enums.TrafficDirectionTypeEnum.Incoming;
				case NetFwTypeLib.NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT:
					return Enums.TrafficDirectionTypeEnum.Outgoing;
				case NetFwTypeLib.NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_MAX:
					return Enums.TrafficDirectionTypeEnum.Max;
				default:
					throw new ArgumentException(string.Format("Unknown NET_FW_RULE_DIRECTION_ value", eVal.ToString()));
			}
		}
		internal static NetFwTypeLib.NET_FW_ACTION_ GetCOMEnum(this Enums.FirewallActionTypeEnum eVal)
		{
			switch (eVal)
			{
				case Enums.FirewallActionTypeEnum.Allow:
					return NetFwTypeLib.NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
				case Enums.FirewallActionTypeEnum.Block:
					return NetFwTypeLib.NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
				case Enums.FirewallActionTypeEnum.Max:
					return NetFwTypeLib.NET_FW_ACTION_.NET_FW_ACTION_MAX;
				default :
					throw new ArgumentException(string.Format("Unknown FirewallActionTypeEnum value", eVal.ToString()));
			}
		}
		internal static Enums.FirewallActionTypeEnum GetPublicEnum(this NetFwTypeLib.NET_FW_ACTION_ eVal)
		{
			switch (eVal)
			{
				case NetFwTypeLib.NET_FW_ACTION_.NET_FW_ACTION_ALLOW:
					return Enums.FirewallActionTypeEnum.Allow;
				case NetFwTypeLib.NET_FW_ACTION_.NET_FW_ACTION_BLOCK:
					return Enums.FirewallActionTypeEnum.Block;
				case NetFwTypeLib.NET_FW_ACTION_.NET_FW_ACTION_MAX:
					return Enums.FirewallActionTypeEnum.Max;
				default:
					throw new ArgumentException(string.Format("Unknown NET_FW_ACTION_ value", eVal.ToString()));
			}
		}
		internal static NetFwTypeLib.NET_FW_PROFILE_TYPE2_ GetCOMEnum(this Enums.ProfileTypeEnum eVal)
		{
			switch (eVal)
			{
				case Enums.ProfileTypeEnum.Domain:
					return NetFwTypeLib.NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN;
				case Enums.ProfileTypeEnum.Private:
					return NetFwTypeLib.NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE;
				case Enums.ProfileTypeEnum.Public:
					return NetFwTypeLib.NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC;
				case Enums.ProfileTypeEnum.All:
					return NetFwTypeLib.NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_ALL;
				default:
					throw new ArgumentException(string.Format("Unknown ProfileTypeEnum value", eVal.ToString()));
			}
		}
		internal static Enums.ProfileTypeEnum GetPublicEnum(this NetFwTypeLib.NET_FW_PROFILE_TYPE2_ eVal)
		{
			switch (eVal)
			{
				case NetFwTypeLib.NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN:
					return Enums.ProfileTypeEnum.Domain;
				case NetFwTypeLib.NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE:
					return Enums.ProfileTypeEnum.Private;
				case NetFwTypeLib.NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC:
					return Enums.ProfileTypeEnum.Public;
				case NetFwTypeLib.NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_ALL:
					return Enums.ProfileTypeEnum.All;
				default:
					throw new ArgumentException(string.Format("Unknown NET_FW_PROFILE_TYPE2_ value", eVal.ToString()));
			}
		}
		internal static NetFwTypeLib.NET_FW_IP_PROTOCOL_ GetCOMEnum(this Enums.ProtocolTypeEnum eVal)
		{
			switch (eVal)
			{
				case Enums.ProtocolTypeEnum.TCP:
					return NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
				case Enums.ProtocolTypeEnum.UDP:
					return NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
				case Enums.ProtocolTypeEnum.Any:
					return NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;
				default:
					throw new ArgumentException(string.Format("Unknown ProtocolTypeEnum value", eVal.ToString()));
			}
		}
		internal static Enums.ProtocolTypeEnum GetPublicEnum(this NetFwTypeLib.NET_FW_IP_PROTOCOL_ eVal)
		{
			switch (eVal)
			{
				case NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP:
					return Enums.ProtocolTypeEnum.TCP;
				case NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP:
					return Enums.ProtocolTypeEnum.UDP;
				case NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY:
					return Enums.ProtocolTypeEnum.Any;
				default:
					throw new ArgumentException(string.Format("Unknown NET_FW_IP_PROTOCOL_ value", eVal.ToString()));
			}
		}
		#endregion
		#endregion
		internal static dynamic InstantiateTypeFromGUID(string sGUID)
		{
			Type tRef = Type.GetTypeFromCLSID(new Guid(sGUID));
			return Activator.CreateInstance(tRef);
		}
		internal static dynamic InstantiateTypeFromProgID(string sProgID)
		{
			Type tRef = Type.GetTypeFromProgID(sProgID);
			return Activator.CreateInstance(tRef);
		}
		public static bool Matches(this string ValueToTest, string ComparisonValue, bool AnyCase = true)
		{
			if (AnyCase)
				return ValueToTest.ToLower().Equals(ComparisonValue.ToLower());
			else
				return ValueToTest.Equals(ComparisonValue);
		}
		public static bool Same(this NetFwTypeLib.INetFwRule rule1, NetFwTypeLib.INetFwRule rule2)
		{
			if (rule1.Name != null && rule2.Name != null)
			{
				if (!rule1.Name.Equals(rule2.Name))
					return false;
			}
			if (!rule1.Profiles.Equals(rule2.Profiles))
				return false;
			if (!rule1.Action.Equals(rule2.Action))
				return false;
			if (rule1.ApplicationName != null && rule2.ApplicationName != null)
			{
				if (!rule1.ApplicationName.Equals(rule2.ApplicationName))
					return false;
			}
			if (rule1.Description != null && rule2.Description != null)
			{
				if (!rule1.Description.Equals(rule2.Description))
					return false;
			}
			if (!rule1.Direction.Equals(rule2.Direction))
				return false;
			if (!rule1.EdgeTraversal.Equals(rule2.EdgeTraversal))
				return false;
			if (rule1.Grouping != null && rule2.Grouping != null)
			{
				if (!rule1.Grouping.Equals(rule2.Grouping))
					return false;
			}
			if (rule1.IcmpTypesAndCodes != null && rule2.IcmpTypesAndCodes != null)
			{
				if (!rule1.IcmpTypesAndCodes.Equals(rule2.IcmpTypesAndCodes))
					return false;
			}
			//TODO: compare Interfaces?  References of Interfaces usage: //http://msdn.microsoft.com/en-us/library/windows/desktop/dd339603(v=vs.85).aspx
			if (rule1.InterfaceTypes != null && rule2.InterfaceTypes != null)
			{
				if (!rule1.InterfaceTypes.Equals(rule2.InterfaceTypes))
					return false;
			}
			if (rule1.LocalAddresses != null && rule2.LocalAddresses != null)
			{
				if (!rule1.LocalAddresses.Equals(rule2.LocalAddresses))
					return false;
			}
			if (rule1.LocalPorts != null && rule2.LocalPorts != null)
			{
				if (!rule1.LocalPorts.Equals(rule2.LocalPorts))
					return false;
			}
			if (!rule1.Protocol.Equals(rule2.Protocol))
				return false;
			if (rule1.RemoteAddresses != null && rule2.RemoteAddresses != null)
			{
				if (!rule1.RemoteAddresses.Equals(rule2.RemoteAddresses))
					return false;
			}
			if (rule1.RemotePorts != null && rule2.RemotePorts != null)
			{
				if (!rule1.RemotePorts.Equals(rule2.RemotePorts))
					return false;
			}
			if (rule1.serviceName != null && rule2.serviceName != null)
			{
				if (!rule1.serviceName.Equals(rule2.serviceName))
					return false;
			}
			if (!rule1.Enabled.Equals(rule2.Enabled))
				return false;
			return true;
		}
	}
}
