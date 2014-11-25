using UnityEngine;
using System.Collections;

public class PowerupMenuUIDebugTester : MonoBehaviour 
{
	public PowerupMenuUIController m_Controller;
	public PowerupMenuUIModel m_Model;

	void Start()
	{
		m_Controller.OnCancelEvent += OnCancelEvent;
		m_Controller.OnPowerupEvent += OnPowerupEvent;
	}

	void OnGUI() 
	{
		if (GUI.Button(new Rect(0, 0, 200, 100), "Inject Data"))
		{
			m_Controller.InjectData(m_Model);
		}
	}

	void OnCancelEvent ()
	{
		Debug.Log ("PAL :: OptionMenuUIDebugTester :: OnCancelEvent");
	}

	void OnPowerupEvent (PowerupType a_PowerupType)
	{
		Debug.Log ("PAL :: OptionMenuUIDebugTester :: a_PowerupType = "+a_PowerupType.ToString());
	}
}
