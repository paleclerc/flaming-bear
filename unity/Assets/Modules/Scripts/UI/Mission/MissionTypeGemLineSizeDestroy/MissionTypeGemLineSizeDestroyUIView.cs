using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MissionTypeGemLineSizeDestroyUIView : MissionTypeBaseUIView 
{
	public Sprite m_UnCompletedImage;
	public Text m_Title;

	public override void UpdateVisual (MissionTypeBaseUIModel a_Model)
	{
		base.UpdateVisual (a_Model);
		MissionTypeGemLineSizeDestroyUIModel myModel = (MissionTypeGemLineSizeDestroyUIModel)a_Model;
		m_Title.text = myModel.m_TitleText;
	}
	protected override Sprite GetUnCompletedIcon (MissionTypeBaseUIModel a_Model)
	{
		return m_UnCompletedImage;
	}

}