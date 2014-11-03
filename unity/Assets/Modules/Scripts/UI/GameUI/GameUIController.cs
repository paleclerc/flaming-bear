using UnityEngine;
using System.Collections;

public class GameUIController : MonoBehaviour
{
	public GameUIView m_View;
	private GameUIModel m_Model;

	public void Init(int a_RemainingMove)
	{
		m_Model = new GameUIModel();
		m_Model.m_Score = 0;
		m_Model.m_RemainingMove = a_RemainingMove;

		UpdateView();
	}

	void UpdateView ()
	{
		m_View.UpdateVisual(m_Model);
	}

	public void UpdateScore(int a_Value)
	{
		m_Model.m_Score = a_Value;
		if(m_Model.m_Score <= 0)
		{
			m_Model.m_Score = 0;
		}

		UpdateView();
	}
	
	public void UpdateMove(int a_Value)
	{
		m_Model.m_RemainingMove = a_Value;
		if(m_Model.m_RemainingMove <= 0)
		{
			m_Model.m_RemainingMove = 0;
		}

		UpdateView();
	}

	//Only here as a testing purpose
	public void InjectData (GameUIModel a_Model)
	{
		m_Model = a_Model;

		UpdateView();
	}
}
