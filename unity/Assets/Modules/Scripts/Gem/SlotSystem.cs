using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotSystem : MonoBehaviour
{

	public int m_SizeX;
	public int m_SizeY;
	public Vector2 m_SpaceBetween;
	public Vector3 m_SizeDebugSlot = new Vector3(1,1,1);

	public float m_DroppingSpeed = 0.1f;
	public float m_DropCooldown = 1;

	public GameObject m_GemContainer;

	public GameObject m_GemSlotPrefab;
	public GameObject m_GemDropperPrefab;

	public GameObject m_GemDropperContainer;
	public GameObject m_GemSlotContainer;

	private Transform myTransform;

	private List<List<GemSlot>> m_ListGameSlot;
	private List<GemDropper> m_ListGemDropper;


	// Use this for initialization
	void Start () 
	{
		CreateAll();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	[ContextMenu("DeleteAll")]
	void DeleteAll()
	{
		foreach (List<GemSlot> listGemSlot in m_ListGameSlot)
		{
			foreach (GemSlot gemSlot in listGemSlot)
			{
				gemSlot.Dispose();
				Destroy(gemSlot.gameObject);
			}
			listGemSlot.Clear();
		}
		m_ListGameSlot.Clear();


		foreach (GemDropper gemDropper in m_ListGemDropper)
		{
			gemDropper.Dispose();
			Destroy(gemDropper.gameObject);
		}
		m_ListGemDropper.Clear();
	}

	[ContextMenu("CreateAll")]
	void CreateAll()
	{
		StartCoroutine("CreateAllCoroutine");
	}

	IEnumerator CreateAllCoroutine()
	{
		myTransform = transform;

		CreateAllGemSlot();
		LinkGemSlotBetweenThem();
		CreateGemDropper();
		while(GameController.Instance.GetFlowController == null || !GameController.Instance.GetFlowController.GetIsCanSwap())
		{
			yield return 0;
		}
		SetRealSpeed();
	}

	void CreateGemDropper ()
	{
		m_ListGemDropper = new List<GemDropper>();
		for (int k = 0; k < m_SizeX; k++) 
		{
			GameObject gemDropperGO = (GameObject)Instantiate(m_GemDropperPrefab);
			if(m_GemDropperContainer != null)
			{
				gemDropperGO.transform.parent = m_GemDropperContainer.transform;
			}
			else
			{
				gemDropperGO.transform.parent = transform;
			}

			Vector3 tempPosition = CalculatePosition(k, m_SizeY);
			gemDropperGO.transform.position = tempPosition;

			GemDropper gemDropper = gemDropperGO.GetComponent<GemDropper>();
			gemDropper.AddGemSlot(m_ListGameSlot[(m_SizeY-1)][k]);
			gemDropper.m_GemContainer = m_GemContainer;
			gemDropper.m_DropSpeed = 1000;
			gemDropper.m_DropCooldown = 0;

			m_ListGemDropper.Add(gemDropper);
		}
	}

	void SetRealSpeed ()
	{
		foreach (GemDropper gemDropper in m_ListGemDropper)
		{
			gemDropper.m_DropSpeed = m_DroppingSpeed;
			gemDropper.m_DropCooldown = m_DropCooldown;
		}

		for (int i = 0; i < m_SizeY; i++) 
		{
			for (int k = 0; k < m_SizeX; k++) 
			{
				GemSlot currentGemSlot = m_ListGameSlot[i][k];
				currentGemSlot.m_Gem.m_DroppingSpeed = m_DroppingSpeed;
			}
		}
	}

	private void CreateAllGemSlot()
	{
		m_ListGameSlot = new List<List<GemSlot>>();
		for (int i = 0; i < m_SizeY; i++) 
		{
			List<GemSlot> rowGameSlot = new List<GemSlot>();
			m_ListGameSlot.Add(rowGameSlot);
			for (int k = 0; k < m_SizeX; k++) 
			{
				CreateGemSlotAtPosition(k, i);
			}
		}
	}

	void LinkGemSlotBetweenThem ()
	{
		for (int i = 0; i < m_SizeY; i++) 
		{
			for (int k = 0; k < m_SizeX; k++) 
			{
				GemSlot currentGemSlot = m_ListGameSlot[i][k];

				GemSlot previousYGemSlot = null;
				if(i > 0)
				{
					previousYGemSlot = m_ListGameSlot[i-1][k];
				}
				GemSlot nextYGemSlot = null;
				if(i < (m_SizeY-1))
				{
					nextYGemSlot = m_ListGameSlot[i+1][k];
				}

				GemSlot previousXGemSlot = null;
				if(k > 0)
				{
					previousXGemSlot = m_ListGameSlot[i][k-1];
				}

				GemSlot nextXGemSlot = null;
				if(k < (m_SizeX-1))
				{
					nextXGemSlot = m_ListGameSlot[i][k+1];
				}

				currentGemSlot.setNeigborGemSlot(previousYGemSlot,nextYGemSlot,previousXGemSlot,nextXGemSlot);
			}
		}
	}

	void CreateGemSlotAtPosition (int a_PositionX, int a_PositionY)
	{
		Vector3 tempPosition = CalculatePosition(a_PositionX, a_PositionY);

		GameObject gameSlotGO = (GameObject) Instantiate(m_GemSlotPrefab);


		if(m_GemSlotContainer!= null)
		{
			gameSlotGO.transform.parent = m_GemSlotContainer.transform;
		}
		else
		{
			gameSlotGO.transform.parent = myTransform;
		}

		gameSlotGO.transform.position = tempPosition;


		GemSlot gameSlot = gameSlotGO.GetComponent<GemSlot>();
		m_ListGameSlot[a_PositionY].Add(gameSlot);
	}

	private Vector3 CalculatePosition(int a_PositionX, int a_PositionY)
	{
		Vector3 tempPosition = new Vector3(a_PositionX* m_SpaceBetween.x, a_PositionY * m_SpaceBetween.y);
		tempPosition = tempPosition + transform.position;

		return tempPosition;
	}

	void OnDrawGizmos()
	{
		if(!Application.isPlaying)
		{
			for (int i = 0; i < m_SizeY; i++) 
			{
				for (int k = 0; k < m_SizeX; k++) 
				{
					Vector3 tempPosition = CalculatePosition(k, i);
					Gizmos.DrawCube(tempPosition, m_SizeDebugSlot);
				}
			}

			Gizmos.color = Color.cyan;
			for (int k = 0; k < m_SizeX; k++) 
			{
				Vector3 tempPosition = CalculatePosition(k, m_SizeY);
				Gizmos.DrawCube(tempPosition, m_SizeDebugSlot);
			}
		}
	}

	[ContextMenu("Reset")]
	public void Reset()
	{
		DeleteAll();
		CreateAll();
	}



}
