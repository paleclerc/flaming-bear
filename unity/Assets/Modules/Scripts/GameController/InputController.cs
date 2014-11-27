using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour
{
	public GameObject m_SelectedCursorPrefab;
	public SelectionCursor m_SelectedCursorInstance;

	// Use this for initialization
	void Start ()
	{
		GameObject cursorGo = (GameObject)Instantiate(m_SelectedCursorPrefab);
		cursorGo.transform.parent = transform;

		m_SelectedCursorInstance = cursorGo.GetComponent<SelectionCursor>();
		m_SelectedCursorInstance.UnSelectAll();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			GameController.Instance.GetPowerupController.StopWaitingPowerup();
		}
	}

	public void ClickOnGem (Gem a_Gem)
	{
		if(GameController.Instance.GetFlowController.GetIsCanSwap())
		{
			m_SelectedCursorInstance.SelectGem(a_Gem);
		}
		else if (GameController.Instance.GetPowerupController.IsWaitingPowerup)
		{
			GameController.Instance.GetPowerupController.ClickOnGemSlot(a_Gem.MyGemSlot);
		}
	}
}
