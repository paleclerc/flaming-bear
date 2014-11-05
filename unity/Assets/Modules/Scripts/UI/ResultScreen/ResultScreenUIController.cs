using UnityEngine;
using System.Collections;
using System;

public class ResultScreenUIController : MonoBehaviour
{
	public ResultScreenUIView m_View;
	private ResultScreenUIModel m_Model;

	public Action OnContinueEvent = delegate { };
	public Action OnReplayEvent = delegate { };
	public Action OnLeaveEvent = delegate { };

	void Start()
	{
		m_View.OnContinueClick += OnContinueClick;
		m_View.OnReplayClick += OnReplayClick;
		m_View.OnLeaveClick += OnLeaveClick;
	}

	public void Init(int a_Score, int a_TargetScore)
	{
		m_Model = new ResultScreenUIModel();
		m_Model.m_Score = a_Score;
		m_Model.m_TargetScore = a_TargetScore;

		UpdateView();
	}

	void UpdateView ()
	{
		m_View.UpdateVisual(m_Model);
	}
	
	//Only here as a testing purpose
	public void InjectData (ResultScreenUIModel a_Model)
	{
		m_Model = a_Model;

		UpdateView();
	}

	void OnContinueClick ()
	{
		OnContinueEvent();
	}
	
	void OnReplayClick ()
	{
		OnReplayEvent();
	}
	void OnLeaveClick ()
	{
		OnLeaveEvent();
	}
}
