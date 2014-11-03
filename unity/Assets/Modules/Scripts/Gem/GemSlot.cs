using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GemSlot : MonoBehaviour {

	public GemSlot m_UpSlot;
	public GemSlot m_DownSlot;
	public GemSlot m_LeftSlot;
	public GemSlot m_RightSlot;

	public bool m_DebugDisplayNeigbor = false;


	public Gem m_Gem;

	private bool m_IsCheckHorizontal;
	private bool m_IsCheckVertical;
	// Use this for initialization
	void Start ()
	{
		GameController.Instance.GetFlowController.RegisterGemSlot(this);
	}

	public void Dispose ()
	{
		GameController.Instance.GetFlowController.UnregisterGemSlot(this);
		m_UpSlot = null;
		m_DownSlot = null;
		m_LeftSlot = null;
		m_RightSlot = null;

		DeleteGem();
	}

	public void DeleteGem ()
	{
		if(m_Gem != null)
		{
			m_Gem.Dispose();
			Destroy(m_Gem.gameObject);
			m_Gem= null;
		}

		MakeTopGemDrop();
	}

	public void DebugGem(Color color)
	{
		if(m_Gem != null)
		{
			SpriteRenderer spriteRenderer= m_Gem.GetComponentInChildren<SpriteRenderer>();
			spriteRenderer.color = color;
		}
	}
	public void MakeTopGemDrop()
	{
		if((m_UpSlot != null) && (m_UpSlot.m_Gem != null))
		{
			m_UpSlot.m_Gem.CheckIfCanDrop();
		}
	}

	public bool IsGemMoving()
	{
		if(m_Gem == null)
		{
			return true;
		}

		return m_Gem.IsMoving();
	}

	public bool GetHaveGem ()
	{
		return (m_Gem != null);
	}

	public void setNeigborGemSlot (GemSlot a_PreviousYGemSlot, GemSlot a_NextYGemSlot, GemSlot a_PreviousXGemSlot, GemSlot a_NextXGemSlot)
	{
		m_DownSlot = a_PreviousYGemSlot;
		m_UpSlot = a_NextYGemSlot;
		m_RightSlot = a_PreviousXGemSlot;
		m_LeftSlot = a_NextXGemSlot;
	}

	public GemSlot GetDroppingSlot ()
	{
		if((m_DownSlot != null) && (!m_DownSlot.GetHaveGem()))
		{
			return m_DownSlot;
		}
		return null;
	}

	public void ResetValidation ()
	{
		m_IsCheckHorizontal = false;
		m_IsCheckVertical = false;
	}

	public void SetCheckedHorizontal()
	{
		m_IsCheckHorizontal = true;
	}

	public void SetCheckedVertical()
	{
		m_IsCheckVertical = true;
	}

	[ContextMenu("TestHorizontal")]
	private void DebugTestHorizontal()
	{
		GameController.Instance.GetFlowController.ResetValidation();
		List<GemSlot> listGemSlot = new List<GemSlot>();
		ValidateHorizontal(listGemSlot);

		Debug.Log("PAL :: DebugTestHorizontal :: " +listGemSlot.Count);
	}

	[ContextMenu("TestVertical")]
	private void DebugTestVertical()
	{
		GameController.Instance.GetFlowController.ResetValidation();
		List<GemSlot> listGemSlot = new List<GemSlot>();
		ValidateVertical(listGemSlot);
		
		Debug.Log("PAL :: DebugTestVertical :: " +listGemSlot.Count);
	}

	public void ValidateHorizontal(List<GemSlot> a_ListGemSlot, Gem a_Gem = null)
	{
		if(!m_IsCheckHorizontal)
		{
			Gem compareGem = a_Gem;
			if(compareGem == null)
			{
				compareGem = m_Gem;
			}
			if(m_Gem.m_GemInfo.m_GemType == compareGem.m_GemInfo.m_GemType)
			{
				m_IsCheckHorizontal = true;
				if(m_RightSlot != null)
				{
					m_RightSlot.ValidateHorizontal(a_ListGemSlot, m_Gem);
				}
				if(m_LeftSlot != null)
				{
					m_LeftSlot.ValidateHorizontal(a_ListGemSlot, m_Gem);
				}

				a_ListGemSlot.Add(this);
			}
		}
	}

	public void ValidateVertical(List<GemSlot> a_ListGemSlot, Gem a_Gem = null)
	{
		if(!m_IsCheckVertical)
		{
			Gem compareGem = a_Gem;
			if(compareGem == null)
			{
				compareGem = m_Gem;
			}
			if(m_Gem.m_GemInfo.m_GemType == compareGem.m_GemInfo.m_GemType)
			{
				m_IsCheckVertical = true;
				if(m_UpSlot != null)
				{
					m_UpSlot.ValidateVertical(a_ListGemSlot, m_Gem);
				}
				if(m_DownSlot != null)
				{
					m_DownSlot.ValidateVertical(a_ListGemSlot, m_Gem);
				}
				
				a_ListGemSlot.Add(this);
			}
		}
	}

	public bool IsGemAround(Gem a_Gem)
	{
		if(a_Gem != null)
		{
			bool left = (m_LeftSlot != null) && (m_LeftSlot.m_Gem == a_Gem);
			bool right = (m_RightSlot != null) && (m_RightSlot.m_Gem == a_Gem);
			bool top = (m_UpSlot != null) && (m_UpSlot.m_Gem == a_Gem);
			bool down = (m_DownSlot != null) && (m_DownSlot.m_Gem == a_Gem);
			return  left || right || top || down;
		}
		return false;
	}
	
	void OnDrawGizmos()
	{
		if(m_DebugDisplayNeigbor)
		{
			if(m_DownSlot)
			{
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(transform.position,m_DownSlot.transform.position);
			}
			if(m_UpSlot)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawLine(transform.position,m_UpSlot.transform.position);
			}
			if(m_LeftSlot)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawLine(transform.position,m_LeftSlot.transform.position);
			}
			if(m_RightSlot)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(transform.position,m_RightSlot.transform.position);
			}
		}
	}
}
