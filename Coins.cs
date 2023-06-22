using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

	Text m_coinText;
	int m_coins;

	void Start()
	{
		m_coins = PlayerPrefs.GetInt ("Coins", 0);

		m_coinText = GetComponent<Text> ();
		m_coinText.text = m_coins.ToString();
	}
}
