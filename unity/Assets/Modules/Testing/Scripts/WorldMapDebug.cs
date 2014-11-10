using UnityEngine;
using System.Collections;

public class WorldMapDebug : MonoBehaviour 
{
	public GameObject m_WorldMapTemplate;

	// Use this for initialization
	void Start () 
	{
		WorldMapSection currentSection = WorldMapConfigSO.Instance.GetSectionByIndex(0);
		
		GameObject worldMapTemplateGO = (GameObject)GameObject.Instantiate(m_WorldMapTemplate);
		WorldMapSectionTemplateController worldMapTemplateController = worldMapTemplateGO.GetComponent<WorldMapSectionTemplateController>();
		worldMapTemplateController.Init(currentSection);
		worldMapTemplateController.OnStartLevelEvent+= OnStartLevelEvent;
	}

	void OnStartLevelEvent (string a_LevelName)
	{
		Debug.Log("PAL :: Level Started :: "+a_LevelName);
	}
}
