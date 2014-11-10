using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WorldMapSectionTemplateController : MonoBehaviour 
{
	public Action<string> OnStartLevelEvent = delegate { };

	public GameObject m_WorldMapItemTemplate;
	public WorldMapSectionTemplateView m_View;
	private WorldMapSectionTemplateModel m_Model;

	public void Init(WorldMapSection a_WorldMapSection)
	{

		m_Model = new WorldMapSectionTemplateModel();
		m_Model.m_Background = a_WorldMapSection.m_Background;

		CreateWorldMapItem(a_WorldMapSection.m_ListItem);

		UpdateView();
	}
	
	void UpdateView ()
	{
		m_View.UpdateVisual(m_Model);
	}
	

	void CreateWorldMapItem (List<WorldMapItem> m_ListItem)
	{
		foreach (WorldMapItem item in m_ListItem) 
		{
			GameObject worldMapItemGO = (GameObject)GameObject.Instantiate(m_WorldMapItemTemplate);
			worldMapItemGO.transform.parent = gameObject.transform;
			worldMapItemGO.transform.localPosition = Vector3.zero;
			WorldMapItemTemplateController controller = worldMapItemGO.GetComponent<WorldMapItemTemplateController>();
			
			controller.OnStartLevelEvent += OnStartLevelEventTriggered;
			controller.Init(item);
		}
	}

	void OnStartLevelEventTriggered (string a_LevelName)
	{
		OnStartLevelEvent(a_LevelName);
	}
}
