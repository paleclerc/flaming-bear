using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultScreenLoseUIView : ResultScreenUIView 
{
	
	public GameObject m_MissionContainer;
	public GameObject m_MissionPanelUIPrefab;
	private MissionPanelUIController m_MissionPanelUIController;

	override public void UpdateVisual(ResultScreenUIModel a_Model)
	{
		if(m_MissionPanelUIController == null)
		{
			CreateMissionPanel(a_Model.m_Mission, a_Model.m_Progression);
		}
		else
		{
			UpdateMissionPanel(a_Model.m_Progression);
		}
	}
	
	void CreateMissionPanel (Mission a_Mission, MissionProgression a_MissionProgression)
	{
		GameObject go = (GameObject)Instantiate(m_MissionPanelUIPrefab);
		go.transform.parent = m_MissionContainer.transform;
		UIUtils.SetFullSize(go.GetComponent<RectTransform>());
		m_MissionPanelUIController = go.GetComponent<MissionPanelUIController>();
		
		m_MissionPanelUIController.Init(a_Mission, a_MissionProgression);
	}
	
	void UpdateMissionPanel (MissionProgression a_MissionProgression)
	{
		m_MissionPanelUIController.UpdateProgression(a_MissionProgression);
	}

	public void ReplayButtonClick()
	{
		OnReplayClick();
	}
	
	public void LeaveButtonClick()
	{
		OnLeaveClick();
	}
}
