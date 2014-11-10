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
		m_Container.offsetMin = Vector2.zero;
		m_Container.offsetMax = Vector2.zero;

		Vector2 size = m_Container.anchorMax - m_Container.anchorMin;
		m_Container.anchorMin = a_Model.m_Position;
		m_Container.anchorMax = a_Model.m_Position + size;
	}

	public void OnLaunchButtonClick()
	{
		OnStartLevelButtonClick(m_Model.m_LevelToTrigger);
	}	
}
