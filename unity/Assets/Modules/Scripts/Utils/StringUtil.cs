
using System.Globalization;

public class StringUtil
{
	static public string SeparateThousand(int a_Value)
	{
		return string.Format("{0:n0}", a_Value);
	}
}
