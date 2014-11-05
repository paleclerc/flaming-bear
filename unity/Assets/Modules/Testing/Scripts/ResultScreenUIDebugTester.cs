using UnityEngine;
using System.Collections;

public class ResultScreenUIDebugTester : MonoBehaviour 
{
	public ResultScreenUIController m_Controller;
	public ResultScreenUIModel m_Model;

	void Start()
	{
		m_Controller.OnContinueEvent += OnContinueEvent;
		m_Controller.OnReplayEvent += OnReplayEvent;
		m_Controller.OnLeaveEvent += OnLeaveEvent;
	}

	void OnGUI() 
	{
		if (GUI.Button(new Rect(0, 0, 200, 100), "Inject Data"))
		{
			m_Controller.InjectData(m_Model);
		}
	}

	void OnContinueEvent ()
	{
		Debug.Log ("PAL :: ResultScreenUIDebugTester :: OnContinueEvent");
	}

	void OnReplayEvent ()
	{
		Debug.Log ("PAL :: ResultScreenUIDebugTester :: OnReplayEvent");
	}

	void OnLeaveEvent ()
	{
		Debug.Log ("PAL :: ResultScreenUIDebugTester :: OnLeaveEvent");
	}
}
