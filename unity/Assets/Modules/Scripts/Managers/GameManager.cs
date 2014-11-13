using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int m_TargetFrameRate = 60;
	public GameObject m_SceneManagerPrefab;
	public GameObject m_DebugManagerPrefab;
	public GameObject m_AudioManagerPrefab;
	static bool m_AlreadyExist = false;

	void Awake()
	{
		Application.targetFrameRate = m_TargetFrameRate;
	}
	// Use this for initialization
	void Start () 
	{
		if(!m_AlreadyExist)
		{
			m_AlreadyExist = true;
			DontDestroyOnLoad(gameObject);
			CreateManager<SceneManager>(m_SceneManagerPrefab);
			CreateManager<DebugManager>(m_DebugManagerPrefab);
			CreateManager<AudioManager>(m_AudioManagerPrefab);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void CreateManager<T>(GameObject a_Prefab) where T : Component
	{
		if(a_Prefab != null)
		{
			T manager = GetComponentInChildren<T>();
			if(manager == null)
			{
				GameObject go = (GameObject)Instantiate(a_Prefab);
				if(go != null)
				{
					go.transform.parent = transform;
				}
				else
				{
					Debug.LogError("Error :: Manager creation :: Manager type =" +  typeof(T));
				}
			}
		}
	}
}
