using UnityEngine;
using System.Collections;

public class SphereDestroy : MonoBehaviour 
{
	public float m_PositionYToDelete;

	// Update is called once per frame
	void Update () 
	{
		if(this.transform.position.y < m_PositionYToDelete)
		{
			Destroy(this.gameObject);
		}
	}
}
