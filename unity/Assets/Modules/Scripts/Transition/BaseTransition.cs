using UnityEngine;
using System.Collections;

public class BaseTransition : MonoBehaviour
{

	private bool m_TransitionInCompleted = false;
	private bool m_TransitionOutCompleted = false;

	public bool TransitionInCompleted {get {return m_TransitionInCompleted;}}
	public bool TransitionOutCompleted {get {return m_TransitionOutCompleted;}}

	public MiniTweener m_Tweener;
	public GameObject m_BackgroundImage;


	#region TransitionIn
	public void StartTransitionIn ()
	{
		m_Tweener.OnAnimationCompleted += OnTransitionInHalfDone;
		m_Tweener.StartMoveFoward();
	}

	void OnTransitionInHalfDone ()
	{
		m_Tweener.OnAnimationCompleted -= OnTransitionInHalfDone;
		
		m_BackgroundImage.SetActive(true);
		m_Tweener.StartMoveBackward();
		m_Tweener.OnAnimationCompleted += OnTransitionInDone;
	}

	void OnTransitionInDone ()
	{
		m_Tweener.OnAnimationCompleted -= OnTransitionInDone;
		m_TransitionInCompleted = true;
	}
	#endregion

	#region TransitionOut
	public void StartTransitionOut ()
	{
		m_Tweener.OnAnimationCompleted += OnTransitionOutHalfDone;
		m_Tweener.StartMoveFoward();
	}

	void OnTransitionOutHalfDone ()
	{
		m_Tweener.OnAnimationCompleted -= OnTransitionOutHalfDone;
		
		m_BackgroundImage.SetActive(false);
		m_Tweener.StartMoveBackward();
		m_Tweener.OnAnimationCompleted += OnTransitionOutDone;
	}
	
	void OnTransitionOutDone ()
	{
		m_Tweener.OnAnimationCompleted -= OnTransitionOutDone;
		m_TransitionOutCompleted = true;
	}
	#endregion

}
