using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonWithAudio : Button 
{
	const string m_SfxToPlay = "SFXButtonClick";

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		if(AudioManager.Instance != null)
		{
			AudioManager.Instance.PlayAudioItem(m_SfxToPlay);
		}
		base.OnPointerClick (eventData);
	}
}
