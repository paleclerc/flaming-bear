using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour 
{
	public GameObject m_LauncherUIControllerPrefab;
	private LauncherUIController m_LauncherUIController;

	// Use this for initialization
	void Start () 
	{
		GameObject go = CreateScreen(m_LauncherUIControllerPrefab);
		m_LauncherUIController = go.GetComponent<LauncherUIController>();

		m_LauncherUIController.OnPlayEvent += OnPlayEvent;
		m_LauncherUIController.Init();

		if(SceneManager.Instance != null)
		{
			SceneManager.Instance.SceneLoadingCompleted();
		}
	}

	private GameObject CreateScreen(GameObject a_Prefab)
	{
		GameObject go = (GameObject)Instantiate(a_Prefab);
		go.transform.parent = this.transform;
		
		return go;
	}

	void OnPlayEvent ()
	{
		SceneManager.Instance.ChangeScene(SceneInfoConfigSO.Instance.SCENE_NAME.WorldMap);
	}
}
