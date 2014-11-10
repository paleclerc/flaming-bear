using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LevelConfig : ScriptableObject 
{
	public List<LevelItem> m_ListLevel;
	
	public LevelItem GetLevelById(string a_LevelId)
	{
		foreach (LevelItem levelItem in m_ListLevel)
		{
			if(levelItem.m_Id == a_LevelId)
			{
				return levelItem;
			}
		}
		return null;
	}
	
}

public class LevelConfigSO : AccessorsSO<LevelConfig>
{
	static public LevelConfig Instance{get{return GetInternalInstance(ResourcesPathUtil.LEVEL_CONFIG);}}
}