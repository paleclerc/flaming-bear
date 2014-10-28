using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour 
{
	public GameObject m_TargetRotate;
	public float m_SpeedRotate = 1f;

	void Start()
	{
		UpdateRotation();
	}
	// Update is called once per frame
	void Update () 
	{
		UpdateRotation();
	}

	void UpdateRotation ()
	{
		transform.LookAt(m_TargetRotate.transform);
		transform.Translate(Vector3.right * Time.deltaTime * m_SpeedRotate);
	}
}
