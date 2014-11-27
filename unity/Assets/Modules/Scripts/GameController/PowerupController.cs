using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupController : MonoBehaviour	
{
	public int m_BreakRandomGemQuantity = 5;

	private bool m_WaitingPowerup;

	public bool WaitingPowerup {get {return m_WaitingPowerup;}}

	// Use this for initialization
	void Start ()
	{
		m_WaitingPowerup = false;
	}

	public void UsePowerup (PowerupType a_PowerupType)
	{
		m_WaitingPowerup = true;
		Debug.Log("PAL :: PowerupController :: UsePowerup :: " + a_PowerupType.ToString());
		switch (a_PowerupType) 
		{
		case PowerupType.BREAK_RANDOM_GEM :
		{
			BreakRandomGem(m_BreakRandomGemQuantity);
			break;
		}
		}
		m_WaitingPowerup = false;
	}

	void BreakRandomGem (int a_QuantityToBreak)
	{
		List<GemSlot> listGemSlot = GameController.Instance.GetFlowController.CurrentLevelInstance.m_ListGemSlot;
		List<GemSlot> randomList = new List<GemSlot>();

		int quantityRandom = 0;
		int fallback = 0;
		while(quantityRandom < m_BreakRandomGemQuantity && (fallback < 1000))
		{
			fallback++;
			int randomIndex = Random.Range(0, listGemSlot.Count-1);
			if(listGemSlot[randomIndex].m_Gem != null)
			{
				if(!randomList.Contains(listGemSlot[randomIndex]))
				{
					randomList.Add(listGemSlot[randomIndex]);
					quantityRandom++;
				}
			}
		}
		List<List<GemSlot>> result = new List<List<GemSlot>>();
		result.Add(randomList);
		GameController.Instance.GetFlowController.DeleteGemAndAddScore(result);
	}
}