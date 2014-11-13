
using UnityEngine;

public class AudioItem : ScriptableObject
{
	public AudioClip m_AudioClip;
	public AudioItemType m_Type;
	public float m_DefaultVolume = 1f;
	public bool m_Loopable = false;
}

public enum AudioItemType
{
	SFX,
	MUSIC
}
