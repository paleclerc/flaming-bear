using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FXTester : MonoBehaviour {

	public List<GameObject> m_ListFxToTest;

	private int m_CurrentIndex = 0;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () {
		if(m_ListFxToTest.Count > 0)
		{
			if (Input.GetMouseButtonDown(0))
			{
				GameObject go = (GameObject)Instantiate(m_ListFxToTest[m_CurrentIndex]);

				go.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
		}
	}


	void OnGUI() 
	{
		if(m_ListFxToTest.Count > 0)
		{
			if (GUI.Button(new Rect(10, 10, 75, 50), "Previous"))
			{
				m_CurrentIndex --;
			}
			if (GUI.Button(new Rect(10, 75, 75, 50), "Next"))
			{
				m_CurrentIndex ++;
			}

			if(m_CurrentIndex < 0)
			{
				m_CurrentIndex = m_ListFxToTest.Count -1;
			}

			
			if(m_CurrentIndex  >= m_ListFxToTest.Count)
			{
				m_CurrentIndex = 0;
			}

			GUI.Label(new Rect(90, 10, 200, 50), "Current FX :" + m_ListFxToTest[m_CurrentIndex].name);
		}
	}
}
