using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowController : MonoBehaviour	
{
	public string m_DebugDefaultLevelId;

	private bool m_IsNeedToValidate;
	private GemSwappingData m_GemSwapData = null;
	private bool m_IsPaused;
	private bool m_IsInit;
	private bool m_IsGameFinish;
	private bool m_ResultScreenDisplayed;
	private LevelInstance m_LevelInstance;
	private MissionProgression m_Progression;
	private LevelItem m_LevelItem;
	private GameStartParam m_CurrentLevelParam;
	private int m_MissionCompleted;

	public int RemainingMove {get {return CurrentMission.m_TotalMoveAvailable-m_Progression.m_Move;}}
	public bool IsPaused {get {return m_IsPaused;}}
	public MissionProgression MissionProgression {get {return m_Progression;}}
	public Mission CurrentMission {get {return m_LevelItem.m_Mission;}}
	public string m_AudioMissionCompleted;

	// Use this for initialization
	void Start ()
	{
		m_IsInit = false;
		m_IsGameFinish = false;
		m_ResultScreenDisplayed = false;
		m_IsPaused = false;
		m_MissionCompleted = 0;
		m_Progression = new MissionProgression();
		GetLevelItem();

		CreateLevel();
	}

	void GetLevelItem ()
	{
		string levelId = string.Empty;

		if(SceneManager.Instance != null)
		{
			m_CurrentLevelParam = (GameStartParam)SceneManager.Instance.GetParam();
			if(m_CurrentLevelParam != null)
			{
				levelId = m_CurrentLevelParam.m_LevelId;
			}
		}

		if(levelId == string.Empty)
		{
			levelId = m_DebugDefaultLevelId;
		}

		m_LevelItem = LevelConfigSO.Instance.GetLevelById(levelId);
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
	public void ReturnMainMenu()
	{
		SceneManager.Instance.ChangeScene(SceneInfoConfigSO.Instance.SCENE_NAME.MainMenu);
	}

	public void ReturnWorldMap()
	{
		SceneManager.Instance.ChangeScene(SceneInfoConfigSO.Instance.SCENE_NAME.WorldMap);
	}


	public void ReplayLevel()
	{
		SceneManager.Instance.ChangeScene(SceneInfoConfigSO.Instance.SCENE_NAME.Gameplay, m_CurrentLevelParam);
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

			CheckMissionCompleted();
		}
		else if(!m_IsInit)
		{
			InitCompleted();
		}
		else if(m_IsGameFinish && !m_ResultScreenDisplayed)
		{
			m_ResultScreenDisplayed = true;
			GameController.Instance.GetGameUIFlowController.DisplayResultScreen(m_Progression, CurrentMission);
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
			AddGemLineCount(listGemSlot.Count);
			foreach (GemSlot gemSlot in listGemSlot)
			{
				gemDeleted = true;
				GemEnum gemDestroyed = gemSlot.DeleteGem();
				AddGemDestroyCount(gemDestroyed);

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

	void AddGemLineCount (int a_QuantityInLine)
	{
		if(m_Progression.m_GemLineDestroy.ContainsKey(a_QuantityInLine))
		{
			m_Progression.m_GemLineDestroy[a_QuantityInLine] ++;
		}
		else
		{
			
			m_Progression.m_GemLineDestroy.Add(a_QuantityInLine, 1);
		}
	}

	void AddGemDestroyCount (GemEnum a_GemDestroyed)
	{
		if(a_GemDestroyed != GemEnum.NONE)
		{
			if(m_Progression.m_GemDestroy.ContainsKey(a_GemDestroyed))
			{
				m_Progression.m_GemDestroy[a_GemDestroyed] ++;
			}
			else
			{
				
				m_Progression.m_GemDestroy.Add(a_GemDestroyed, 1);
			}
			m_Progression.m_TotalGemDestroy++;
		}
	}

	void RemoveMove ()
	{
		m_Progression.m_Move ++;
		GameController.Instance.GetGameUIFlowController.UpdateProgression(m_Progression);

		if(RemainingMove <= 0)
		{
			m_Progression.m_Move = CurrentMission.m_TotalMoveAvailable;
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

			m_Progression.m_Score += score;
			GameController.Instance.GetGameUIFlowController.UpdateProgression(m_Progression);
		}
	}

	void CreateLevel ()
	{
		GameObject go = (GameObject)Instantiate(m_LevelItem.m_LevelPrefab);
		m_LevelInstance = go.GetComponent<LevelInstance>();

		m_LevelInstance.Init();
		m_IsNeedToValidate = m_LevelInstance.CreateGems();
	}

	void CheckMissionCompleted ()
	{

		int newQuantity = m_LevelItem.m_Mission.QuantityMissionCompleted(m_Progression);
		if(newQuantity != m_MissionCompleted)
		{
			AudioManager.Instance.PlayAudioItem(m_AudioMissionCompleted);
			m_MissionCompleted = newQuantity;
		}
	}
}

public class GemSwappingData
{
	public Gem m_FirstGemSwap;
	public Gem m_SecondGemSwap;
	public bool m_IsSwapHalfCompleted;
}
