using UnityEngine;
using System.Collections;

public class MainMenuDebug : MonoBehaviour 
{
	void Start()
	{
		SceneManager.Instance.SceneLoadingCompleted();
	}

	void OnGUI() 
	{
		if (GUI.Button(new Rect(300, 300, 300, 150), "PLAY"))
		{
			SceneManager.Instance.ChangeScene(SceneManager.Instance.m_SceneName.Gameplay);
		}
	}
}
