using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GemDropper : MonoBehaviour 
{

	public bool m_DebugDisplayGemSlotLink = false;
	public float m_DropCooldown = 1;
	public List<GemDropTableItem> m_GemDropTable;
	public float m_DropSpeed = 1;

	public GemSlot m_GemSlot;
	private float m_WaitingTimeDrop;
	public GameObject m_GemContainer;

	public bool m_CanDrop = true;
	// Use this for initialization
	void Start ()
	{
		m_WaitingTimeDrop = m_DropCooldown;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_CanDrop)
		{
			m_WaitingTimeDrop -= Time.deltaTime;
			if(m_WaitingTimeDrop < 0)
			{
				m_WaitingTimeDrop = 0;

				if(!m_GemSlot.GetHaveGem())
				{
					DropGem();
				}
			}
		}
	}

	public void Dispose ()
	{
		m_CanDrop = false;
		m_GemSlot = null;
		m_GemContainer = null;
	}

	void DropGem ()
	{
		m_WaitingTimeDrop = m_DropCooldown;

		GameObject gemToCreate = FindGemToCreate();

		GameObject gemGO = (GameObject)Instantiate(gemToCreate);
		if(m_GemContainer != null)
		{
			gemGO.transform.parent = m_GemContainer.transform;
			gemGO.transform.position = transform.position;
		}
		Gem gem = gemGO.GetComponent<Gem>();
		gem.m_DroppingSpeed = m_DropSpeed;
		gem.GotoToSlot(m_GemSlot);

		GameController.Instance.GetFlowController.NewGemDropped();
	}

	GameObject FindGemToCreate ()
	{

		int maxValue = 0;
		foreach (GemDropTableItem item in m_GemDropTable)
		{
			maxValue += item.m_DropChanceWeight;
		}
		int random = Mathf.FloorToInt((UnityEngine.Random.value * maxValue));

		int tempValue = 0;
		foreach (GemDropTableItem item in m_GemDropTable)
		{
			tempValue += item.m_DropChanceWeight;
			if(tempValue > random)
			{
				return item.m_GemPrefab;
			}
		}

		//if not found
		return m_GemDropTable[0].m_GemPrefab;
	}

	public void AddGemSlot(GemSlot a_GemSlot)
	{
		m_GemSlot = a_GemSlot;
	}

	void OnDrawGizmos()
	{
		if(m_DebugDisplayGemSlotLink)
		{
			if(m_GemSlot != null)
			{
				Gizmos.DrawLine(transform.position,m_GemSlot.transform.position);
			}
		}
	}
}

[Serializable]
public class GemDropTableItem
{
	public GameObject m_GemPrefab;
	public int m_DropChanceWeight;
}