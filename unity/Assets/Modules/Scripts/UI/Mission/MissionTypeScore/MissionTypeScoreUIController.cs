using UnityEngine;
using System.Collections;
using System;

public class MissionTypeScoreUIController : MissionTypeBaseUIController
{
	public MissionTypeScoreUIView m_View;
	private MissionTypeScoreUIModel m_Model;
	private const string TEXT_TO_REPLACE = "{0}\nof\n{1}";
	
	public override void Init (MissionTypeBase a_MissionType, MissionProgression a_Progression)
	{
		if(!(a_MissionType is MissionTypeScore))
		{
			Debug.LogError("ERROR :: Mission Type do not fit, need a mission type : "+typeof(MissionTypeScore));
			return ;
		}
		MissionTypeScore myMissionType = (MissionTypeScore)a_MissionType;
		m_Model = new MissionTypeScoreUIModel();
		m_Model.m_TotalScore = myMissionType.m_MinScore;

		UpdateMission(a_Progression);
	}

	public override void UpdateMission (MissionProgression a_Progression)
	{
		m_Model.m_CurrentScore = a_Progression.m_Score;

		UpdateView();
	}

	void UpdateView ()
	{
		DetermineScoreInfo();
		m_View.UpdateVisual(m_Model);
	}

	void DetermineScoreInfo()
	{
		m_Model.m_Completed = (m_Model.m_CurrentScore >= m_Model.m_TotalScore);

		m_Model.m_DisplayText = string.Format(TEXT_TO_REPLACE,StringUtil.SeparateThousand(m_Model.m_CurrentScore),StringUtil.SeparateThousand(m_Model.m_TotalScore));
	}

	//Only here as a testing purpose
	public void InjectData (MissionTypeScoreUIModel a_Model)
	{
		m_Model = a_Model;

		UpdateView();
	}

}
