using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ResultScreenUIView : MonoBehaviour 
{
	public Action OnContinueClick = delegate { };
	public Action OnReplayClick = delegate { };
	public Action OnLeaveClick = delegate { };

	virtual public void UpdateVisual(ResultScreenUIModel a_Model)
	{

	}
	
}
