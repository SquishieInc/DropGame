using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;

public class Drop : MonoBehaviour {

	GameManager m_gameManager;
	SoundManager m_soundManager;



	public string ScreenshotName = "screenshot.png";

	// Use this for initialization
	void Start () {
		m_gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		m_soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		// If the Collider2D component is enabled on the collided object
		if (col.gameObject.name == "Player")
		{
			m_gameManager.m_gameOver = true;
			string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
			if(File.Exists(screenShotPath)) File.Delete(screenShotPath);

			GameObject player = col.gameObject;
			Transform playerTransform = player.transform;
			Vector3 playerPos = playerTransform.position;

			Vector3 pos = gameObject.transform.position;

			m_gameManager.SendPlayer (playerPos.x, playerPos.y);
			m_gameManager.SendDrop (pos.x, pos.y);

			StartGameOver(player);

			ScreenCapture.CaptureScreenshot(ScreenshotName);

			//particle player(playerPos.x, playerPosy);

			//Destroy (this.gameObject);
			//Destroy (col.gameObject);

			//m_gameManager.GameOver ();
			//m_soundManager.PlayLoseSound ();
		}
			

		if (col.gameObject.name == "Ground")
		{
			Vector3 pos = gameObject.transform.position;

			m_gameManager.SendDrop (pos.x, pos.y);

			Destroy (this.gameObject);
			m_soundManager.PlayBonusSound ();

			if (!m_gameManager.m_gameOver) {
				m_gameManager.UpdateScore ();
			}
		}
	}

	void StartGameOver(GameObject col)
	{
		Destroy (this.gameObject);
		Destroy (col.gameObject);

		m_gameManager.GameOver ();
		m_soundManager.PlayLoseSound ();

	}
}
