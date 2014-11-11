using System;

[Serializable]
public class MissionItem
{
	public MissionType m_MissionType;
	public MissionTypeScore m_MissionTypeScore;
	public MissionTypeGemDestroy m_MissionTypeGemDestroy;
	
	public MissionTypeBase GetCurrentMission()
	{
		switch (m_MissionType)
		{
		case MissionType.SCORE :
		{
			return m_MissionTypeScore;
		}
		case MissionType.GEM_DESTROYED :
		{
			return m_MissionTypeGemDestroy;
		}
		}

		return null;
	}
}
