using UnityEngine;
using System.Collections;

public class DebugInfoUIDebugTester : MonoBehaviour 
{
	public DebugInfoUIController m_Controller;
	public string m_VersionNumber;

	void Start()
	{
		m_Controller.Init();
	}

	void OnGUI() 
	{

	}

}
