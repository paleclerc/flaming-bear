using UnityEngine;
using System.Collections.Generic;

public class GemUtil 
{
	static private LevelGemDropTable m_LevelDropTable;
	static private GameObject m_GemContainer;
	static private GemConfig m_GemConfig;
	static public void InitGemUtil(LevelGemDropTable a_LevelDropTable, GameObject a_GemContainer)
	{
		m_LevelDropTable = a_LevelDropTable;
		m_GemContainer = a_GemContainer;
		if(m_GemConfig == null)
		{
			m_GemConfig = Resources.Load<GemConfig>(ResourcesPathUtil.GEM_CONFIG);
		}
	}

	static public GameObject FindGemToCreate (int a_DropTableIndex, List<GemEnum> a_MustIgnoreGem = null)
	{
		List<GemDropTableItem> tempGemDropTable = null;

		if((a_DropTableIndex < 0) || (a_DropTableIndex >= m_LevelDropTable.m_OtherDropTable.Count))
		{
			tempGemDropTable = m_LevelDropTable.m_DefaultDropTable.m_DropTable;
		}
		else
		{
			tempGemDropTable = m_LevelDropTable.m_OtherDropTable[a_DropTableIndex].m_DropTable;
		}

		List<GemDropTableItem> realDropTable;
		if(a_MustIgnoreGem == null)
		{
			realDropTable = tempGemDropTable;
		}
		else
		{
			realDropTable = new List<GemDropTableItem>();
			foreach (GemDropTableItem item in tempGemDropTable) 
			{
				if(!a_MustIgnoreGem.Contains(item.m_GemType))
				{
					realDropTable.Add(item);
				}
			}
		}
		int maxValue = 0;
		foreach (GemDropTableItem item in realDropTable)
		{
			maxValue += item.m_DropChanceWeight;
		}
		int random = Mathf.FloorToInt((UnityEngine.Random.value * maxValue));
		
		int tempValue = 0;
		foreach (GemDropTableItem item in realDropTable)
		{
			tempValue += item.m_DropChanceWeight;
			if(tempValue > random)
			{
				return m_GemConfig.GetGemPrefabByType(item.m_GemType);
			}
		}
		
		//if not found, we fallback as we can...
		return m_GemConfig.GetGemPrefabByType(tempGemDropTable[0].m_GemType);
	}

	public static Gem CreateGemAtPosition (int a_DropTableIndex, Vector3 a_Position, List<GemEnum> a_MustIgnoreGem = null)
	{
		GameObject gemToCreate = GemUtil.FindGemToCreate(a_DropTableIndex, a_MustIgnoreGem);
		
		GameObject gemGO = (GameObject)GameObject.Instantiate(gemToCreate);
		if(m_GemContainer != null)
		{
			gemGO.transform.parent = m_GemContainer.transform;
			gemGO.transform.position = a_Position;
		}
		Gem gem = gemGO.GetComponent<Gem>();

		return gem;
	}
}
