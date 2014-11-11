using UnityEngine;
using System.Collections;
using System;

public class MissionTypeBaseUIController : MonoBehaviour
{
	public MissionTypeBaseUIView m_View;
	protected MissionTypeBaseUIModel m_Model;
	private const string TEXT_TO_REPLACE = "{0}\nof\n{1}";
	
	virtual public void Init (MissionTypeBase a_MissionType, MissionProgression a_Progression)
	{
		m_Model = CreateModel(a_MissionType);
		UpdateMission(a_Progression);
	}

	virtual protected MissionTypeBaseUIModel CreateModel(MissionTypeBase a_MissionType)
	{
		return null;
	}

	virtual public void UpdateMission (MissionProgression a_Progression)
	{
		UpdateView();
	}

	private void UpdateView ()
	{
		m_Model.m_Completed = GetIsCompleted();
		m_Model.m_DisplayText = GetDisplayText();
		m_View.UpdateVisual(m_Model);
	}

	virtual protected bool GetIsCompleted()
	{
		return true;
	}

	virtual protected string GetDisplayText()
	{	
		return string.Empty;
	}

	//Only here as a testing purpose
	public void InjectData (MissionTypeBaseUIModel a_Model)
	{
		m_Model = a_Model;

		UpdateView();
	}

}
