using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAPManager : Singleton <IAPManager> {

	Text updateText;
	GameObject noAdsButton;
	GameObject restoreButton;

	// Use this for initialization
	void Start () 
	{
		updateText = GameObject.Find ("UpdateText").GetComponent<Text> ();
		noAdsButton = GameObject.Find ("TurnOffAds");
		restoreButton = GameObject.Find ("RestorePurchases");

		if (updateText != null) 
		{
			updateText.text = " ";
		}

		if (noAdsButton != null) 
		{
			noAdsButton.SetActive (true);
		}

		if (restoreButton != null) 
		{
			restoreButton.SetActive (true);
		}
	}

	public void PurchaseSucessful()
	{
		if (updateText != null) 
		{
			updateText.text = "Purchase Successful";
		}
		if (noAdsButton != null) 
		{
			noAdsButton.SetActive (false);
		}

		if (restoreButton != null) 
		{
			restoreButton.SetActive (false);
		}
	}

	public void PurchaseFailed()
	{
		if (updateText != null) 
		{
			updateText.text = "Purchase failed" +
				" Please Try again Later";

			Debug.Log ("Purchase failed" +
			"Please Try again Later");
		}
		if (noAdsButton != null) 
		{
			noAdsButton.SetActive (false);
		}

		if (restoreButton != null) 
		{
			restoreButton.SetActive (false);
		}
	}

	public void PurchaseRestored()
	{
		if (updateText != null) 
		{
			updateText.text = "Purchase Successful";
		}
		if (noAdsButton != null) 
		{
			noAdsButton.SetActive (false);
		}

		if (restoreButton != null) 
		{
			restoreButton.SetActive (false);
		}
	}

	public void RestoreFailed()
	{
		if (updateText != null) 
		{
			updateText.text = "Restore failed" +
				" Please Try again Later";

			Debug.Log ("Restore failed" +
				"Please Try again Later");
		}
		if (noAdsButton != null) 
		{
			noAdsButton.SetActive (false);
		}

		if (restoreButton != null) 
		{
			restoreButton.SetActive (false);
		}
	}

	public void ResetPanel()
	{
		if (updateText != null) 
		{
			updateText.text = " ";
		}

		if (noAdsButton != null) 
		{
			noAdsButton.SetActive (true);
		}

		if (restoreButton != null) 
		{
			restoreButton.SetActive (true);
		}
	}
}
