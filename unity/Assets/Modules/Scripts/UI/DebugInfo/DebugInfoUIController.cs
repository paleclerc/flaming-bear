using UnityEngine;
using System.Collections;
using System;

public class DebugInfoUIController : MonoBehaviour
{
	public DebugInfoUIView m_View;
	public int m_TargetFPS = 60;

	private DebugInfoUIModel m_Model;
	private float m_TimeBefore = 0;

	public void Init(string a_Version)
	{
		m_Model = new DebugInfoUIModel();
		m_Model.m_VersionNumber = a_Version;
		m_Model.m_CurrentFPS = GetFps();

		UpdateView();
	}

	void UpdateView ()
	{
		m_Model.m_VersionColor = CalculateFpsColor(m_Model.m_CurrentFPS);
		m_View.UpdateVisual(m_Model);
	}

	Color CalculateFpsColor (int a_FPS)
	{
		return Color.Lerp(Color.red, Color.green, ((float)a_FPS / (float)m_TargetFPS));
	}

	int GetFps ()
	{
		return UnityEngine.Random.Range(0,60);
	}

	void Update () {

		if(Time.timeSinceLevelLoad  - m_TimeBefore <= 1)
		{
			m_Model.m_CurrentFPS++;
		}
		else
		{
			UpdateView();
//			m_LastFPS = m_Model.m_CurrentFPS + 1;
			m_TimeBefore= Time.timeSinceLevelLoad;
			m_Model.m_CurrentFPS = 0;
		}
	}
}
