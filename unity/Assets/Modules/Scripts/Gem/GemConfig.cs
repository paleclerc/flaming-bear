using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GemConfig : ScriptableObject 
{
	public List<GemConfigItem> m_ListGem;

	public GameObject GetGemPrefabByType(GemEnum a_GemType)
	{
		foreach (GemConfigItem configItem in m_ListGem)
		{
			if(configItem.m_GemType == a_GemType)
			{
				return configItem.m_GemPrefab;
			}
		}
		return null;
	}

}

public class GemConfigSO : AccessorsSO<GemConfig>
{
	static public GemConfig Instance{get{return GetInternalInstance(ResourcesPathUtil.GEM_CONFIG);}}
}

[Serializable]
public class GemConfigItem
{
	public GemEnum m_GemType;
	public GameObject m_GemPrefab;
}