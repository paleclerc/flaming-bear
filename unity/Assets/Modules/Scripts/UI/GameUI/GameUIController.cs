using UnityEngine;
using System.Collections;
using System;

public class GameUIController : MonoBehaviour
{
	public GameUIView m_View;
	private GameUIModel m_Model;
	public Action OnOptionEvent = delegate { };
	public Action OnPowerupEvent = delegate { };

	void Start()
	{
		m_View.OnOptionClick += OnOptionClick;
		m_View.OnPowerupClick += OnPowerupClick;
	}

	public void Init(Mission a_mission, MissionProgression a_MissionProgression)
	{
		m_Model = new GameUIModel();
		m_Model.m_Score = 0;
		m_Model.m_Mission = a_mission;

		UpdateProgression(a_MissionProgression);
	}

	void UpdateView ()
	{
		m_View.UpdateVisual(m_Model);
	}
	
	public void UpdateProgression(MissionProgression a_MissionProgression)
	{
		m_Model.m_MissionProgression = a_MissionProgression;

		m_Model.m_RemainingMove = m_Model.m_Mission.m_TotalMoveAvailable-a_MissionProgression.m_Move;
		if(m_Model.m_RemainingMove <= 0)
		{
			m_Model.m_RemainingMove = 0;
		}

		m_Model.m_Score = a_MissionProgression.m_Score;
		if(m_Model.m_Score <= 0)
		{
			m_Model.m_Score = 0;
		}

		UpdateView();
	}

	public void PowerupTarget(bool a_Display)
	{
		m_Model.m_PowerupTargetEnabled = a_Display;

		UpdateView();
	}

	//Only here as a testing purpose
	public void InjectData (GameUIModel a_Model)
	{
		m_Model = a_Model;

		UpdateView();
	}

	void OnOptionClick ()
	{
		OnOptionEvent();
	}

	void OnPowerupClick ()
	{
		OnPowerupEvent();
	}
}
