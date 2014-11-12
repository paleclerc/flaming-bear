using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultScreenWinUIView : ResultScreenUIView 
{

	override public void UpdateVisual(ResultScreenUIModel a_Model)
	{
	}

	public void ContinueButtonClick()
	{
		OnContinueClick();
	}

	public void LeaveButtonClick()
	{
		OnLeaveClick();
	}

}
