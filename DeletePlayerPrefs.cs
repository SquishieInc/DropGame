using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeletePlayerPrefs : MonoBehaviour {

	string m_levelName;

	void Start()
	{
		Scene scene = SceneManager.GetActiveScene();
		m_levelName = scene.name;
	}

	public void ResetGame()
	{
		PlayerPrefs.DeleteAll ();
		SceneManager.LoadScene(m_levelName, LoadSceneMode.Single);
	}
}