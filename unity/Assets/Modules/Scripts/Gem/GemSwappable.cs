using UnityEngine;
using System.Collections;

public class GemSwappable : MonoBehaviour
{

	public float m_MoveSpeed = 1;

	private Gem m_Gem;

	// Use this for initialization
	void Start ()
	{
		m_Gem = GetComponent<Gem>();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void Dispose()
	{

	}

	void OnMouseDown () 
	{
		GameController.Instance.GetInputController.ClickOnGem(m_Gem);
	}

}
