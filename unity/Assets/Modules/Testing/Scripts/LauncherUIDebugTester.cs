using UnityEngine;
using System.Collections;

public class LauncherUIDebugTester : MonoBehaviour 
{
	public LauncherUIController m_Controller;
	public LauncherUIModel m_Model;

	void Start()
	{
		m_Controller.OnPlayEvent += OnPlayEvent;
	}

	void OnGUI() 
	{
		if (GUI.Button(new Rect(0, 0, 200, 100), "Inject Data"))
		{
			m_Controller.InjectData(m_Model);
		}
	}

	void OnPlayEvent ()
	{
		Debug.Log ("PAL :: LauncherUIDebugTester :: OnPlayEvent");
	}

}
