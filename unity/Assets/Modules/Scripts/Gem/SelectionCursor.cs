﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionCursor : MonoBehaviour {

	public List<SpriteRenderer> m_AnimationPart;
	
	private Animator m_Animator;
	private Gem m_SelectedGem = null;
	public string m_AudioToPlay;

	public void UnSelectAll()
	{
		m_SelectedGem = null;
		DisplayCusor(false);
	}

	public void SelectGem(Gem a_GemToSelect)
	{
		AudioManager.Instance.PlayAudioItem(m_AudioToPlay);
		if(m_SelectedGem == a_GemToSelect)
		{
			UnSelectAll();
		}
		else
		{
			if(IsCursorNeighborGem(a_GemToSelect))
			{
				SwapGem(a_GemToSelect);
			}
			else
			{
				m_SelectedGem = a_GemToSelect;
				SetPosition(m_SelectedGem.gameObject.transform.position);
				DisplayCusor(true);
			}
		}

	}

	bool IsCursorNeighborGem (Gem a_GemToSelect)
	{
		if(m_SelectedGem != null)
		{
			return m_SelectedGem.MyGemSlot.IsGemAround(a_GemToSelect);
		}
		return false;
	}

	void SwapGem (Gem a_GemToSelect)
	{
		GameController.Instance.GetFlowController.SwapGem(a_GemToSelect, m_SelectedGem);
		UnSelectAll();
	}

	private void DisplayCusor(bool a_IsDisplayed)
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
	
	private void SetPosition (Vector3 a_Position)
	{
		transform.position = a_Position;
	}
}
