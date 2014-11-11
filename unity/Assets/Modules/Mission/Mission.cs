using System;
using System.Collections.Generic;

[Serializable]
public class Mission
{
	public List<MissionItem> m_ListMissionItem;
	public int m_TotalMoveAvailable;

	public bool CheckMissionCompleted(MissionProgression a_MissionProgression)
	{
		foreach (MissionItem missionItem in m_ListMissionItem)
		{
			MissionTypeBase missionType = missionItem.GetCurrentMission();
			if(missionType != null)
			{
				if(!missionType.IsMissionTypeCompleted(a_MissionProgression))
				{
					return false;
				}
			}
		}

		return true;
	}

	public int GetScoreToWin()
	{
		int score = 0;
		foreach (MissionItem missionItem in m_ListMissionItem)
		{
			if(missionItem.m_MissionType == MissionType.SCORE)
			{
				MissionTypeScore missionType = (MissionTypeScore)missionItem.GetCurrentMission();
				if(missionType != null)
				{
					if(missionType.m_MinScore > score)
					{
						score = missionType.m_MinScore;
					}
				}
			}
		}
		
		return score;
	}
}

[Serializable]
public class MissionProgression
{
	public int m_Move;
	public int m_Score;
}