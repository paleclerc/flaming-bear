using UnityEngine;
using System.Collections;

public class GameUIFlowController : MonoBehaviour 
{
	public GameObject m_ResultScreenWinUIControllerPrefab;
	public GameObject m_ResultScreenLoseUIControllerPrefab;

	public GameObject m_GameUIControllerPrefab;
	private GameUIController m_GameUIController;

	// Use this for initialization
	void Start () 
	{
		GameObject go = CreateScreen(m_GameUIControllerPrefab);
		m_GameUIController = go.GetComponent<GameUIController>();

		m_GameUIController.Init(GameController.Instance.GetFlowController.GetRamainingMove());
	}

	private GameObject CreateScreen(GameObject a_Prefab)
	{
		GameObject go = (GameObject)Instantiate(a_Prefab);
		go.transform.parent = this.transform;

		return go;
	}

	
	public void UpdateScore(int a_Value)
	{
		m_GameUIController.UpdateScore(a_Value);
	}
	
	public void UpdateMove(int a_Value)
	{
		m_GameUIController.UpdateMove(a_Value);
	}

	public void DisplayResultScreen(bool a_IsWin, int a_Score, int a_TargetScore)
	{
		GameObject go = null;

		if(a_IsWin)
		{
			go = CreateScreen(m_ResultScreenWinUIControllerPrefab);
		}
		else
		{
			go = CreateScreen(m_ResultScreenLoseUIControllerPrefab);
		}

		ResultScreenUIController controller = go.GetComponent<ResultScreenUIController>();

		controller.OnContinueEvent += GameController.Instance.GetFlowController.LeaveLevel;
		controller.OnLeaveEvent += GameController.Instance.GetFlowController.LeaveLevel;

		controller.OnReplayEvent += GameController.Instance.GetFlowController.ReplayLevel;

		controller.Init(a_Score, a_TargetScore);
	}
}
