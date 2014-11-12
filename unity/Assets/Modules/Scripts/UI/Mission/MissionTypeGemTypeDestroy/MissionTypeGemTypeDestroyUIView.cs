using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MissionTypeGemTypeDestroyUIView : MissionTypeBaseUIView 
{
	protected override Sprite GetUnCompletedIcon (MissionTypeBaseUIModel a_Model)
	{
		MissionTypeGemTypeDestroyUIModel myModel =(MissionTypeGemTypeDestroyUIModel)a_Model;
		return GemConfigSO.Instance.GetGemVisualByType(myModel.m_GemType);
	}

}