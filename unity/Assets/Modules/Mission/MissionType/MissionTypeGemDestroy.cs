
using System;

[Serializable]
public class MissionTypeGemDestroy : MissionTypeBase 
{
	public int m_QuantityToDestroy;

	public override bool IsMissionTypeCompleted (MissionProgression a_MissionProgression)
	{
		return (a_MissionProgression.m_TotalGemDestroy >= m_QuantityToDestroy);
	}
}