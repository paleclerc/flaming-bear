using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MissionTypeGemDestroyedUIView : MissionTypeBaseUIView 
{
	protected override Sprite GetUnCompletedIcon (MissionTypeBaseUIModel a_Model)
	{
		MissionTypeGemDestroyedUIModel myModel =(MissionTypeGemDestroyedUIModel)a_Model;
		return GemConfigSO.Instance.GetGemVisualByType(myModel.m_GemType);
	}

}