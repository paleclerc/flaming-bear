using UnityEngine.UI;
using UnityEngine;

public static class UIUtils
{
	public static void SetPositionCenteredUI(RectTransform a_RectTransform, Vector2 a_PercentPosition)
	{
		a_RectTransform.offsetMin = Vector2.zero;
		a_RectTransform.offsetMax = Vector2.zero;
		
		Vector2 sizeHalf = ((a_RectTransform.anchorMax - a_RectTransform.anchorMin)/2);
		a_RectTransform.anchorMin = a_PercentPosition - sizeHalf;
		a_RectTransform.anchorMax = a_PercentPosition + sizeHalf;
	}

	public static void CopyTransformToOtherTransform (RectTransform a_TransformToMove, RectTransform a_TransformReference)
	{
		a_TransformToMove.offsetMin = a_TransformReference.offsetMin;
		a_TransformToMove.offsetMax = a_TransformReference.offsetMax;

		a_TransformToMove.anchorMin = a_TransformReference.anchorMin;
		a_TransformToMove.anchorMax = a_TransformReference.anchorMax;
	}
}
