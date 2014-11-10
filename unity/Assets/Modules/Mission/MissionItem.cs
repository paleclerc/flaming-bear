using System;

[Serializable]
public class MissionItem
{
	public MissionType m_MissionType;
	public MissionTypeScore m_MissionTypeScore;
	
	public MissionTypeBase GetCurrentMission()
	{
		switch (m_MissionType)
		{
		case MissionType.SCORE :
		{
			return m_MissionTypeScore;
		}
		}

		return null;
	}
}
