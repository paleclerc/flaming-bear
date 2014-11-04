using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowController : MonoBehaviour	
{
	public List<GemSlot> m_GemSlot;

	private bool m_IsNeedToValidate;
	private GemSwappingData m_GemSwapData = null;

	private int m_RemainingMove;
	private int m_Score;
	private bool m_IsInit;

	// Use this for initialization
	void Start ()
	{
		m_IsInit = false;
		m_RemainingMove = 20;
		m_Score = 0;
	}

	void InitCompleted ()
	{
		m_IsInit = true;
		SceneManager.Instance.SceneLoadingCompleted();
	}

	public int GetRamainingMove ()
	{
		return m_RemainingMove;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_IsNeedToValidate)
		{
			foreach (GemSlot gemSlot in m_GemSlot)
			{
				if(gemSlot.IsGemMoving())
				{
					return;
				}
			}

			ValidateGem();
		}
		else if(!m_IsInit)
		{
			InitCompleted();
		}
	}

	public bool GetIsCanSwap()
	{
		return !m_IsNeedToValidate;
	}

	public void NewGemDropped()
	{
		m_IsNeedToValidate = true;
	}

	public void RegisterGemSlot(GemSlot a_GemSlot)
	{
		if(m_GemSlot == null)
		{
			m_GemSlot = new List<GemSlot>();
		}

		if(!m_GemSlot.Contains(a_GemSlot))
		{
			m_GemSlot.Add(a_GemSlot);
		}

		m_IsNeedToValidate = true;
	}

	public void SwapGem(Gem a_FirstGem, Gem a_SecondGem)
	{
		m_GemSwapData = new GemSwappingData();
		m_GemSwapData.m_IsSwapHalfCompleted = false;
		m_GemSwapData.m_FirstGemSwap = a_FirstGem;
		m_GemSwapData.m_SecondGemSwap = a_SecondGem;

		m_IsNeedToValidate = true;

		SwapGemData();
	}

	void SwapGemData()
	{
		m_IsNeedToValidate = true;

		m_GemSwapData.m_FirstGemSwap.ExchangeSlot(m_GemSwapData.m_SecondGemSwap);
	}

	public void UnregisterGemSlot(GemSlot a_GemSlot)
	{
		if(m_GemSlot.Contains(a_GemSlot))
		{
			m_GemSlot.Remove(a_GemSlot);
		}
	}

	public void ResetValidation ()
	{
		foreach (GemSlot gemSlot in m_GemSlot)
		{
			gemSlot.ResetValidation();
		}
	}

	void ValidateGem ()
	{
		ResetValidation();

		List<List<GemSlot>> allResult = new List<List<GemSlot>>();
		foreach (GemSlot gemSlot in m_GemSlot)
		{
			List<GemSlot> listSameGemHorizontal = new List<GemSlot>();
			gemSlot.ValidateHorizontal(listSameGemHorizontal);
			if(listSameGemHorizontal.Count > 2)
			{
				allResult.Add(listSameGemHorizontal);
				foreach (GemSlot checkedGemSlot in listSameGemHorizontal)
				{
					checkedGemSlot.SetCheckedHorizontal();
				}
			}
			List<GemSlot> listSameGemVertical = new List<GemSlot>();
			gemSlot.ValidateVertical(listSameGemVertical);
			if(listSameGemVertical.Count > 2)
			{
				allResult.Add(listSameGemVertical);
				foreach (GemSlot checkedGemSlot in listSameGemVertical)
				{
					checkedGemSlot.SetCheckedVertical();
				}
			}
		}
		bool gemDeleted = false;
		foreach (List<GemSlot> listGemSlot in allResult)
		{

			foreach (GemSlot gemSlot in listGemSlot)
			{
				gemDeleted = true;
				gemSlot.DeleteGem();
			}
		}

		if(!gemDeleted)
		{
			m_IsNeedToValidate = false;
		}

		AddScore(allResult);

		if(!gemDeleted && m_GemSwapData != null)
		{

			if(m_GemSwapData.m_IsSwapHalfCompleted)
			{ 
				m_GemSwapData = null;
				RemoveMove();
			}
			else
			{
				m_GemSwapData.m_IsSwapHalfCompleted = true;
				SwapGemData();
			}
		}
		else
		{
			if(m_GemSwapData != null)
			{
				m_GemSwapData = null;
				RemoveMove();
			}
		}
	}

	void RemoveMove ()
	{
		m_RemainingMove --;
		GameController.Instance.GetGameUIFlowController.UpdateMove(m_RemainingMove);
	}

	
	void AddScore(List<List<GemSlot>> a_Result)
	{
		if(m_IsInit)
		{
			int score = 0;
			foreach (List<GemSlot> listGemSlot in a_Result)
			{
				score += 1000 + (listGemSlot.Count-3)*500;
			}

			m_Score += score;
			GameController.Instance.GetGameUIFlowController.UpdateScore(m_Score);
		}
	}
}

public class GemSwappingData
{
	public Gem m_FirstGemSwap;
	public Gem m_SecondGemSwap;
	public bool m_IsSwapHalfCompleted;
}
