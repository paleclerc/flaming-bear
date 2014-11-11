using UnityEngine;
using System.Collections;

public class MissionTypeScoreTester : MonoBehaviour 
{
	public MissionTypeScoreUIController m_Controller;
	public MissionTypeScoreUIModel m_Model;

	public MissionProgression m_MissionProgression;
	public MissionTypeScore m_MissionTypeScore;
	public 

	void OnGUI() 
	{
		if (GUI.Button(new Rect(0, 0, 200, 100), "Inject Data"))
		{
			m_Controller.InjectData(m_Model);
		}

		if (GUI.Button(new Rect(0, 125, 200, 100), "Init with Bad Mission type"))
		{
			m_Controller.Init(new MissionTypeBase(), m_MissionProgression);
		}

		if (GUI.Button(new Rect(0, 250, 200, 100), "Init with Debug Mission"))
		{
			m_Controller.Init(m_MissionTypeScore, m_MissionProgression);
		}

		
		if (GUI.Button(new Rect(0, 375, 200, 100), "Update Mission Progression"))
		{
			m_Controller.UpdateMission(m_MissionProgression);
		}
	}

}
