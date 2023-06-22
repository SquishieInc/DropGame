using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class GameCenter : MonoBehaviour {

	public bool loginSuccessful;

	string leaderboardID = "DropGameHighScoreLeaderBoard";

	public void AuthenticateUser()
	{
		Social.localUser.Authenticate((bool success) => {
			if(success)
			{
				loginSuccessful = true;

				Debug.Log("success");
			}
			else
			{
				Debug.Log("unsuccessful");
			}
			// handle success or failure
		});
	}

	public void PostScoreOnLeaderBoard(int myScore)

	{
		if(loginSuccessful)
		{
		Social.ReportScore(myScore, leaderboardID, (bool success) => 
			{
		if(success)

		Debug.Log("Successfully uploaded");

		// handle success or failure

		});

		}

		else

		{

		Social.localUser.Authenticate((bool success) => {

		if(success)

		{

		loginSuccessful = true;

		Social.ReportScore(myScore ,leaderboardID, (bool successful) => {

		// handle success or failure

		});

		}

		else

		{

		Debug.Log("unsuccessful");

		}

		// handle success or failure

		});

		}
	}

	public void ShowGameCenter()
	{
		Social.ShowLeaderboardUI();
	}


}
