using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SceneInfoConfig : ScriptableObject {
	
	public SceneName SCENE_NAME;

	public List<SceneInfoConfigItem> m_ListSceneInfo;
	
	public SceneInfoConfigItem GetSceneInfoByName(string a_SceneName)
	{
		foreach (SceneInfoConfigItem item in m_ListSceneInfo)
		{
			if(item.m_SceneName == a_SceneName)
			{
				return item;
			}
		}

		Debug.LogWarning("Warning :: Scene Info Not Found for scene name = "+a_SceneName);
		return null;
	}

}

public class SceneInfoConfigSO : AccessorsSO<SceneInfoConfig>
{
	static public SceneInfoConfig Instance{get{return GetInternalInstance(ResourcesPathUtil.SCENE_INFO_CONFIG);}}
}


[Serializable]
public class SceneInfoConfigItem
{
	public string m_SceneName;
	public string m_MusicToPlayName;
}


[Serializable]
public class SceneName
{
	public string EmptyScene;
	public string MainMenu;
	public string Gameplay;
	public string WorldMap;
}