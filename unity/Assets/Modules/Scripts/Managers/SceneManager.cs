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

	void Start()
	{
		StartCoroutine("WaitToExecuteInitTraitement");
	}

	private IEnumerator WaitToExecuteInitTraitement()
	{
		yield return 0;
		TryPlayMusic(Application.loadedLevelName);
	}

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

		Application.LoadLevel(SceneInfoConfigSO.Instance.SCENE_NAME.EmptyScene); 

		yield return 0;

		m_SceneLoadingCompleted = false;
		Application.LoadLevel(a_SceneName);

		while(!m_SceneLoadingCompleted)
		{
			yield return 0;
		}

		TryPlayMusic(a_SceneName);

		transition.StartTransitionOut();
		while(!transition.TransitionOutCompleted)
		{
			yield return 0;
		}

		Destroy(currentTransition);
		m_BetweenScene = false;
	}

	void TryPlayMusic (string a_SceneName)
	{
		SceneInfoConfigItem config = SceneInfoConfigSO.Instance.GetSceneInfoByName(a_SceneName);
		
		if((config != null) && (config.m_MusicToPlayName != string.Empty))
		{
			AudioManager.Instance.PlayAudioItem(config.m_MusicToPlayName);
		}
	}

	public void SceneLoadingCompleted()
	{
		m_SceneLoadingCompleted = true;
	}
}
