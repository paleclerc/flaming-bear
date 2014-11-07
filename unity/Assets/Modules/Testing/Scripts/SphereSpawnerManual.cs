using UnityEngine;
using System.Collections;

public class SphereSpawnerManual : MonoBehaviour {

	public GameObject m_Prefab;

	void OnGUI() 
	{
		if (GUI.Button(new Rect(10, 10, 150, 100), "SPAWN"))
		{
			CreateSphere();
		}
		
	}
	
	[ContextMenu("Spawn Sphere")]
	private void CreateSphere()
	{
		GameObject go = (GameObject)Instantiate(m_Prefab);
		go.transform.parent = this.transform;
		go.transform.localPosition = Vector3.zero;
	}
}
