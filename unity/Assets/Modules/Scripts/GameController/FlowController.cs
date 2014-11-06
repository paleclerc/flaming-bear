using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowController : MonoBehaviour	
{
	public LevelInformation m_LevelInformationDebug;
	public bool IsPaused {get {return m_IsPaused;}}

	private bool m_IsNeedToValidate;
	private GemSwappingData m_GemSwapData = null;
	private bool m_IsPaused;
	private int m_Score;
	private bool m_IsInit;
	private bool m_IsGameFinish;
	private bool m_ResultScreenDisplayed;
	private LevelInstance m_LevelInstance;
	private int m_RemainingMove;

	// Use this for initialization
	void Start ()
	{
		m_IsInit = false;
		m_Score = 0;
		m_IsGameFinish = false;
		m_ResultScreenDisplayed = false;
		m_IsPaused = false;
		m_RemainingMove = m_LevelInformationDebug.m_TargetMove;

		CreateLevel();
	}

	void InitCompleted ()
	{
		m_IsInit = true;
		if(SceneManager.Instance != null)
		{
			SceneManager.Instance.SceneLoadingCompleted();
		}
	}

	#region Event Game
	public void LeaveLevel()
	{
		SceneManager.Instance.ChangeScene(SceneManager.Instance.m_SceneName.MainMenu);
	}

	public void ReplayLevel()
	{
		SceneManager.Instance.ChangeScene(SceneManager.Instance.m_SceneName.Gameplay);
	}

	public void PauseGame ()
	{
		m_IsPaused = true;
		GameController.Instance.GetGameUIFlowController.DisplayOptionMenu();
	}

	public void ResumeGame ()
	{
		GameController.Instance.GetGameUIFlowController.RemoveOptionMenu();
		m_IsPaused = false;
	}
	#endregion

	public int GetRamainingMove ()
	{
		return m_RemainingMove;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_IsNeedToValidate)
		{
			foreach (GemSlot gemSlot in m_LevelInstance.m_ListGemSlot)
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
			GameController.Instance.GetGameUIFlowController.DisplayResultScreen((m_LevelInformationDebug.m_TargetScore<m_Score), m_Score, m_LevelInformationDebug.m_TargetScore);
		}
	}

	public bool GetIsCanSwap()
	{
		return !m_IsNeedToValidate && !m_IsGameFinish && !m_IsPaused;
	}

	public void NewGemDropped()
	{
		m_IsNeedToValidate = true;
	}

	/*public void RegisterGemSlot(GemSlot a_GemSlot)
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
	}*/

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

	/*public void UnregisterGemSlot(GemSlot a_GemSlot)
	{
		if(m_GemSlot.Contains(a_GemSlot))
		{
			m_GemSlot.Remove(a_GemSlot);
		}
	}*/

	public void ResetValidation ()
	{
		foreach (GemSlot gemSlot in m_LevelInstance.m_ListGemSlot)
		{
			gemSlot.ResetValidation();
		}
	}

	void ValidateGem ()
	{
		ResetValidation();

		List<List<GemSlot>> allResult = new List<List<GemSlot>>();
		foreach (GemSlot gemSlot in m_LevelInstance.m_ListGemSlot)
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
			if(m_GemSwapData == null)
			{
				bool checkExistMove = m_LevelInstance.ExistPossibleMove();
				if(!checkExistMove)
				{
					m_LevelInstance.RandomizeGem();
					m_IsNeedToValidate = true;
				}
			}
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

	void CreateLevel ()
	{
		GameObject go = (GameObject)Instantiate(m_LevelInformationDebug.m_LevelPrefab);
		m_LevelInstance = go.GetComponent<LevelInstance>();

		m_LevelInstance.Init();
		m_IsNeedToValidate = m_LevelInstance.CreateGems();
	}
}

public class GemSwappingData
{
	public Gem m_FirstGemSwap;
	public Gem m_SecondGemSwap;
	public bool m_IsSwapHalfCompleted;
}
