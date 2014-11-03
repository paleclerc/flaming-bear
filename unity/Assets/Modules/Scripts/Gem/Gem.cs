using UnityEngine;
using System.Collections;

public class Gem : MonoBehaviour {

	public float m_DroppingSpeed = 1;
	public GemInfo m_GemInfo;

	private GemSlot m_GemSlot;

	private Transform myTransform;
	private bool m_IsMoving;
	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_IsMoving)
		{
			Vector3 movement = m_GemSlot.transform.position - myTransform.position;
			if(movement.magnitude < m_DroppingSpeed)
			{
				m_IsMoving = false;
				myTransform.position = m_GemSlot.transform.position;
				CheckNextGemSlot();
			}
			else
			{
				myTransform.Translate((movement.normalized * m_DroppingSpeed));
			}
		}
	}

	public void Dispose()
	{
		m_IsMoving = false;
		m_GemSlot = null;
		myTransform = null;
	}

	public void GotoToSlot (GemSlot a_GemSlot)
	{
		if(m_GemSlot != null)
		{
			m_GemSlot.m_Gem = null;
		}
		m_IsMoving = true;
		GemSlot oldGemSlot = m_GemSlot; 
		m_GemSlot = a_GemSlot;
		m_GemSlot.m_Gem = this;

		if(oldGemSlot != null)
		{
			oldGemSlot.MakeTopGemDrop();
		}
	}

	void CheckNextGemSlot ()
	{
		GemSlot gemSlot = m_GemSlot.GetDroppingSlot();
		if(gemSlot != null)
		{
			GotoToSlot(gemSlot);
		}
	}

	public bool IsMoving ()
	{
		return m_IsMoving;
	}

	public void CheckIfCanDrop()
	{
		m_IsMoving = true;
	}
}
