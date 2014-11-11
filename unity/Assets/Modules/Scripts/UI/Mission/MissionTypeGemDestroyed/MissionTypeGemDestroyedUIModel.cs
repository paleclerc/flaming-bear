using System;

[Serializable]
public class MissionTypeGemDestroyedUIModel : MissionTypeBaseUIModel
{
	public int m_CurrentGemQuantity;
	public int m_TotalGemQuantity;
	public GemEnum m_GemType;
}
