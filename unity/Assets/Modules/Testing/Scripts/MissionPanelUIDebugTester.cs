using UnityEngine;
using System.Collections;

public class MissionPanelUIDebugTester : MonoBehaviour 
{
	public MissionPanelUIController m_Controller;
	public MissionPanelUIModel m_Model;
	
	void OnGUI() 
	{
		if (GUI.Button(new Rect(0, 0, 200, 100), "Inject Data"))
		{
			m_Controller.InjectData(m_Model);
		}
	}


}
