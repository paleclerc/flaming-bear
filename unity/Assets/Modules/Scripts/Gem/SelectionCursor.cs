using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionCursor : MonoBehaviour {

	public List<SpriteRenderer> m_AnimationPart;
	
	private Animator m_Animator;
	

	public void DisplayCusor(bool a_IsDisplayed)
	{
		foreach (SpriteRenderer renderer in m_AnimationPart)
		{
			renderer.enabled = a_IsDisplayed;
		}

		if(m_Animator == null)
		{
			m_Animator = GetComponent<Animator>();
		}
		m_Animator.enabled = a_IsDisplayed;
	}
	
	public void SetPosition (Vector3 a_Position)
	{
		transform.position = a_Position;
	}
}
