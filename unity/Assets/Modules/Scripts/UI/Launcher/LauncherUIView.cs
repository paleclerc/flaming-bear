using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LauncherUIView : MonoBehaviour 
{
	public Action OnPlayClick = delegate { };

	public void UpdateVisual(LauncherUIModel a_Model)
	{

	}

	public void OnPlayButtonClick()
	{
		OnPlayClick();
	}
	
}
