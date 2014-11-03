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
		m_SelectedCursorInstance.DisplayCusor(false);
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void ClickOnGem (Gem m_Gem)
	{
		m_SelectedCursorInstance.SetPosition(m_Gem.gameObject.transform.position);
		m_SelectedCursorInstance.DisplayCusor(true);
	}
}
