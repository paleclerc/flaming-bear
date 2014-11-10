using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WorldMapItemTemplateController : MonoBehaviour 
{
	public Action<string> OnStartLevelEvent = delegate { };
	
	public WorldMapItemTemplateView m_View;
	private WorldMapItemTemplateModel m_Model;

	public void Init(WorldMapItem a_WorldMapItem)
	{
		m_Model = new WorldMapItemTemplateModel();
		m_Model.m_Image = 	a_WorldMapItem.m_IconSprite;
		m_Model.m_Position = a_WorldMapItem.m_RelativePositionPercent;
		m_Model.m_LevelToTrigger = a_WorldMapItem.m_LevelId;

		LevelItem levelItem = LevelConfigSO.Instance.GetLevelById(a_WorldMapItem.m_LevelId);
		if(levelItem == null)
		{
			Debug.LogError("Error While creating world map Item, Level not found : "+a_WorldMapItem.m_LevelId);
		}
		else
		{
			m_Model.m_LevelName = levelItem.m_DisplayName;
		}

		m_View.OnStartLevelButtonClick += OnStartLevelButtonClick;

		UpdateView();
	}
	
	void UpdateView ()
	{
		m_View.UpdateVisual(m_Model);
	}

	void OnStartLevelButtonClick (string a_LevelName)
	{
		OnStartLevelEvent(a_LevelName);
	}
}
