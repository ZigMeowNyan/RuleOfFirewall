using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuleOfFirewall.Enums
{
	[Flags]
	public enum ProfileTypeEnum
	{
		Domain=1,
		Private=2,
		Public=4,
		All = 7,//This works as well as the below All value, unless a new Flag value is introduced.
		//All = 2147483647,
	}
}
