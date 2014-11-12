using UnityEngine;
using System.Collections;
using System;

public class MissionTypeGemDestroyUIController : MissionTypeBaseUIController
{
	private const string TEXT_TO_REPLACE = "{0}\nof\n{1}";

	public MissionTypeGemDestroyUIModel myModel {get {return (MissionTypeGemDestroyUIModel)m_Model;}}

	protected override MissionTypeBaseUIModel CreateModel (MissionTypeBase a_MissionType)
	{
		if(!(a_MissionType is MissionTypeGemDestroy))
		{
			Debug.LogError("ERROR :: Mission Type do not fit, need a mission type : "+typeof(MissionTypeGemDestroy));
			return null;
		}
		MissionTypeGemDestroy myMissionType = (MissionTypeGemDestroy)a_MissionType;

		MissionTypeGemDestroyUIModel model = new MissionTypeGemDestroyUIModel();
		
		model.m_TotalGemQuantity = myMissionType.m_QuantityToDestroy;

		return model;
	}

	public override void UpdateMission (MissionProgression a_Progression)
	{
		myModel.m_CurrentGemQuantity = a_Progression.m_TotalGemDestroy;

		base.UpdateMission (a_Progression);
	}

	protected override bool GetIsCompleted ()
	{
		return (myModel.m_CurrentGemQuantity >= myModel.m_TotalGemQuantity);
	}

	override protected string GetDisplayText()
	{	
		return string.Format(TEXT_TO_REPLACE,StringUtil.SeparateThousand(myModel.m_CurrentGemQuantity),StringUtil.SeparateThousand(myModel.m_TotalGemQuantity));
	}


}
