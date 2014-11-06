
using UnityEditor;

static public class ConfigMenu 
{
	[MenuItem ("CustomMenu/Config/Create Gem Config")]
	static void CreateConfig () 
	{
		MenuUtil.CreateAsset<GemConfig>();
	}
}
