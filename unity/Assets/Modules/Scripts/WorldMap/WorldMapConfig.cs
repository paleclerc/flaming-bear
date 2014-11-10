using UnityEngine;
using System.Collections.Generic;

public class WorldMapConfig : ScriptableObject 
{
	public List<WorldMapSection> m_ListSection;
	
	public WorldMapSection GetSectionByIndex(int a_Index)
	{
		if((m_ListSection.Count == 0) || (a_Index >= m_ListSection.Count) || (a_Index < 0))
		{
			return null;
		}
		return m_ListSection[a_Index];
	}
}


public class WorldMapConfigSO : AccessorsSO<WorldMapConfig>
{
	static public WorldMapConfig Instance{get{return GetInternalInstance(ResourcesPathUtil.WORLD_MAP_CONFIG);}}
}