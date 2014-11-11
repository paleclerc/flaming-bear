using UnityEngine;
using System.Collections;
using System;

public class MissionTypeScoreUIController : MissionTypeBaseUIController
{
	private const string TEXT_TO_REPLACE = "{0}\nof\n{1}";

	public MissionTypeScoreUIModel myModel {get {return (MissionTypeScoreUIModel)m_Model;}}

	protected override MissionTypeBaseUIModel CreateModel (MissionTypeBase a_MissionType)
	{
		if(!(a_MissionType is MissionTypeScore))
		{
			Debug.LogError("ERROR :: Mission Type do not fit, need a mission type : "+typeof(MissionTypeScore));
			return null;
		}
		MissionTypeScore myMissionType = (MissionTypeScore)a_MissionType;

		MissionTypeScoreUIModel model = new MissionTypeScoreUIModel();
		model.m_TotalScore = myMissionType.m_MinScore;

		return model;
	}

	public override void UpdateMission (MissionProgression a_Progression)
	{
		myModel.m_CurrentScore = a_Progression.m_Score;

		base.UpdateMission(a_Progression);
	}

	override protected bool GetIsCompleted()
	{
		return (myModel.m_CurrentScore >= myModel.m_TotalScore);
	}
	
	override protected string GetDisplayText()
	{	
		return string.Format(TEXT_TO_REPLACE,StringUtil.SeparateThousand(myModel.m_CurrentScore),StringUtil.SeparateThousand(myModel.m_TotalScore));
	}

}
