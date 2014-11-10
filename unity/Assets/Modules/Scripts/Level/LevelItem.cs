using UnityEngine;
using System.Collections.Generic;

public class LevelItem : ScriptableObject
{
	public string m_Id;
	public string m_DisplayName;
	public GameObject m_LevelPrefab;
	public Mission m_Mission;
	public int m_MaxMove;
}
