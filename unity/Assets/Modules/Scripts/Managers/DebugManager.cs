using UnityEngine;
using System.Collections;
using System;

public class DebugManager : MonoBehaviour 
{
	#region Singleton
	static private DebugManager m_Instance;
	static public DebugManager Instance{get{return m_Instance;}}
	void Awake()
	{
		m_Instance = this;
	}
	#endregion

	public GameObject m_DebugUIPrefab;
	public string m_VersionNumber;

	void Start()
	{
		GameObject go = (GameObject)Instantiate(m_DebugUIPrefab);
		go.transform.parent = this.transform;

		DebugInfoUIController controller = go.GetComponent<DebugInfoUIController>();
		controller.Init();
	}

}
