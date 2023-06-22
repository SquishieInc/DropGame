using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// this component allows a UI component to move from a start position to onscreen position to an end position

[RequireComponent(typeof(RectTransform))]
public class RectXformMover : MonoBehaviour 
{
    // starting position (typically offscreen)
	public Vector3 startPosition;

    // our onscreen position
	public Vector3 onscreenPosition;

    // our end position (typically offscreen again)
	public Vector3 endPosition;

    // time needed to move
	public float timeToMove = 1f;

    // reference to the RectTransform 
	RectTransform m_rectXform;

	CanvasGroup m_thisImage;

    // are we currently moving?
	bool m_isMoving = false;


	void Awake() 
	{
        // cache a reference to our RectTransform
		m_rectXform = GetComponent<RectTransform>();

		m_thisImage = GetComponent<CanvasGroup> ();
	}

    // move the RectTransform
	void Move(Vector3 startPos, Vector3 endPos, float timeToMove, float startAlpha = 0, float endAlpha = 1)
	{
		if (!m_isMoving) 
		{
			StartCoroutine (MoveRoutine (startPos, endPos, timeToMove, startAlpha, endAlpha));
		}
	}

    // coroutine for movement; this is generic, just pass in a start position, end position and time to move
	IEnumerator MoveRoutine(Vector3 startPos, Vector3 endPos, float timeToMove, float startAlpha, float endAlpha)
	{
        // set our current position to our start position
		if (m_rectXform != null) 
		{
			m_rectXform.anchoredPosition = startPos;
			m_thisImage.alpha = startAlpha;
		}

        // we have not reached our destination
		bool reachedDestination = false;

        // reset the amount of time that has passed
		float elapsedTime = 0f;

        // we are moving
		m_isMoving = true;

        // while we have not reached the destination...
		while (!reachedDestination) 
		{
            // ... check to see if we are close to the target position
			if (Vector3.Distance (m_rectXform.anchoredPosition, endPos) < 0.01f)
			{
				reachedDestination = true;
				break;

			}
            // increment our elapsed time by the time for this frame
			elapsedTime += Time.deltaTime;

            // calculate the interpolation parameter
			float t = Mathf.Clamp (elapsedTime / timeToMove, 0f, 1f);
			t = t * t * t * (t * (t * 6 - 15) + 10);

            // linearly interpolate from the start to the end position
			if (m_rectXform != null)
			{
				m_rectXform.anchoredPosition = Vector3.Lerp (startPos, endPos, t);
				m_thisImage.alpha = Mathf.Lerp (startAlpha, endAlpha, t);
              
			}

            // wait one frame
			yield return null;

		}
        // we are no longer moving
		m_isMoving = false;
		m_thisImage.interactable = true;
	
	}

    // move from a starting position offscreen to a position onscreen
	public void MoveOn()
	{
		Move (startPosition, onscreenPosition, timeToMove, 0, 1);
		m_thisImage.interactable = false;
	}

    // move from the position onscreen to an end position offscreen
	public void MoveOff()
	{
		Move (onscreenPosition, endPosition, timeToMove, 1, 0);
		m_thisImage.interactable = false;
	}

	public void ReverseOff()
	{
		Move (onscreenPosition, startPosition, timeToMove, 1, 0);
		m_thisImage.interactable = false;
	}

	public void ReverseOn()
	{
		Move (endPosition, onscreenPosition, timeToMove, 0, 1);
		m_thisImage.interactable = false;
	}


}
