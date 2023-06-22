using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UnlockLevel : MonoBehaviour {

	public GameObject Coins;
	public int m_cost;
	public int m_amountPaid;
	string _levelName;
	string m_currentScene;
	public Text CoinText;

	// Use this for initialization
	void Start () {
		_levelName = gameObject.name;
		Debug.Log (_levelName);

		Scene scene = SceneManager.GetActiveScene();
		m_currentScene = scene.name;


		m_amountPaid = PlayerPrefs.GetInt (_levelName, 0);
		if(m_amountPaid < m_cost)
		{
			Coins.SetActive (true);
			if (CoinText != null) 
			{
				CoinText.text = m_cost.ToString ();
			}
		}

		if (m_amountPaid == m_cost) 
		{
			Coins.SetActive (false);
			if (CoinText != null) 
			{
				CoinText.text = "";
			}
		}

		if (_levelName == m_currentScene) 
		{
			gameObject.SetActive (false);
		}
	}

	public void LevelButton()
	{
		if (m_amountPaid < m_cost) 
		{
			if (GameManager.Instance.m_totalCoins >= m_cost) 
			{
				Coins.SetActive (false);
				PlayerPrefs.SetInt(_levelName, m_cost);
				m_amountPaid = m_cost;
				GameManager.Instance.m_totalCoins -= m_cost;
				PlayerPrefs.SetInt ("Coins", GameManager.Instance.m_totalCoins);

				Debug.Log ("Level " + _levelName + " bought");
			}
		}
		if (m_amountPaid == m_cost) 
		{
			Debug.Log ("Level " + _levelName + " starting");
			PlayerPrefs.SetString ("LevelToLoad", _levelName);
			SceneManager.LoadScene (_levelName, LoadSceneMode.Single);
		}
	}
}
