using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WorldMapItemTemplateView : MonoBehaviour
{
	public Action<string> OnStartLevelButtonClick = delegate { };
	public RectTransform m_Container;
	public Image m_ButtonImage;
	public Text m_DisplayNameText;

	private WorldMapItemTemplateModel m_Model;
	public float m_TextWaiting = 4;

	public void UpdateVisual(WorldMapItemTemplateModel a_Model)
	{
		m_Model = a_Model;
		m_ButtonImage.sprite = m_Model.m_Image;
		m_DisplayNameText.text = a_Model.m_LevelName;

		UIUtils.SetPositionCenteredUI(m_Container, a_Model.m_Position);
	}

	public void OnLaunchButtonClick()
	{
		OnStartLevelButtonClick(m_Model.m_LevelToTrigger);
	}	
}
