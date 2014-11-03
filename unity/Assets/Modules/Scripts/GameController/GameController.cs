using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
	
	#region Content
	public GameObject m_FlowControllerPrefab;
	private FlowController m_FlowControllerComponent;
	public FlowController GetFlowController {get {return m_FlowControllerComponent;}}
	
	public GameObject m_InputControllerPrefab;
	private InputController m_InputControllerComponent;
	public InputController GetInputController {get {return m_InputControllerComponent;}}
	#endregion

	public Action OnGameControllerInitialized = delegate { };
	public bool m_IsControllerInitialized;

	#region Singleton
	static private GameController m_Instance;
	static public GameController Instance{get{return m_Instance;}}
	void Awake()
	{
		m_Instance = this;
	}
	#endregion

	// Use this for initialization
	void Start () 
	{
		CreateGameController();
	}

	public void CreateGameController()
	{
		CreateController<FlowController>(m_FlowControllerPrefab, ref m_FlowControllerComponent);
		CreateController<InputController>(m_InputControllerPrefab, ref m_InputControllerComponent);

		OnGameControllerInitialized();
	}

	static void CreateController<T>(GameObject a_Prefab, ref T a_ControllerContainer) where T : Component
	{
		if(a_ControllerContainer == null)
		{
			GameObject go = (GameObject)Instantiate(a_Prefab);
			if(go != null)
			{
				go.transform.parent = m_Instance.gameObject.transform;
				a_ControllerContainer = go.GetComponent<T>();
				if(a_ControllerContainer == null)
				{
					Debug.LogError("Error :: Controller Get Component :: Controller type =" +  typeof(T));
				}
			}
			else
			{
				Debug.LogError("Error :: Controller creation :: Controller type =" +  typeof(T));
			}
		}
	}
}

