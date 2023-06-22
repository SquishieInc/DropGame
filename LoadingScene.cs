using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

	GameCenter m_gameCenter;
	RectXformMover m_alphaChange;
	public float waitTme = 2;

	string m_levelToLoad;

	// Use this for initialization
	void Start () 
	{
		m_levelToLoad = PlayerPrefs.GetString ("LevelToLoad", "firstLevel");

		m_gameCenter = GetComponent<GameCenter> ();
		m_alphaChange = GetComponent<RectXformMover> ();

		if (m_gameCenter != null) {
			m_gameCenter.AuthenticateUser ();
		}
		if (m_alphaChange != null) {
			m_alphaChange.MoveOn ();
		}

		StartCoroutine ("EndLoadingScene");
	}
	
	// Update is called once per frame
	IEnumerator EndLoadingScene()
	{
		yield return new WaitForSeconds (waitTme);

		if (m_alphaChange != null) {
			m_alphaChange.MoveOff ();
		}

		yield return new WaitForSeconds (waitTme);

		SceneManager.LoadScene (m_levelToLoad, LoadSceneMode.Single);

		yield break;
	}
}
