using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInstance : MonoBehaviour 
{
	public List<GemDropper> m_ListGemDropper;
	public List<GemSlot> m_ListGemSlot;
	public GameObject m_GemContainer;
	public LevelGemDropTable m_GemDropTable;

	public void Init()
	{
		GemUtil.InitGemUtil(m_GemDropTable, m_GemContainer);

		foreach (GemSlot gemSlot in m_ListGemSlot) 
		{
			gemSlot.Init();
		}
		
		foreach (GemDropper gemDropper in m_ListGemDropper)
		{
			gemDropper.Init();
		}
	}

	public void CreateGems ()
	{
		foreach (GemSlot gemSlot in m_ListGemSlot) 
		{
			gemSlot.CreateStartGem(true);
		}

		foreach (GemDropper gemDropper in m_ListGemDropper)
		{
			gemDropper.m_CanDrop = true;
		}
	}
}
