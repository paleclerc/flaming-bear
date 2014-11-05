using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameUIView : MonoBehaviour 
{
	public Action OnOptionClick = delegate { };

	public Text m_ScoreText;
	public Text m_RemainingMoveText;

	public void UpdateVisual(GameUIModel a_Model)
	{
		m_ScoreText.text = StringUtil.SeparateThousand(a_Model.m_Score);
		m_RemainingMoveText.text = a_Model.m_RemainingMove.ToString();
	}

	public void OnOptionButtonClick()
	{
		OnOptionClick();
	}
}
