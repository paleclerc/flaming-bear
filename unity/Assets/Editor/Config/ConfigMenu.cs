﻿
using UnityEditor;

static public class ConfigMenu 
{

	[MenuItem ("CustomMenu/Config/Create Level Item")]
	static void CreateLevelItem() 
	{
		MenuUtil.CreateAsset<LevelItem>();
	}

	[MenuItem ("CustomMenu/Config/Create World Map Section")]
	static void CreateWorldMapSection() 
	{
		MenuUtil.CreateAsset<WorldMapSection>();
	}

	[MenuItem ("CustomMenu/Config/Create Audio Item")]
	static void CreateAudioItem()
	{
		MenuUtil.CreateAsset<AudioItem>();
	}

	#region Unity Creation
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

	[MenuItem ("CustomMenu/Config/Unique/Create World Map Config")]
	static void CreateWorldMapConfig () 
	{
		MenuUtil.CreateAsset<WorldMapConfig>();
	}

	[MenuItem ("CustomMenu/Config/Unique/Create Mission Config")]
	static void CreateMissionConfig () 
	{
		MenuUtil.CreateAsset<MissionConfig>();
	}

	[MenuItem ("CustomMenu/Config/Unique/Create Audio Config")]
	static void CreateAudioConfig () 
	{
		MenuUtil.CreateAsset<AudioConfig>();
	}

	[MenuItem ("CustomMenu/Config/Unique/Create Scene Info Config")]
	static void CreateSceneInfoConfig () 
	{
		MenuUtil.CreateAsset<SceneInfoConfig>();
	}
	#endregion
}
