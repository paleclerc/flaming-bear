using UnityEngine;
using System.Collections;
using System;

public class LauncherUIController : MonoBehaviour
{
	public LauncherUIView m_View;
	private LauncherUIModel m_Model;

	public Action OnPlayEvent = delegate { };

	void Start()
	{
		m_View.OnPlayClick += OnPlayClick;
	}

	public void Init()
	{
		m_Model = new LauncherUIModel();

		UpdateView();
	}

	void UpdateView ()
	{
		m_View.UpdateVisual(m_Model);
	}
	
	//Only here as a testing purpose
	public void InjectData (LauncherUIModel a_Model)
	{
		m_Model = a_Model;

		UpdateView();
	}

	void OnPlayClick ()
	{
		OnPlayEvent();
	}

}
