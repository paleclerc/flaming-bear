
using System;

[Serializable]
public class MissionTypeScore : MissionTypeBase 
{
	public int m_MinScore;

	public override bool IsMissionTypeCompleted (MissionProgression a_MissionProgression)
	{
		return (a_MissionProgression.m_Score >= m_MinScore);
	}
}
