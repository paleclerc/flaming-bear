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
			SceneManager.Instance.ChangeScene(SceneInfoConfigSO.Instance.SCENE_NAME.Gameplay);
		}
	}
}
