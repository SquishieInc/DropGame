using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityAdsButton : MonoBehaviour {

	public bool adReady;

	void Start()
	{
		adReady = true;
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

	public void ShowRewardAd()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			Debug.Log ("Ad is ready to be played");
			var options = new ShowOptions {resultCallback = HandleShowResult};
			Advertisement.Show ("rewardedVideo", options);
			//ShowOptions options = new ShowOptions();
			//options.resultCallback = HandleShowResult;

			Advertisement.Show ("rewardedVideo", options);
		}
	}

	private void HandleShowResult (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			GameManager.Instance.UpdateCoinsFromAds ();
			break;
		case ShowResult.Skipped:
			Debug.LogWarning ("Video was skipped.");
			break;
		case ShowResult.Failed:
			Debug.LogError ("Video failed to show.");
			break;
		}
	}
}
