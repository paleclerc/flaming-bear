using UnityEngine;
using System.Collections;

public class GameUIFlowController : MonoBehaviour 
{
	public GameObject m_ResultScreenWinUIControllerPrefab;
	public GameObject m_ResultScreenLoseUIControllerPrefab;
	public GameObject m_OptionMenuUIControllerPrefab;

	public GameObject m_GameUIControllerPrefab;
	private GameUIController m_GameUIController;
	private OptionMenuUIController m_OptionMenuUIController;

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
		}
		else
		{
			go = CreateScreen(m_ResultScreenLoseUIControllerPrefab);
		}

		ResultScreenUIController controller = go.GetComponent<ResultScreenUIController>();

		controller.OnContinueEvent += GameController.Instance.GetFlowController.ReturnWorldMap;
		controller.OnLeaveEvent += GameController.Instance.GetFlowController.ReturnMainMenu;

		controller.OnReplayEvent += GameController.Instance.GetFlowController.ReplayLevel;

		controller.Init(a_Mission, a_Progression);
	}
	#endregion

	#region Option Menu
	public void DisplayOptionMenu()
	{
		if(m_OptionMenuUIController == null)
		{
			GameObject go = CreateScreen(m_OptionMenuUIControllerPrefab);
			m_OptionMenuUIController = go.GetComponent<OptionMenuUIController>();
			m_OptionMenuUIController.OnExitEvent += GameController.Instance.GetFlowController.ReturnWorldMap;
			m_OptionMenuUIController.OnResumeEvent += GameController.Instance.GetFlowController.ResumeGame;
			m_OptionMenuUIController.Init();
		}
	}

	public void RemoveOptionMenu()
	{
		if(m_OptionMenuUIController != null)
		{
			Destroy(m_OptionMenuUIController.gameObject);
		}
	}
	#endregion
}
