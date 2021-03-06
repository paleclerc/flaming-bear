﻿using UnityEngine;
using System.Collections;

public class GameUIDebugTester : MonoBehaviour 
{
	public GameUIController m_Controller;
	public GameUIModel m_Model;

	void Start()
	{
		m_Controller.OnOptionEvent += OnOptionEvent;
		m_Controller.OnPowerupEvent += OnPowerupEvent;
	}
	
	void OnGUI() 
	{
		if (GUI.Button(new Rect(400, 300, 200, 100), "Inject Data"))
		{
			m_Controller.InjectData(m_Model);
		}
	}

	void OnOptionEvent ()
	{
		Debug.Log ("PAL :: GameUIDebugTester :: OnOptionEvent");
	}

	void OnPowerupEvent ()
	{
		Debug.Log ("PAL :: GameUIDebugTester :: OnPowerupEvent");
	}

}
