using UnityEngine;
using System.Collections;

public class AccessorsSO<T> where T : ScriptableObject
{
	static T m_Instance;

	static protected T GetInternalInstance(string a_Path)
	{
		if(m_Instance == null)
		{
			m_Instance = (T)Resources.Load(a_Path, typeof(T));
		}

		return m_Instance;
	}

}
