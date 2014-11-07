using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DebugInfoUIView : MonoBehaviour 
{
	public Text m_FpsText;
	public Text m_VersionText;

	public void UpdateVisual(DebugInfoUIModel a_Model)
	{
		m_VersionText.text = a_Model.m_VersionNumber;
		m_FpsText.text = a_Model.m_CurrentFPS.ToString();
		m_FpsText.color = a_Model.m_VersionColor;
	}
	
}
