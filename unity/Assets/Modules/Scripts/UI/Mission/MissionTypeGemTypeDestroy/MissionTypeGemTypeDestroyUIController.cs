using UnityEngine;
using System.Collections;
using System;

public class MissionTypeGemTypeDestroyUIController : MissionTypeBaseUIController
{
	private const string TEXT_TO_REPLACE = "{0}\nof\n{1}";

	public MissionTypeGemTypeDestroyUIModel myModel {get {return (MissionTypeGemTypeDestroyUIModel)m_Model;}}

	protected override MissionTypeBaseUIModel CreateModel (MissionTypeBase a_MissionType)
	{
		if(!(a_MissionType is MissionTypeGemTypeDestroy))
		{
			Debug.LogError("ERROR :: Mission Type do not fit, need a mission type : "+typeof(MissionTypeGemTypeDestroy));
			return null;
		}
		MissionTypeGemTypeDestroy myMissionType = (MissionTypeGemTypeDestroy)a_MissionType;

		MissionTypeGemTypeDestroyUIModel model = new MissionTypeGemTypeDestroyUIModel();
		
		model.m_TotalGemQuantity = myMissionType.m_QuantityToDestroy;
		model.m_GemType = myMissionType.m_TypeGem;

		return model;
	}

	public override void UpdateMission (MissionProgression a_Progression)
	{
		if(a_Progression.m_GemDestroy.ContainsKey(myModel.m_GemType))
		{
			myModel.m_CurrentGemQuantity = a_Progression.m_GemDestroy[myModel.m_GemType];
		}

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
