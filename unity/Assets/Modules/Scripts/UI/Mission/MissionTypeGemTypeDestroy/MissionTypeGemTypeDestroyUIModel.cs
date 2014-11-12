using System;

[Serializable]
public class MissionTypeGemTypeDestroyUIModel : MissionTypeBaseUIModel
{
	public int m_CurrentGemQuantity;
	public int m_TotalGemQuantity;
	public GemEnum m_GemType;
}
