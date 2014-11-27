using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupController : MonoBehaviour	
{
	public int m_BreakRandomGemQuantity = 5;

	private PowerupType m_WaitingPowerup;

	public bool IsWaitingPowerup {get {return m_WaitingPowerup != PowerupType.NONE;}}

	// Use this for initialization
	void Start ()
	{
		m_WaitingPowerup = PowerupType.NONE;
	}

	public void UsePowerup (PowerupType a_PowerupType)
	{
		m_WaitingPowerup = a_PowerupType;

		Debug.Log("PAL :: PowerupController :: UsePowerup :: " + m_WaitingPowerup.ToString());

		if(m_WaitingPowerup == PowerupType.BREAK_RANDOM_GEM)
		{
			BreakRandomGem(m_BreakRandomGemQuantity);
		}

	}

	public void StopWaitingPowerup()
	{
		Debug.Log("PAL :: StopWaitingPowerup");
		m_WaitingPowerup = PowerupType.NONE;
	}

	public void ClickOnGemSlot (GemSlot a_GemSlot)
	{
		switch (m_WaitingPowerup) 
		{
		case PowerupType.BREAK_GEM_LINE :
		{
			BreakGemLine(a_GemSlot);
			break;
		}
		case PowerupType.BREAK_GEM_COLUMN :
		{
			BreakGemColumn(a_GemSlot);
			break;
		}
		case PowerupType.BREAK_THREE_BY_THREE_GEM :
		{
			BreakGemThreeByThree(a_GemSlot);
			break;
		}
		case PowerupType.CHANGE_GEM_COLOR_RANDOM :
		{
			ChangeRandomColor(a_GemSlot);
			break;
		}
		case PowerupType.BREAK_ALL_ONE_TYPE_GEM :
		{
			BreakAllGemSameType(a_GemSlot);
			break;
		}

		}

	}

	void SendGemToDelete(List<GemSlot> a_ListGemSlot)
	{
		List<List<GemSlot>> result = new List<List<GemSlot>>();
		result.Add(a_ListGemSlot);
		GameController.Instance.GetFlowController.DeleteGemAndAddScore(result);
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

		SendGemToDelete(randomList);

		StopWaitingPowerup();
	}


	void BreakGemLine (GemSlot a_GemSlot)
	{
		List<GemSlot> listToDelete = new List<GemSlot>();

		listToDelete.Add(a_GemSlot);

		GemSlot tempGemSlot = a_GemSlot.m_RightSlot;
		while(tempGemSlot != null)
		{
			listToDelete.Add(tempGemSlot);
			tempGemSlot = tempGemSlot.m_RightSlot;
		}

		tempGemSlot = a_GemSlot.m_LeftSlot;
		while(tempGemSlot != null)
		{
			listToDelete.Add(tempGemSlot);
			tempGemSlot = tempGemSlot.m_LeftSlot;
		}

		SendGemToDelete(listToDelete);

		StopWaitingPowerup();
	}

	void BreakGemColumn (GemSlot a_GemSlot)
	{
		List<GemSlot> listToDelete = new List<GemSlot>();
		
		listToDelete.Add(a_GemSlot);
		
		GemSlot tempGemSlot = a_GemSlot.m_UpSlot;
		while(tempGemSlot != null)
		{
			listToDelete.Add(tempGemSlot);
			tempGemSlot = tempGemSlot.m_UpSlot;
		}
		
		tempGemSlot = a_GemSlot.m_DownSlot;
		while(tempGemSlot != null)
		{
			listToDelete.Add(tempGemSlot);
			tempGemSlot = tempGemSlot.m_DownSlot;
		}
		
		SendGemToDelete(listToDelete);
		
		StopWaitingPowerup();
	}

	List<GemSlot> GetListLeftRight (GemSlot a_GemSlot)
	{
		List<GemSlot> listToSelect = new List<GemSlot>();
		if(a_GemSlot != null)
		{
			listToSelect.Add(a_GemSlot);

			if(a_GemSlot.m_LeftSlot != null)
			{
				listToSelect.Add(a_GemSlot.m_LeftSlot);
			}

			if(a_GemSlot.m_RightSlot != null)
			{
				listToSelect.Add(a_GemSlot.m_RightSlot);
			}
		}

		return listToSelect;
	}

	void BreakGemThreeByThree (GemSlot a_GemSlot)
	{
		List<List<GemSlot>> result = new List<List<GemSlot>>();

		List<GemSlot> listToDelete = GetListLeftRight(a_GemSlot);
		if(listToDelete.Count > 0)
		{
			result.Add(listToDelete);
		}
		listToDelete = GetListLeftRight(a_GemSlot.m_UpSlot);
		if(listToDelete.Count > 0)
		{
			result.Add(listToDelete);
		}
		listToDelete = GetListLeftRight(a_GemSlot.m_DownSlot);
		if(listToDelete.Count > 0)
		{
			result.Add(listToDelete);
		}

		GameController.Instance.GetFlowController.DeleteGemAndAddScore(result);
		
		StopWaitingPowerup();
	}

	void ChangeRandomColor (GemSlot a_GemSlot)
	{
		GemEnum gemType = a_GemSlot.m_Gem.m_GemInfo.m_GemType;

		while(gemType == a_GemSlot.m_Gem.m_GemInfo.m_GemType)
		{
			a_GemSlot.DeleteAndRandomizeGem();
		}

		GameController.Instance.GetFlowController.NewGemDropped();

		StopWaitingPowerup();
	}

	void BreakAllGemSameType(GemSlot a_GemSlot)
	{
		GemEnum gemType = a_GemSlot.m_Gem.m_GemInfo.m_GemType;

		List<GemSlot> listGemSlot = GameController.Instance.GetFlowController.CurrentLevelInstance.m_ListGemSlot;
		List<GemSlot> listToDelete = new List<GemSlot>();
		
		foreach (GemSlot gemSlot in listGemSlot) 
		{
			if(gemSlot.m_Gem != null && gemSlot.m_Gem.m_GemInfo.m_GemType == gemType)
			{
				listToDelete.Add(gemSlot);
			}
		}
		
		SendGemToDelete(listToDelete);
		
		StopWaitingPowerup();
	}
}