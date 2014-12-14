using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RuleOfFirewall
{
	public static class WinFormsExtensions
	{
		public static void PopulateComboBoxItemsWithEnumVals<T>(this ComboBox cbItem, T? optionalDefaultValue=null, ICollection<T> OptionalExcludeList = null) where T : struct, IConvertible, IFormattable
		{
			Type t = typeof(T);
			if (!t.IsEnum)
				throw new ArgumentException("PopulateComboBoxItemsWithEnumVals<T> requires that T be an Enum type.");
			List<T> values = new List<T>();
			if (OptionalExcludeList != null)
			{
				foreach (T tVal in (T[])Enum.GetValues(typeof(T)))
				{
					if (!OptionalExcludeList.Contains(tVal))
					{
						values.Add(tVal);
					}
				}
			}
			else
				values.AddRange((T[])Enum.GetValues(typeof(T)));
			cbItem.DataSource = values;
			if (optionalDefaultValue.HasValue)
				cbItem.SelectedItem = optionalDefaultValue.Value;
		}
		public static void PopulateComboBoxItemsWithEnumVals(this ComboBox cbItem, Type t, object optionalDefaultValue = null)
		{
			if (!t.IsEnum)
				throw new ArgumentException("PopulateComboBoxItemsWithEnumVals<T> requires that T be an Enum type.");
			ArrayList values = new ArrayList();
			values.AddRange(Enum.GetValues(t));
			cbItem.DataSource = values;
			if (optionalDefaultValue != null)
				cbItem.SelectedItem = optionalDefaultValue;
		}
	}
}
