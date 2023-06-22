using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using System.IO;

public class GameManager : Singleton<GameManager> {

	string m_levelName;

	public int m_score;
	int m_highScore;
	public Text scoreText;
	public Text goScoreText;
	public Text gameStartHighScoreText;
	public Text highScoreText;

	[Space]

	public Button rewardsButton;

	[Space]

	//public GameObject GameDemoPanel;
	public GameObject menuPanel;
	public GameObject inGamePanel;
	public GameObject gameOverPanel;
	public GameObject iapPanel;
	public GameObject settingsPanel;
	public GameObject removeAdsPanel;

	[Space]

	public string ScreenshotName = "screenshot.png";

	[Space]

	public GameObject gameCenterButton;
	GameCenter m_gameCenter;

	public bool m_gameOver = false;

	public int m_totalCoins;
	public int m_coinsWonThisGame;
	public Text m_GameOverCoinText;
	public Text m_existingCoinText;


	GameObject m_guild;

	AdsManager m_adsManager;

	DropSpawner m_dropSpawner;

	ParticleManager m_particleManager;

	// Use this for initialization
	void Start () {
		Scene scene = SceneManager.GetActiveScene();
		m_levelName = scene.name;
		PlayerPrefs.GetString (m_levelName, "firstLevel");
		Debug.Log (m_levelName);

		m_dropSpawner = GameObject.Find ("DropSpawner").GetComponent<DropSpawner> ();
		m_particleManager = GameObject.Find ("ParticleManager").GetComponent<ParticleManager> ();
		m_adsManager = GameObject.Find ("AdsManager").GetComponent<AdsManager> ();
		m_guild = GameObject.Find ("Guild");
		m_gameCenter = GameObject.Find ("GameCenterManager").GetComponent<GameCenter> ();

		m_highScore = PlayerPrefs.GetInt ("highScore", 0);
		m_totalCoins = PlayerPrefs.GetInt ("Coins", 0);

		gameStartHighScoreText.text = "Highscore: " + m_highScore;

		if (menuPanel != null) {
			menuPanel.SetActive (true);
			menuPanel.GetComponent<RectXformMover> ().MoveOn ();
		}
		if (inGamePanel != null) {
			inGamePanel.SetActive (false);
		}


		if (gameOverPanel != null) {
			gameOverPanel.SetActive (false);
		}

		if (iapPanel != null) {
			iapPanel.SetActive (false);
		}


		gameCenterButton.SetActive (false);

		#if UNITY_IOS
		gameCenterButton.SetActive (true);

		IOSLocalNotification.RegisterNotificationSettings();

		//IOSLocalNotification.SetNotification("Come back and loose yourself in a quick, relaxing game!", 10);

		IOSLocalNotification.SetNotification("Come back and loose yourself in a quick, relaxing game!", 60 * 60 * 24);
		#endif

		#if UNITY_EDITOR
		gameCenterButton.SetActive (true);
		#endif

	}

	public void SendDrop(float posX, float posY)
	{
		m_particleManager.DestroyDrop (posX, posY);
	}

	public void SendPlayer(float posX, float posY)
	{
		m_particleManager.DestroyPlayer (posX, posY);
	}

	public void UpdateScore()
	{
		m_score += 1;
		if (scoreText != null) {
			scoreText.text = "Score: " + m_score;
		}
	}

	public void StartGame()
	{
		m_dropSpawner.ChangeDrop ();
		if (menuPanel != null) {
			menuPanel.SetActive (false);
			//menuPanel.GetComponent<RectXformMover> ().MoveOff ();
		}
		if (inGamePanel != null) {
			inGamePanel.SetActive (true);
			//menuPanel.GetComponent<RectXformMover> ().MoveOn ();
		}

		if (scoreText != null) 
		{
			scoreText.text = "Score: " + m_score;
		}

		if(m_guild != null)
		{
			m_guild.SetActive (false);
		}
	}
		
