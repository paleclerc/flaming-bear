using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MissionConfig : ScriptableObject
{
	public List<MissionInfoByMissionType> m_ListInfo;

	public GameObject GetMissionPrefabByMissionType(MissionType a_MissionType)
	{
		foreach (MissionInfoByMissionType item in m_ListInfo)
		{
			if(item.m_MissionType == a_MissionType)
			{
				return item.m_UIPrefab;
			}
		}
		return null;
	}
}


public class MissionConfigSO : AccessorsSO<MissionConfig>
{
	static public MissionConfig Instance{get{return GetInternalInstance(ResourcesPathUtil.MISSION_CONFIG);}}
}

[Serializable]
public class MissionInfoByMissionType
{
	public MissionType m_MissionType;
	public GameObject m_UIPrefab;
}
