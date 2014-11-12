
using System;

[Serializable]
public class MissionTypeGemLineSizeDestroy : MissionTypeBase 
{
	public int m_QuantityInLine = 3;
	public int m_QuantityToDestroy;

	public override bool IsMissionTypeCompleted (MissionProgression a_MissionProgression)
	{
		if(a_MissionProgression.m_GemLineDestroy.ContainsKey(m_QuantityInLine))
		{
			return (a_MissionProgression.m_GemLineDestroy[m_QuantityInLine] >= m_QuantityToDestroy);
		}
		return (m_QuantityToDestroy <= 0);
	}
}