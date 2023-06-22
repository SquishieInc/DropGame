using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	float m_speed = 5f;

	bool m_isMoving;

	public InterpType interpolation = InterpType.SmootherStep;

	bool dragEnabled = false;
	Vector3 dragStartPosition;
	float dragStartDistance;

	public enum InterpType
	{
		Linear,
		EaseOut,
		EaseIn,
		SmoothStep,
		SmootherStep
	};

	//Rigidbody2D m_rigidbody2D;

	void FixedUpdate () {
		if (Input.GetAxis("Horizontal") < -0.05f)
			//m_rigidbody2D.velocity = new Vector2 (m_speed * -1, 0f);
			transform.Translate (-Vector2.right * m_speed * Time.deltaTime);
		if (Input.GetAxis("Horizontal") > 0.05f)
			//m_rigidbody2D.velocity = new Vector2 (m_speed, 0f);
			transform.Translate (Vector2.right * m_speed * Time.deltaTime);
	}

	void OnMouseDown()
	{
		dragEnabled = true;
		dragStartPosition = transform.position;
		dragStartDistance = (Camera.main.transform.position - transform.position).magnitude;
	}
	void Update()
	{
		if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
		{
			dragEnabled = false;
		}
	}
	void OnMouseDrag()
	{
		if (dragEnabled)
		{
			Vector3 worldDragTo = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragStartDistance));
			transform.position = new Vector3(worldDragTo.x, dragStartPosition.y, dragStartPosition.z);
		}
	}
}
