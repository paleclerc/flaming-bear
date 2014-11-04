using UnityEngine;
using System.Collections;

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

	public GameObject m_DefaultTransition;
	public string m_EmptySceneTransition;
	private bool m_BetweenScene;
	private bool m_SceneLoadingCompleted;

	public void ChangeScene(string a_SceneName)
	{
		if(!m_BetweenScene)
		{
			m_BetweenScene = true;
			StartCoroutine("ChangeSceneCoroutine", a_SceneName);
		}
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

		Application.LoadLevel(m_EmptySceneTransition); 

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
	}

	public void SceneLoadingCompleted()
	{
		m_SceneLoadingCompleted = true;
	}
}
