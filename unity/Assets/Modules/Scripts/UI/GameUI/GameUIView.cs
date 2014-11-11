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
	private List<MissionTypeBaseUIController> m_ListMissionController = null;

	public List<GameUIMissionType> m_ListTemplateMissionType;

	public List<RectTransform> m_ListPositionMission;


	public void UpdateVisual(GameUIModel a_Model)
	{
		m_ScoreText.text = StringUtil.SeparateThousand(a_Model.m_Score);
		m_RemainingMoveText.text = a_Model.m_RemainingMove.ToString();

		if(m_ListMissionController == null)
		{
			CreateListMission(a_Model.m_Mission.m_ListMissionItem, a_Model.m_MissionProgression);
		}
		else
		{
			UpdateListMission(a_Model.m_MissionProgression);
		}
	}

	void CreateListMission (List<MissionItem> a_ListMissionItem, MissionProgression a_MissionProgression)
	{
		int currentIndex = 0;
		foreach (MissionItem item in a_ListMissionItem)
		{
			if(currentIndex < m_ListPositionMission.Count)
			{
				bool added = CreateMissionController(item, a_MissionProgression, m_ListPositionMission[currentIndex]);
				if(added)
				{
					currentIndex++;
				}
			}
			else
			{
				Debug.LogError("Too Much Mission for the quantity of mission position");
			}

		}
	}

	void UpdateListMission (MissionProgression a_MissionProgression)
	{
		foreach (MissionTypeBaseUIController controller in m_ListMissionController)
		{
			controller.UpdateMission(a_MissionProgression);
		}
	}

	bool CreateMissionController (MissionItem a_MissionItem, MissionProgression a_MissionProgression, RectTransform a_Position)
	{
		MissionTypeBaseUIController controller = null;
		GameObject go = null;
		foreach (GameUIMissionType template in m_ListTemplateMissionType) 
		{
			if(template.m_MissionType == a_MissionItem.m_MissionType)
			{
				go = (GameObject)Instantiate(template.m_PrefabToCreate);
				go.transform.parent = m_MissionContainer.transform;
				if(go != null)
				{
					controller = go.GetComponent<MissionTypeBaseUIController>();
				}
			}
		}

		if(controller == null)
		{
			Debug.LogError("ERROR :: Cannot create mission type UI correctly for type "+a_MissionItem.m_MissionType);
			return false;
		}

		RectTransform newItemPosition = go.GetComponent<RectTransform>();
		UIUtils.CopyTransformToOtherTransform(newItemPosition, a_Position);
		controller.Init(a_MissionItem.GetCurrentMission(), a_MissionProgression);

		return true;
	}

	public void OnOptionButtonClick()
	{
		OnOptionClick();
	}
}


[Serializable]
public class GameUIMissionType
{
	public MissionType m_MissionType;
	public GameObject m_PrefabToCreate;
}
