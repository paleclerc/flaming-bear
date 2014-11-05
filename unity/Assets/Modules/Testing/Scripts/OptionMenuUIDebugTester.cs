using UnityEngine;
using System.Collections;

public class OptionMenuUIDebugTester : MonoBehaviour 
{
	public OptionMenuUIController m_Controller;
	public OptionMenuUIModel m_Model;

	void Start()
	{
		m_Controller.OnExitEvent += OnExitEvent;
		m_Controller.OnResumeEvent += OnResumeEvent;
	}

	void OnGUI() 
	{
		if (GUI.Button(new Rect(0, 0, 200, 100), "Inject Data"))
		{
			m_Controller.InjectData(m_Model);
		}
	}

	void OnExitEvent ()
	{
		Debug.Log ("PAL :: OptionMenuUIDebugTester :: OnExitEvent");
	}

	void OnResumeEvent ()
	{
		Debug.Log ("PAL :: OptionMenuUIDebugTester :: OnResumeEvent");
	}
}
