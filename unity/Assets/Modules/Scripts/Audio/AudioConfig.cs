using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AudioConfig : ScriptableObject
{
	public List<AudioConfigItem> m_ListAudioConfigItem;

	public AudioItem GetAudioItemByName(string a_Name)
	{
		foreach (AudioConfigItem item in m_ListAudioConfigItem)
		{
			if(item.m_UniqueName == a_Name)
			{
				return item.m_AudioItem;
			}
		}
		return null;
	}
}


public class AudioConfigSO : AccessorsSO<AudioConfig>
{
	static public AudioConfig Instance{get{return GetInternalInstance(ResourcesPathUtil.AUDIO_CONFIG);}}
}

[Serializable]
public class AudioConfigItem
{
	public string m_UniqueName;
	public AudioItem m_AudioItem;
}