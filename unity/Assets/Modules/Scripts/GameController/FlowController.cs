using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowController : MonoBehaviour	
{
	public List<GemSlot> m_GemSlot;
	public int m_TargetScore;
	public int m_RemainingMove;

	private bool m_IsNeedToValidate;
	private GemSwappingData m_GemSwapData = null;

	private int m_Score;
	private bool m_IsInit;
	private bool m_IsGameFinish;
	private bool m_ResultScreenDisplayed;

	// Use this for initialization
	void Start ()
	{
		m_IsInit = false;
		m_Score = 0;
		m_IsGameFinish = false;
		m_ResultScreenDisplayed = false;
	}

	void InitCompleted ()
	{
		m_IsInit = true;
		SceneManager.Instance.SceneLoadingCompleted();
	}

	public void LeaveLevel()
	{
		SceneManager.Instance.ChangeScene(SceneManager.Instance.m_SceneName.MainMenu);
	}

	public void ReplayLevel()
	{
		SceneManager.Instance.ChangeScene(SceneManager.Instance.m_SceneName.Gameplay);
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
		else if(m_IsGameFinish && !m_ResultScreenDisplayed)
		{
			m_ResultScreenDisplayed = true;
			GameController.Instance.GetGameUIFlowController.DisplayResultScreen((m_TargetScore<m_Score), m_Score, m_TargetScore);
		}
	}

	public bool GetIsCanSwap()
	{
		return !m_IsNeedToValidate && !m_IsGameFinish;
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

		if(m_RemainingMove <= 0)
		{
			m_IsGameFinish = true;
		}
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