	public void GameOver()
	{
		m_dropSpawner.TurnOffDrop ();
		if (gameOverPanel != null) {
			gameOverPanel.SetActive (true);
			gameOverPanel.GetComponent<RectXformMover> ().MoveOn ();

		}
		if (inGamePanel != null) {
			inGamePanel.SetActive (false);
		}

		if (m_score > m_highScore) {
			m_highScore = m_score;
			PlayerPrefs.SetInt ("highScore", m_highScore);
			m_gameCenter.PostScoreOnLeaderBoard (m_score);
		}

		m_coinsWonThisGame = m_score / 10;

		highScoreText.text = "Highscore: " + m_highScore;
		goScoreText.text = "Score: " + m_score;

		m_adsManager.ShowAd ();

		if (m_existingCoinText != null) 
		{
			m_existingCoinText.text = m_totalCoins.ToString ();
		}

		if(m_GameOverCoinText != null)
		{
			Debug.Log (m_totalCoins + "+ " + m_coinsWonThisGame);
			m_GameOverCoinText.text = " + " + m_coinsWonThisGame.ToString ();
		}

		m_totalCoins += m_coinsWonThisGame;
		PlayerPrefs.SetInt ("Coins", m_totalCoins);
	}

	public void Restart()
	{
		//m_totalCoins += m_coinsWonThisGame;
		//PlayerPrefs.SetInt ("Coins", m_totalCoins);
		SceneManager.LoadScene (m_levelName, LoadSceneMode.Single);
	}

	public void OpenIAPStore()
	{
		if (iapPanel != null) {
			iapPanel.SetActive (true);
			iapPanel.GetComponent<RectXformMover> ().MoveOn ();
		}

		if (menuPanel != null) {
			//menuPanel.SetActive (false);
			menuPanel.GetComponent<RectXformMover> ().MoveOff ();
		}
	}

	public void CloseIAPStore()
	{
		if (iapPanel != null) {
			//iapPanel.SetActive (false);
			iapPanel.GetComponent<RectXformMover> ().MoveOff ();
		}

		if (menuPanel != null) {
			menuPanel.SetActive (true);
			menuPanel.GetComponent<RectXformMover> ().MoveOn ();
		}
	}

	public void OpenSettings()
	{
		if (settingsPanel != null) {
			settingsPanel.SetActive (true);
			settingsPanel.GetComponent<RectXformMover> ().MoveOn ();
		}

		if (menuPanel != null) {
			//menuPanel.SetActive (false);
			menuPanel.GetComponent<RectXformMover> ().MoveOff ();
		}
	}

	public void CloseSettings()
	{
		if (settingsPanel != null) {
			//settingsPanel.SetActive (false);
			settingsPanel.GetComponent<RectXformMover> ().MoveOff ();
		}

		if (menuPanel != null) {
			//menuPanel.SetActive (true);
			menuPanel.GetComponent<RectXformMover> ().MoveOn ();
		}
	}

	public void UpdateCoinsFromAds()
	{
		m_totalCoins += 10;
		PlayerPrefs.SetInt ("Coins", m_totalCoins);
		m_existingCoinText.text = m_totalCoins.ToString ();
		if (rewardsButton != null) 
		{
			rewardsButton.interactable = false;
		}

		//SceneManager.LoadScene (m_levelName, LoadSceneMode.Single);
	}

	void OnApplicationPause(bool isPause){
		if (!isPause) {
			IOSLocalNotification.CancelAllNotifications ();
			IOSLocalNotification.CleanIconBadge ();
		}
	}

	public void OpenRemoveAds()
	{
		if (removeAdsPanel != null) {
			removeAdsPanel.SetActive (true);
			removeAdsPanel.GetComponent<RectXformMover> ().MoveOn ();
			IAPManager.Instance.ResetPanel ();
		}

		if (menuPanel != null) {
			//menuPanel.SetActive (true);
			menuPanel.GetComponent<RectXformMover> ().MoveOff ();
		}
	}

	public void CloseRemoveAds()
	{
		if (removeAdsPanel != null) {
			//removeAdsPanel.SetActive (false);
			removeAdsPanel.GetComponent<RectXformMover> ().MoveOff ();
		}

		if (menuPanel != null) {
			//menuPanel.SetActive (true);
			menuPanel.GetComponent<RectXformMover> ().MoveOn ();
		}
	}

	public void StarReview()
	{
		NativeReviewRequest.RequestReview();
	}
}
