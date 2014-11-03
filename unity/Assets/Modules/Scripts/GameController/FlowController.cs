using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowController : MonoBehaviour	
{
	public List<GemSlot> m_GemSlot;

	private bool m_IsNeedToValidate;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_IsNeedToValidate)
		{
			foreach (GemSlot gemSlot in m_GemSlot)
			{
				if(gemSlot.IsGemMoving())
				{
					return;
				}
			}

			ValidateGem();
		}
	}

	public bool GetIsCanSwap()
	{
		return m_IsNeedToValidate;
	}

	public void NewGemDropped()
	{
		m_IsNeedToValidate = true;
	}

	public void RegisterGemSlot(GemSlot a_GemSlot)
	{
		if(m_GemSlot == null)
		{
			m_GemSlot = new List<GemSlot>();
		}

		if(!m_GemSlot.Contains(a_GemSlot))
		{
			m_GemSlot.Add(a_GemSlot);
		}

		m_IsNeedToValidate = true;
	}

	public void UnregisterGemSlot(GemSlot a_GemSlot)
	{
		if(m_GemSlot.Contains(a_GemSlot))
		{
			m_GemSlot.Remove(a_GemSlot);
		}
	}

	public void ResetValidation ()
	{
		foreach (GemSlot gemSlot in m_GemSlot)
		{
			gemSlot.ResetValidation();
		}
	}

	void ValidateGem ()
	{
		m_IsNeedToValidate = false;
		ResetValidation();

		List<List<GemSlot>> allResult = new List<List<GemSlot>>();
		foreach (GemSlot gemSlot in m_GemSlot)
		{
			List<GemSlot> listSameGemHorizontal = new List<GemSlot>();
			gemSlot.ValidateHorizontal(listSameGemHorizontal);
			if(listSameGemHorizontal.Count > 2)
			{
				allResult.Add(listSameGemHorizontal);
				foreach (GemSlot checkedGemSlot in listSameGemHorizontal)
				{
					checkedGemSlot.SetCheckedHorizontal();
				}
			}
			List<GemSlot> listSameGemVertical = new List<GemSlot>();
			gemSlot.ValidateVertical(listSameGemVertical);
			if(listSameGemVertical.Count > 2)
			{
				allResult.Add(listSameGemVertical);
				foreach (GemSlot checkedGemSlot in listSameGemVertical)
				{
					checkedGemSlot.SetCheckedVertical();
				}
			}
		}
		foreach (List<GemSlot> listGemSlot in allResult)
		{

			foreach (GemSlot gemSlot in listGemSlot)
			{
				gemSlot.DeleteGem();
			}
		}
	}
}
