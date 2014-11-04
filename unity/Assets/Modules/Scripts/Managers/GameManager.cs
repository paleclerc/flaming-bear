using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject m_SceneManagerPrefab;
	static bool m_AlreadyExist = false;
	// Use this for initialization
	void Start () 
	{
		if(!m_AlreadyExist)
		{
			m_AlreadyExist = true;
			DontDestroyOnLoad(gameObject);
			CreateManager<SceneManager>(m_SceneManagerPrefab);
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
