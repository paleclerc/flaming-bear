using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MissionTypeBaseUIView : MonoBehaviour 
{
	public Image m_BackgroundImageToChanceColor;
	public Image m_Icon;
	public Text m_DisplayText;

	public Sprite m_CompletedImage;
	public Color m_UncompletedColor;
	public Color m_CompletedColor;

	virtual public void UpdateVisual(MissionTypeBaseUIModel a_Model)
	{
		if(a_Model.m_Completed)
		{
			m_Icon.sprite = m_CompletedImage;
			m_BackgroundImageToChanceColor.color = m_CompletedColor;
		}
		else
		{
			m_Icon.sprite = GetUnCompletedIcon(a_Model);
			m_BackgroundImageToChanceColor.color  = m_UncompletedColor;
		}

		m_DisplayText.text = a_Model.m_DisplayText;
	}

	virtual protected Sprite GetUnCompletedIcon(MissionTypeBaseUIModel a_Model)
	{
		return null;
	}
	

}
