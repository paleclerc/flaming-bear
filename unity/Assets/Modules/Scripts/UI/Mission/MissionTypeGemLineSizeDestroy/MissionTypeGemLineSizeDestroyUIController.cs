using UnityEngine;
using System.Collections;
using System;

public class MissionTypeGemLineSizeDestroyUIController : MissionTypeBaseUIController
{
	private const string TEXT_TO_REPLACE = "{0}\nof\n{1}";
	private const string TITLE_TO_REPLACE = "Line of {0}";

	public MissionTypeGemLineSizeDestroyUIModel myModel {get {return (MissionTypeGemLineSizeDestroyUIModel)m_Model;}}

	protected override MissionTypeBaseUIModel CreateModel (MissionTypeBase a_MissionType)
	{
		if(!(a_MissionType is MissionTypeGemLineSizeDestroy))
		{
			Debug.LogError("ERROR :: Mission Type do not fit, need a mission type : "+typeof(MissionTypeGemLineSizeDestroy));
			return null;
		}
		MissionTypeGemLineSizeDestroy myMissionType = (MissionTypeGemLineSizeDestroy)a_MissionType;

		MissionTypeGemLineSizeDestroyUIModel model = new MissionTypeGemLineSizeDestroyUIModel();

		model.m_QuantityInLine = myMissionType.m_QuantityInLine;
		model.m_TotalQuantity = myMissionType.m_QuantityToDestroy;

		return model;
	}

	public override void UpdateMission (MissionProgression a_Progression)
	{
		if(a_Progression.m_GemLineDestroy.ContainsKey(myModel.m_QuantityInLine))
		{
			myModel.m_CurrentQuantity = a_Progression.m_GemLineDestroy[myModel.m_QuantityInLine];
		}
		else
		{
			myModel.m_CurrentQuantity = 0;
		}

		myModel.m_TitleText = string.Format(TITLE_TO_REPLACE, myModel.m_QuantityInLine);

		base.UpdateMission (a_Progression);
	}

	protected override bool GetIsCompleted ()
	{
		return (myModel.m_CurrentQuantity >= myModel.m_TotalQuantity);
	}

	override protected string GetDisplayText()
	{	
		return string.Format(TEXT_TO_REPLACE,StringUtil.SeparateThousand(myModel.m_CurrentQuantity),StringUtil.SeparateThousand(myModel.m_TotalQuantity));
	}


}
