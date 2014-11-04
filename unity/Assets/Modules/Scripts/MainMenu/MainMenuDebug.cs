using UnityEngine;
using System.Collections;

public class MainMenuDebug : MonoBehaviour {

	public string m_PlayScene;

	void OnGUI() 
	{
		if (GUI.Button(new Rect(300, 300, 300, 150), "PLAY"))
		{
			SceneManager.Instance.ChangeScene(m_PlayScene);
		}
	}
}
