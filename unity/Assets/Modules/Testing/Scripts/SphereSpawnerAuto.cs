using UnityEngine;
using System.Collections;

public class SphereSpawnerAuto : MonoBehaviour {

	public GameObject m_Prefab;
	public float m_WaitTime;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine("SpawnSphere");
	}

	IEnumerator SpawnSphere()
	{
		while(true)
		{
			yield return new WaitForSeconds(m_WaitTime);

			GameObject go = (GameObject)Instantiate(m_Prefab);
			go.transform.parent = this.transform;
			go.transform.localPosition = Vector3.zero;
		}
	}
}
