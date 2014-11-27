using UnityEngine;
using System.Collections;

public class GameUIFlowController : MonoBehaviour 
{
	public GameObject m_ResultScreenWinUIControllerPrefab;
	public GameObject m_ResultScreenLoseUIControllerPrefab;
	public GameObject m_OptionMenuUIControllerPrefab;
	public GameObject m_PowerupUIControllerPrefab;

	public string m_WinMusic;
	public string m_LoseMusic;

	public GameObject m_GameUIControllerPrefab;
	private GameUIController m_GameUIController;
	private OptionMenuUIController m_OptionMenuUIController;
	private PowerupMenuUIController m_PowerupMenuUIController;
	// Use this for initialization
	void Start () 
	{
		CreateGameScreen();
	}

	private GameObject CreateScreen(GameObject a_Prefab)
	{
		GameObject go = (GameObject)Instantiate(a_Prefab);
		go.transform.parent = this.transform;
		
		return go;
	}

	#region Game Screen
	void CreateGameScreen ()
	{
		GameObject go = CreateScreen(m_GameUIControllerPrefab);
		m_GameUIController = go.GetComponent<GameUIController>();

		m_GameUIController.OnOptionEvent += GameController.Instance.GetFlowController.PauseGame;
		m_GameUIController.OnPowerupEvent += GameController.Instance.GetFlowController.PowerupMenu;
		m_GameUIController.Init(GameController.Instance.GetFlowController.CurrentMission, GameController.Instance.GetFlowController.MissionProgression);
	}

	public void UpdateProgression(MissionProgression a_Progression)
	{
		m_GameUIController.UpdateProgression(a_Progression);
	}
	#endregion

	#region Result Screen
	public void DisplayResultScreen(MissionProgression a_Progression,  Mission a_Mission)
	{
		GameObject go = null;

		if(a_Mission.CheckMissionCompleted(a_Progression))
		{
			go = CreateScreen(m_ResultScreenWinUIControllerPrefab);
			AudioManager.Instance.PlayAudioItem(m_WinMusic);
		}
		else
		{
			go = CreateScreen(m_ResultScreenLoseUIControllerPrefab);
			AudioManager.Instance.PlayAudioItem(m_LoseMusic);
		}

		ResultScreenUIController controller = go.GetComponent<ResultScreenUIController>();

		controller.OnContinueEvent += GameController.Instance.GetFlowController.ReturnWorldMap;
		controller.OnLeaveEvent += GameController.Instance.GetFlowController.ReturnMainMenu;

		controller.OnReplayEvent += GameController.Instance.GetFlowController.ReplayLevel;

		controller.Init(a_Mission, a_Progression);
	}
	#endregion
	public void RemoveAllOtherMenu()
	{
		if(m_OptionMenuUIController != null)
		{
			Destroy(m_OptionMenuUIController.gameObject);
		}
		if(m_PowerupMenuUIController != null)
		{
			Destroy(m_PowerupMenuUIController.gameObject);
		}

	}
	#region Option Menu
	public void DisplayOptionMenu()
	{
		if(m_OptionMenuUIController == null)
		{
			RemoveAllOtherMenu();

			GameObject go = CreateScreen(m_OptionMenuUIControllerPrefab);
			m_OptionMenuUIController = go.GetComponent<OptionMenuUIController>();
			m_OptionMenuUIController.OnExitEvent += GameController.Instance.GetFlowController.ReturnWorldMap;
			m_OptionMenuUIController.OnResumeEvent += GameController.Instance.GetFlowController.ResumeGame;
			m_OptionMenuUIController.Init();
		}
	}
	#endregion

	#region Option Menu
	public void DisplayPowerupMenu()
	{
		if(m_PowerupMenuUIController == null)
		{
			RemoveAllOtherMenu();

			GameObject go = CreateScreen(m_PowerupUIControllerPrefab);
			m_PowerupMenuUIController = go.GetComponent<PowerupMenuUIController>();
			m_PowerupMenuUIController.OnCancelEvent += GameController.Instance.GetFlowController.ResumeGame;
			m_PowerupMenuUIController.OnPowerupEvent += GameController.Instance.GetFlowController.OnPowerupUse;
			m_PowerupMenuUIController.Init();
		}
	}


	#endregion

	public void DisplayPowerupTarget (bool a_IsEnabled)
	{
		m_GameUIController.PowerupTarget(a_IsEnabled);
	}


}
