using System;
using UnityEngine;

[Serializable]
public class MissionItem
{
	public MissionType m_MissionType;

	public MissionTypeScore m_MissionTypeScore;
	public MissionTypeGemTypeDestroy m_MissionTypeGemTypeDestroy;
	public MissionTypeGemDestroy m_MissionTypeGemDestroy;
	public MissionTypeGemLineSizeDestroy m_MissionQuantityInLine;
	
	public MissionTypeBase GetCurrentMission()
	{
		switch (m_MissionType)
		{
		case MissionType.SCORE :
		{
			return m_MissionTypeScore;
		}
		case MissionType.GEM_TYPE_DESTROYED :
		{
			return m_MissionTypeGemTypeDestroy;
		}
		case MissionType.GEM_DESTROYED :
		{
			return m_MissionTypeGemDestroy;
		}
		case MissionType.QUANTITY_IN_LINE :
		{
			return m_MissionQuantityInLine;
		}
		}

		return null;
	}
}
