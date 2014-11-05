using UnityEngine;
using System.Collections;
using System;

public class OptionMenuUIController : MonoBehaviour
{
	public OptionMenuUIView m_View;
	private OptionMenuUIModel m_Model;

	public Action OnExitEvent = delegate { };
	public Action OnResumeEvent = delegate { };

	void Start()
	{
		m_View.OnExitClick += OnExitClick;
		m_View.OnResumeClick += OnResumeClick;
	}

	public void Init()
	{
		m_Model = new OptionMenuUIModel();

		UpdateView();
	}

	void UpdateView ()
	{
		m_View.UpdateVisual(m_Model);
	}
	
	//Only here as a testing purpose
	public void InjectData (OptionMenuUIModel a_Model)
	{
		m_Model = a_Model;

		UpdateView();
	}

	void OnExitClick ()
	{
		OnExitEvent();
	}
	
	void OnResumeClick ()
	{
		OnResumeEvent();
	}
}
