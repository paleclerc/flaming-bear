using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MissionTypeGemDestroyUIView : MissionTypeBaseUIView 
{
	
	public Sprite m_UnCompletedImage;
	protected override Sprite GetUnCompletedIcon (MissionTypeBaseUIModel a_Model)
	{
		return m_UnCompletedImage;
	}

}