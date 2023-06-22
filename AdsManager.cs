using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : Singleton <AdsManager> {

	public bool adReady;

	private int m_isPurchased;

	IAPManager iapmanager;

	void Start()
	{
		adReady = true;

		m_isPurchased = PlayerPrefs.GetInt ("isPurchased", 0);

		iapmanager = GameObject.Find ("IAPManager").GetComponent<IAPManager> ();
	}

	void Awake ()
	{
		if (Advertisement.isSupported) 
		{
			if(Application.platform == RuntimePlatform.Android)
			{
				Advertisement.Initialize("1602825", false);
			}
			else if(Application.platform == RuntimePlatform.IPhonePlayer)
			{
				Advertisement.Initialize("1602824", false);
			}
		}
	}

	public void ShowAd()
	{
		if(m_isPurchased == 0)
		{
			adReady = false;
			if (Advertisement.IsReady())
			{
				//var options = new ShowOptions {resultCallback = HandleShowResult};
				Advertisement.Show ();
			}
		}
	}

	public void TurnOffAds()
	{
		m_isPurchased = 1;
		PlayerPrefs.SetInt ("isPurchased", m_isPurchased);
	}
}
