using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour 
{

	#region Singleton
	static private AudioManager m_Instance;
	static public AudioManager Instance{get{return m_Instance;}}
	void Awake()
	{
		m_Instance = this;
	}
	#endregion

	public int m_SwapMusicDelayFrame = 30;
	public float m_CurrentVolumeMusic = 1;
	public float m_CurrentVolumeSFX = 1;

	private List<AudioSource> m_ListAudioSource;
	private List<AudioSource> m_PoolAudioSource;

	private AudioSource m_CurrentMusic = null;
	private AudioSource m_NewMusic = null;


	void Start()
	{
		m_ListAudioSource = new List<AudioSource>();
		m_PoolAudioSource = new List<AudioSource>();
	}

	void Update() 
	{
		if (m_ListAudioSource.Count > 0)
		{
			for (int i = (m_ListAudioSource.Count-1); i >= 0; i--)
			{
				if(!m_ListAudioSource[i].isPlaying)
				{
					DeleteAudioSource(m_ListAudioSource[i]);
				}
			}
		}
	}

	public void PlayAudioItem(string a_AudioItem)
	{
		AudioItem audioItem = AudioConfigSO.Instance.GetAudioItemByName(a_AudioItem);

		if(audioItem == null)
		{
			Debug.LogError("ERROR :: Audio Item does not exist. Name="+a_AudioItem);
			return;
		}

		AudioSource newAudioSource = CreateNewAudioSource(a_AudioItem, audioItem);

		m_ListAudioSource.Add(newAudioSource);
		newAudioSource.Play();
	}

	public void StopAll()
	{
		for (int i = (m_ListAudioSource.Count-1); i >= 0; i--)
		{
			DeleteAudioSource(m_ListAudioSource[i]);
		}
		StopCoroutine("SwapMusic");

	}

	void DeleteAudioSource (AudioSource a_AudioSource)
	{
		if(a_AudioSource != null)
		{
			a_AudioSource.Stop();
			m_ListAudioSource.Remove(a_AudioSource);
			m_PoolAudioSource.Add(a_AudioSource);
			if(a_AudioSource == m_NewMusic)
			{
				m_NewMusic = null;
			}
			if(a_AudioSource == m_CurrentMusic)
			{
				m_CurrentMusic = null;
			}
			a_AudioSource.gameObject.name = "POOLED";
		}
	}

	AudioSource CreateNewAudioSource(string a_Name, AudioItem a_AudioItem)
	{
		AudioSource audioSource = null;
		if(m_PoolAudioSource.Count > 0)
		{
			audioSource = m_PoolAudioSource[m_PoolAudioSource.Count-1];
			m_PoolAudioSource.RemoveAt(m_PoolAudioSource.Count-1);
		}
		else
		{
			GameObject mygameobject = null;
			mygameobject = new GameObject();
			audioSource = mygameobject.AddComponent<AudioSource>();
		}

		audioSource.gameObject.name = a_Name;
		audioSource.gameObject.transform.parent = transform;
		audioSource.clip = a_AudioItem.m_AudioClip;
		if(a_AudioItem.m_Type == AudioItemType.MUSIC)
		{
			audioSource.volume = a_AudioItem.m_DefaultVolume * m_CurrentVolumeMusic;
		}
		else
		{
			audioSource.volume = a_AudioItem.m_DefaultVolume * m_CurrentVolumeSFX;
		}


		if(a_AudioItem.m_Type == AudioItemType.MUSIC)
		{
			StopCoroutine("SwapMusic");
			StartCoroutine("SwapMusic", audioSource);
		}
		audioSource.loop = a_AudioItem.m_Loopable;

		return audioSource;
	}

	private IEnumerator SwapMusic(AudioSource a_NewMusic)
	{
		if(m_CurrentMusic == null)
		{
			m_CurrentMusic = a_NewMusic;
			m_NewMusic = null;
			yield break;
		}
		
		if(m_NewMusic != null)
		{
			DeleteAudioSource(m_CurrentMusic);
			m_CurrentMusic = m_NewMusic;
		}

		m_NewMusic = a_NewMusic;

		float toRemoveCurrent = m_CurrentMusic.volume / (float)m_SwapMusicDelayFrame;
		float toAddNew = a_NewMusic.volume / (float)m_SwapMusicDelayFrame;
		
		m_NewMusic.volume = 0;

		for (int i = 0; i < m_SwapMusicDelayFrame; i++)
		{
			yield return 0;

			if(m_CurrentMusic != null)
			{
				m_CurrentMusic.volume -= toRemoveCurrent;
			}
			if(m_NewMusic != null)
			{
				m_NewMusic.volume += toAddNew;
			}
		}

		DeleteAudioSource(m_CurrentMusic);
		m_CurrentMusic = m_NewMusic;
		m_NewMusic = null;
	}

}
