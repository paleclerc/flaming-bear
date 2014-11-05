using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class OptionMenuUIView : MonoBehaviour 
{
	public Action OnResumeClick = delegate { };
	public Action OnExitClick = delegate { };

	public void UpdateVisual(OptionMenuUIModel a_Model)
	{

	}

	public void OnResumeButtonClick()
	{
		OnResumeClick();
	}

	
	public void OnExitButtonClick()
	{
		OnExitClick();
	}
}
