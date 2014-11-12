using UnityEngine;
using System.Collections;
using System;

public class MissionPanelUIController : MonoBehaviour
{
	public MissionPanelUIView m_View;
	private MissionPanelUIModel m_Model;

	public void Init(Mission a_Mission, MissionProgression a_MissionProgression)
	{
		m_Model = new MissionPanelUIModel();
		m_Model.m_Mission = a_Mission;
		m_Model.m_MissionProgression = a_MissionProgression;

		UpdateProgression(a_MissionProgression);
	}

	void UpdateView ()
	{
		m_View.UpdateVisual(m_Model);
	}
	
	public void UpdateProgression(MissionProgression a_MissionProgression)
	{
		UpdateView();
	}

	//Only here as a testing purpose
	public void InjectData (MissionPanelUIModel a_Model)
	{
		m_Model = a_Model;

		UpdateView();
	}
}
