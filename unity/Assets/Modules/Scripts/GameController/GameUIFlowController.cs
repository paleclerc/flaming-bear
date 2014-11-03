using UnityEngine;
using System.Collections;

public class GameUIFlowController : MonoBehaviour 
{
	public GameObject m_GameUIControllerPrefab;
	private GameUIController m_GameUIController;

	// Use this for initialization
	void Start () 
	{
		GameObject go = (GameObject)Instantiate(m_GameUIControllerPrefab);
		go.transform.parent = this.transform;
		m_GameUIController = go.GetComponent<GameUIController>();

		m_GameUIController.Init(GameController.Instance.GetFlowController.GetRamainingMove());
	}

	
	public void UpdateScore(int a_Value)
	{
		m_GameUIController.UpdateScore(a_Value);
	}
	
	public void UpdateMove(int a_Value)
	{
		m_GameUIController.UpdateMove(a_Value);
	}
}
