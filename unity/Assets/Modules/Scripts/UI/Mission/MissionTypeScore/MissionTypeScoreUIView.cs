using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MissionTypeScoreUIView : MissionTypeBaseUIView 
{
	public Sprite m_UnCompletedImage;
	
	protected override Sprite GetUnCompletedIcon (MissionTypeBaseUIModel a_Model)
	{
		return m_UnCompletedImage;
	}

}
