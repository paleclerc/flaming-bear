using UnityEngine;
using System.Collections;
using System;

public class PowerupMenuUIController : MonoBehaviour
{
	public PowerupMenuUIView m_View;
	private PowerupMenuUIModel m_Model;

	public Action OnCancelEvent = delegate { };
	public Action<PowerupType> OnPowerupEvent = delegate { };

	void Start()
	{
		m_View.OnPowerupClick += OnPowerupClick;
	}

	public void Init()
	{
		m_Model = new PowerupMenuUIModel();

		UpdateView();
	}

	void UpdateView ()
	{
		m_View.UpdateVisual(m_Model);
	}
	
	//Only here as a testing purpose
	public void InjectData (PowerupMenuUIModel a_Model)
	{
		m_Model = a_Model;

		UpdateView();
	}
	
	void OnPowerupClick (PowerupType a_PowerupType)
	{
		if(a_PowerupType == PowerupType.NONE)
		{
			OnCancelEvent();
		}
		else
		{
			OnPowerupEvent(a_PowerupType);
		}
	}
}

public enum PowerupType
{
	NONE = 0,
	BREAK_RANDOM_GEM = 1,
	BREAK_GEM_LINE = 2,
	BREAK_GEM_COLUMN = 3,
	BREAK_THREE_BY_THREE_GEM = 4,
	CHANGE_GEM_COLOR_RANDOM = 5,
	BREAK_ALL_ONE_TYPE_GEM = 6,
}
