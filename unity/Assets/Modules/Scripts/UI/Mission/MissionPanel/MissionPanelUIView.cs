using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MissionPanelUIView : MonoBehaviour 
{
	public GameObject m_MissionContainer;
	public List<RectTransform> m_ListPositionMission;

	private List<MissionTypeBaseUIController> m_ListMissionController = null;

	public void UpdateVisual(MissionPanelUIModel a_Model)
	{
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

		GameObject prefab = MissionConfigSO.Instance.GetMissionPrefabByMissionType(a_MissionItem.m_MissionType);
		if(prefab != null)
		{
			GameObject go = (GameObject)Instantiate(prefab);
			go.transform.parent = m_MissionContainer.transform;
			controller = go.GetComponent<MissionTypeBaseUIController>();

			RectTransform newItemPosition = go.GetComponent<RectTransform>();
			UIUtils.CopyTransformToOtherTransform(newItemPosition, a_Position);
			controller.Init(a_MissionItem.GetCurrentMission(), a_MissionProgression);
		}
		else
		{
			Debug.LogError("ERROR :: Cannot create mission type UI correctly for type "+a_MissionItem.m_MissionType);
			return false;
		}
		return true;
	}
}