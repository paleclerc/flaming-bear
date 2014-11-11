using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MissionTypeScoreUIView : MonoBehaviour 
{
	public Image m_BackgroundImageToChanceColor;
	public Image m_Icon;
	public Text m_DisplayText;

	public Sprite m_UnCompletedImage;
	public Sprite m_CompletedImage;

	public Color m_UncompletedColor;
	public Color m_CompletedColor;

	public void UpdateVisual(MissionTypeScoreUIModel a_Model)
	{
		if(a_Model.m_Completed)
		{
			m_Icon.sprite = m_CompletedImage;
			m_BackgroundImageToChanceColor.color = m_CompletedColor;
		}
		else
		{
			m_Icon.sprite = m_UnCompletedImage;
			m_BackgroundImageToChanceColor.color  = m_UncompletedColor;
		}

		m_DisplayText.text = a_Model.m_DisplayText;
	}

}
