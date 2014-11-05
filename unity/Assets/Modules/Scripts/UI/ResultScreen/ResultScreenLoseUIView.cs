﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultScreenLoseUIView : ResultScreenUIView 
{
	public Text m_ScoreText;
	public Text m_TargetScoreText;

	override public void UpdateVisual(ResultScreenUIModel a_Model)
	{
		m_ScoreText.text = StringUtil.SeparateThousand(a_Model.m_Score);
		m_TargetScoreText.text = StringUtil.SeparateThousand(a_Model.m_TargetScore);
	}

	public void ReplayButtonClick()
	{
		OnReplayClick();
	}
	
	public void LeaveButtonClick()
	{
		OnLeaveClick();
	}
}