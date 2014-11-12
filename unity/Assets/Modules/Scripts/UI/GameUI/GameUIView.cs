using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameUIView : MonoBehaviour 
{
	public Action OnOptionClick = delegate { };

	public Text m_ScoreText;
	public Text m_RemainingMoveText;

	public GameObject m_MissionContainer;
	public GameObject m_MissionPanelUIPrefab;
	private MissionPanelUIController m_MissionPanelUIController;


	public void UpdateVisual(GameUIModel a_Model)
	{
		m_ScoreText.text = StringUtil.SeparateThousand(a_Model.m_Score);
		m_RemainingMoveText.text = a_Model.m_RemainingMove.ToString();

		if(m_MissionPanelUIController == null)
		{
			CreateMissionPanel(a_Model.m_Mission, a_Model.m_MissionProgression);
		}
		else
		{
			UpdateMissionPanel(a_Model.m_MissionProgression);
		}
	}

	void CreateMissionPanel (Mission a_Mission, MissionProgression a_MissionProgression)
	{
		GameObject go = (GameObject)Instantiate(m_MissionPanelUIPrefab);
		go.transform.parent = m_MissionContainer.transform;
		UIUtils.SetFullSize(go.GetComponent<RectTransform>());
		m_MissionPanelUIController = go.GetComponent<MissionPanelUIController>();

		m_MissionPanelUIController.Init(a_Mission, a_MissionProgression);
	}

	void UpdateMissionPanel (MissionProgression a_MissionProgression)
	{
		m_MissionPanelUIController.UpdateProgression(a_MissionProgression);
	}

	public void OnOptionButtonClick()
	{
		OnOptionClick();
	}
}