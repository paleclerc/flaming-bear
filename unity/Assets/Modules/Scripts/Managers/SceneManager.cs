using UnityEngine;
using System.Collections;
using System;

public class SceneManager : MonoBehaviour 
{
	#region Singleton
	static private SceneManager m_Instance;
	static public SceneManager Instance{get{return m_Instance;}}
	void Awake()
	{
		m_Instance = this;
	}
	#endregion

	public SceneName m_SceneName;
	public GameObject m_DefaultTransition;
	private bool m_BetweenScene;
	private bool m_SceneLoadingCompleted;
	private System.Object m_Param;

	public void ChangeScene(string a_SceneName, System.Object a_Param = null)
	{
		if(!m_BetweenScene)
		{
			m_Param = a_Param;
			m_BetweenScene = true;
			StartCoroutine("ChangeSceneCoroutine", a_SceneName);
		}
	}

	public System.Object GetParam()
	{
		return m_Param;
	}

	private IEnumerator ChangeSceneCoroutine(string a_SceneName)
	{

		GameObject currentTransition = (GameObject)Instantiate(m_DefaultTransition);
		currentTransition.transform.parent = transform;
		BaseTransition transition = currentTransition.GetComponent<BaseTransition>();

		transition.StartTransitionIn();
		while(!transition.TransitionInCompleted)
		{
			yield return 0;
		}

		Application.LoadLevel(m_SceneName.EmptyScene); 

		yield return 0;

		m_SceneLoadingCompleted = false;
		Application.LoadLevel(a_SceneName);

		while(!m_SceneLoadingCompleted)
		{
			yield return 0;
		}

		transition.StartTransitionOut();
		while(!transition.TransitionOutCompleted)
		{
			yield return 0;
		}

		Destroy(currentTransition);
		m_BetweenScene = false;
	}

	public void SceneLoadingCompleted()
	{
		m_SceneLoadingCompleted = true;
	}
}

[Serializable]
public class SceneName
{
	public string EmptyScene;
	public string MainMenu;
	public string Gameplay;
	public string WorldMap;
}