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
	public int m_IndexStartDropTable = -1;
	public int m_IndexRandomDropTable = -1;

	private bool m_IsCheckHorizontal;
	private bool m_IsCheckVertical;

	public void Init()
	{

	}

	public void Dispose ()
	{
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
		if(m_Gem != null)
		{
			return m_Gem.IsMoving();
		}
		
		if(m_UpSlot != null)
		{
			return m_UpSlot.IsGemMoving();
		}

		return false;
	}

	/*internal bool CheckUpHaveGem()
	{
		if(m_Gem != null)
		{
			return m_Gem.IsMoving();
		}
		
		if(m_UpSlot != null)
		{
			m_UpSlot.CheckUpHaveGem();
		}
	}*/

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
	}

	[ContextMenu("TestVertical")]
	private void DebugTestVertical()
	{
		GameController.Instance.GetFlowController.ResetValidation();
		List<GemSlot> listGemSlot = new List<GemSlot>();
		ValidateVertical(listGemSlot);
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
			if(m_Gem != null && compareGem != null)
			{
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
			if(m_Gem != null && compareGem != null)
			{
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

	#region Validate Unique at Start
	internal void GetGemUntilDiffUp(List<GemSlot> a_ListGemSlot, Gem a_Gem = null)
	{
		Gem compareGem = a_Gem;
		if(m_Gem == null)
		{
			return;
		}
		if(compareGem == null)
		{
			compareGem = m_Gem;
		}
		if(m_Gem.m_GemInfo.m_GemType == compareGem.m_GemInfo.m_GemType)
		{
			if(m_UpSlot != null)
			{
				m_UpSlot.GetGemUntilDiffUp(a_ListGemSlot, m_Gem);
			}
			a_ListGemSlot.Add(this);
		}
	}
	internal void GetGemUntilDiffDown(List<GemSlot> a_ListGemSlot, Gem a_Gem = null)
	{
		Gem compareGem = a_Gem;
		if(m_Gem == null)
		{
			return;
		}
		if(compareGem == null)
		{
			compareGem = m_Gem;
		}
		if(m_Gem.m_GemInfo.m_GemType == compareGem.m_GemInfo.m_GemType)
		{
			if(m_DownSlot != null)
			{
				m_DownSlot.GetGemUntilDiffDown(a_ListGemSlot, m_Gem);
			}
			a_ListGemSlot.Add(this);
		}
	}

	internal void GetGemUntilDiffLeft(List<GemSlot> a_ListGemSlot, Gem a_Gem = null)
	{
		Gem compareGem = a_Gem;
		if(m_Gem == null)
		{
			return;
		}
		if(compareGem == null)
		{
			compareGem = m_Gem;
		}
		if(m_Gem.m_GemInfo.m_GemType == compareGem.m_GemInfo.m_GemType)
		{
			if(m_LeftSlot != null)
			{
				m_LeftSlot.GetGemUntilDiffLeft(a_ListGemSlot, m_Gem);
			}
			a_ListGemSlot.Add(this);
		}
	}

	internal void GetGemUntilDiffRight(List<GemSlot> a_ListGemSlot, Gem a_Gem = null)
	{
		Gem compareGem = a_Gem;
		if(m_Gem == null)
		{
			return;
		}
		if(compareGem == null)
		{
			compareGem = m_Gem;
		}
		if(m_Gem.m_GemInfo.m_GemType == compareGem.m_GemInfo.m_GemType)
		{
			if(m_RightSlot != null)
			{
				m_RightSlot.GetGemUntilDiffRight(a_ListGemSlot, m_Gem);
			}
			a_ListGemSlot.Add(this);
		}
	}

	internal List<GemEnum> FindGemToNotUse(DirectionSlot a_IgnoreDirection = DirectionSlot.NONE)
	{
		List<GemEnum> mustIgnoreGem = new List<GemEnum>();
		List<GemSlot> upList = new List<GemSlot>();
		if(m_UpSlot != null && (a_IgnoreDirection != DirectionSlot.UP))
		{
			m_UpSlot.GetGemUntilDiffUp(upList);
		}

		List<GemSlot> downList = new List<GemSlot>();
		if(m_DownSlot != null&& (a_IgnoreDirection != DirectionSlot.DOWN))
		{
			m_DownSlot.GetGemUntilDiffDown(downList);
		}

		List<GemSlot> leftList = new List<GemSlot>();
		if(m_LeftSlot != null && (a_IgnoreDirection != DirectionSlot.LEFT))
		{
			m_LeftSlot.GetGemUntilDiffLeft(leftList);
		}
		
		List<GemSlot> rightList = new List<GemSlot>();
		if(m_RightSlot != null && (a_IgnoreDirection != DirectionSlot.RIGHT))
		{
			m_RightSlot.GetGemUntilDiffRight(rightList);
		}

		bool toCheckCombined = true;
		if(upList.Count >= 2)
		{
			mustIgnoreGem.Add(upList[0].m_Gem.m_GemInfo.m_GemType);
			toCheckCombined = false;
		}

		if(downList.Count >= 2)
		{
			mustIgnoreGem.Add(downList[0].m_Gem.m_GemInfo.m_GemType);
			toCheckCombined = false;
		}

		if(toCheckCombined && (downList.Count + upList.Count) >= 2)
		{
			if(downList[0].m_Gem.m_GemInfo.m_GemType == upList[0].m_Gem.m_GemInfo.m_GemType)
			{
				mustIgnoreGem.Add(downList[0].m_Gem.m_GemInfo.m_GemType);
			}
		}

		toCheckCombined = true;
		if(leftList.Count >= 2)
		{
			mustIgnoreGem.Add(leftList[0].m_Gem.m_GemInfo.m_GemType);
			toCheckCombined = false;
		}
		
		if(rightList.Count >= 2)
		{
			mustIgnoreGem.Add(rightList[0].m_Gem.m_GemInfo.m_GemType);
			toCheckCombined = false;
		}
		
		if(toCheckCombined && (leftList.Count + rightList.Count) >= 2)
		{
			if(leftList[0].m_Gem.m_GemInfo.m_GemType == rightList[0].m_Gem.m_GemInfo.m_GemType)
			{
				mustIgnoreGem.Add(leftList[0].m_Gem.m_GemInfo.m_GemType);
			}
		}

		return mustIgnoreGem;
	}

	#endregion
	
	
	public bool ExistPossibleMove ()
	{
		List<GemEnum> mustIgnoreGem;
		if(m_Gem != null)
		{
			if(m_UpSlot != null)
			{
				mustIgnoreGem = m_UpSlot.FindGemToNotUse(DirectionSlot.DOWN);
				if(mustIgnoreGem.Contains(m_Gem.m_GemInfo.m_GemType))
				{
					return true;
				}
			}

			if(m_DownSlot != null)
			{
				mustIgnoreGem = m_DownSlot.FindGemToNotUse(DirectionSlot.UP);
				if(mustIgnoreGem.Contains(m_Gem.m_GemInfo.m_GemType))
				{
					return true;
				}
			}

			if(m_LeftSlot != null)
			{
				mustIgnoreGem = m_LeftSlot.FindGemToNotUse(DirectionSlot.RIGHT);
				if(mustIgnoreGem.Contains(m_Gem.m_GemInfo.m_GemType))
				{
					return true;
				}
			}

			if(m_RightSlot != null)
			{
				mustIgnoreGem = m_RightSlot.FindGemToNotUse(DirectionSlot.LEFT);
				if(mustIgnoreGem.Contains(m_Gem.m_GemInfo.m_GemType))
				{
					return true;
				}
			}
		}

		return false;
	}

	internal enum DirectionSlot
	{
		NONE,
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	public void CreateStartGem (bool a_ForceValidation)
	{
		List<GemEnum> mustIgnoreGem = FindGemToNotUse();

		Gem gem = GemUtil.CreateGemAtPosition(m_IndexStartDropTable, transform.position, mustIgnoreGem);
		gem.InitAtGemSlot(this);
		m_Gem = gem;
	}

	public void SelectRandomGem (bool a_ForceValidation)
	{
		List<GemEnum> mustIgnoreGem = FindGemToNotUse();
		Gem gem = GemUtil.CreateGemAtPosition(m_IndexRandomDropTable, transform.position, mustIgnoreGem);
		gem.InitAtGemSlot(this);
		m_Gem = gem;
	}

}

