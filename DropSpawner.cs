using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour {

	public float leftRange = -3.5f;
	public float rightRange = 3.5f;
	public GameObject dropPrefab;
	public float m_waitTime = 2f;
	bool m_canCreateDrop = false; // bool to stop the spawning


	// Use this for initialization
	void Start () {
		StartCoroutine("CreateDrop");
	}

	public void ChangeDrop()
	{
		m_canCreateDrop = true;
		Debug.Log (m_canCreateDrop);
		StartCoroutine("CreateDrop");
	}

	public void TurnOffDrop()
	{
		m_canCreateDrop = false;
	}

	IEnumerator CreateDrop()
	{
		while(m_canCreateDrop)
		{
			Instantiate(dropPrefab, new Vector3(Random.Range(leftRange, rightRange), 3.5f, 0f), Quaternion.identity);
			yield return new WaitForSeconds(m_waitTime);

			if (m_waitTime > 0.3f) {
				m_waitTime = m_waitTime - 0.05f;
			}
			yield return null;
		}
		yield return null;
	}
}
