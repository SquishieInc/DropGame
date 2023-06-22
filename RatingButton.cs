using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingButton : MonoBehaviour {

	public string APP_STORE_LINK_IOS = "https://itunes.apple.com/us/app/dripdrip/id1309481917?ls=1&mt=8";
	public string APP_STORE_LINK_ANDROID = "https://play.google.com/store/apps/details?id=com.squishieinc.dripdrip";

	public string ANDROID_RATE_URL = "market://details?id=com.squishieinc.dripdrip";
	public string IOS_RATE_URL = "itms-apps://itunes.apple.com/app/idcom.squishieinc.drop";

	public void PressedRatingButton()
	{
		#if UNITY_ANDROID
		Application.OpenURL(ANDROID_RATE_URL);
		#elif UNITY_IOS
		Application.OpenURL(IOS_RATE_URL);
		#endif
	}
}
