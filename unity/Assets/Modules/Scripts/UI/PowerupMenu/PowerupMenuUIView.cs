using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PowerupMenuUIView : MonoBehaviour 
{
	public Action<PowerupType> OnPowerupClick = delegate { };

	public void UpdateVisual(PowerupMenuUIModel a_Model)
	{

	}

	
	public void OnPowerupButtonClick(PowerupType a_PowerupType)
	{
		OnPowerupClick(a_PowerupType);
	}

	
	public void OnPowerupButtonClick(int a_PowerupTypeInt)
	{
		OnPowerupClick((PowerupType)a_PowerupTypeInt);
	}
}
