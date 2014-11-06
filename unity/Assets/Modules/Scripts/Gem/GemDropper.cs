using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GemDropper : MonoBehaviour 
{
	public bool m_DebugDisplayGemSlotLink = false;
	public float m_DropCooldown = 1;

	public GemSlot m_GemSlot;
	private float m_WaitingTimeDrop;
	public GameObject m_GemContainer;

	public bool m_CanDrop = false;
	public int m_DropTableIndex = -1;

	// Use this for initialization
	public void Init()
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

		Gem gem = GemUtil.CreateGemAtPosition(m_DropTableIndex, transform.position);
		gem.GotoToSlot(m_GemSlot);

		GameController.Instance.GetFlowController.NewGemDropped();
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
	public GemEnum m_GemType;
	public int m_DropChanceWeight;
}