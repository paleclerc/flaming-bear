using UnityEngine;
using System.Collections;

public class AudioDebug : MonoBehaviour
{
	private const float OFF_SET = 10;
	private const int NUMBER_BUTTON_X = 5;
	private const int NUMBER_BUTTON_Y = 8;

	void OnGUI() 
	{
		int musicIndex = 0;
		float buttonWidth = (Screen.width / NUMBER_BUTTON_X)-OFF_SET;
		float buttonHeight = (Screen.height / NUMBER_BUTTON_Y)-OFF_SET;
		for (int k = 0; k < NUMBER_BUTTON_X; k++) 
		{
			for (int i = 0; i < NUMBER_BUTTON_Y; i++) 
			{
				
				Rect positionSize = new Rect(k*buttonWidth+(k*OFF_SET), i*buttonHeight+(i*OFF_SET), buttonWidth, buttonHeight);

				if(k==0 && i==0)
				{
					if (GUI.Button(positionSize, "STOP ALL"))
					{
						AudioManager.Instance.StopAll();
					}
					continue;
				}
				AudioConfigItem config = AudioConfigSO.Instance.m_ListAudioConfigItem[musicIndex];

				if (GUI.Button(positionSize, "Play "+config.m_UniqueName))
				{
					AudioManager.Instance.PlayAudioItem(config.m_UniqueName);
				}
				musicIndex++;

				if(musicIndex >= AudioConfigSO.Instance.m_ListAudioConfigItem.Count)
				{
					return;
				}
			}
		}


	}
}
