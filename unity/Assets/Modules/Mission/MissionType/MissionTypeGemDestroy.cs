
using System;

[Serializable]
public class MissionTypeGemDestroy : MissionTypeBase 
{
	public GemEnum m_TypeGem;
	public int m_QuantityToDestroy;

	public override bool IsMissionTypeCompleted (MissionProgression a_MissionProgression)
	{
		if(a_MissionProgression.m_GemDestroy.ContainsKey(m_TypeGem))
		{
			return (a_MissionProgression.m_GemDestroy[m_TypeGem] >= m_QuantityToDestroy);
		}
		return (m_QuantityToDestroy <= 0);
	}
}