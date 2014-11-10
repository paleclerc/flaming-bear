
using UnityEditor;

static public class ConfigMenu 
{
	[MenuItem ("CustomMenu/Config/Unique/Create Gem Config")]
	static void CreateGemConfig () 
	{
		MenuUtil.CreateAsset<GemConfig>();
	}

	[MenuItem ("CustomMenu/Config/Unique/Create Level Config")]
	static void CreateLevelConfig () 
	{
		MenuUtil.CreateAsset<LevelConfig>();
	}

	[MenuItem ("CustomMenu/Config/Create Level Item")]
	static void CreateLevelItem() 
	{
		MenuUtil.CreateAsset<LevelItem>();
	}
}
