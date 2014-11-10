using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldMapSectionTemplateView : MonoBehaviour
{
	public Image m_ImageBackground;

	public void UpdateVisual(WorldMapSectionTemplateModel a_Model)
	{
		m_ImageBackground.sprite = a_Model.m_Background;
	}
}
