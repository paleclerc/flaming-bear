using UnityEngine;
using System.Collections;

public class WorldMapDisplay : MonoBehaviour 
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

		if(SceneManager.Instance != null)
		{
			SceneManager.Instance.SceneLoadingCompleted();
		}
	}

	void OnStartLevelEvent (string a_LevelName)
	{
		GameStartParam param = new GameStartParam();
		param.m_LevelId = a_LevelName;
		SceneManager.Instance.ChangeScene(SceneManager.Instance.m_SceneName.Gameplay, param);
	}
}
