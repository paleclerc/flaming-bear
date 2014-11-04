using UnityEngine;
using System.Collections;
using System;

public class MiniTweener : MonoBehaviour
{
	public Action OnAnimationCompleted = delegate { };

	public Vector3 m_ScaleStart;
	public Vector3 m_ScaleEnd;

	public int m_Duration;
	private Vector3 m_MovementByFrame;
	private int m_CurrentDuration;
	private Vector3 m_Target;
	private RectTransform m_MovingRectTransform;
	
	public void StartMoveFoward()
	{
		StartMove(m_ScaleStart, m_ScaleEnd);
	}

	public void StartMoveBackward()
	{
		StartMove(m_ScaleEnd, m_ScaleStart);
	}

	private void StartMove(Vector3 a_StartValue, Vector3 a_EndValue)
	{
		if(m_MovingRectTransform == null)
		{
			m_MovingRectTransform = GetComponent<RectTransform>();
		}
		m_MovingRectTransform.localScale = a_StartValue;
		m_Target = a_EndValue;
		Vector3 delta = a_EndValue - a_StartValue;
		m_MovementByFrame = delta / ((float)m_Duration);
		m_CurrentDuration = m_Duration;
	}

	// Update is called once per frame
	void Update () 
	{
		if(m_CurrentDuration > 0)
		{

			m_CurrentDuration --;
			if(m_CurrentDuration == 0)
			{
				m_MovingRectTransform.localScale = m_Target;
				OnAnimationCompleted();
			}
			else
			{
				m_MovingRectTransform.localScale += m_MovementByFrame;
			}
		}
	}
}
