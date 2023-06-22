using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

/*
 * https://github.com/ChrisMaire/unity-native-sharing
 */

public class ShareButton: MonoBehaviour {
	public string ScreenshotName = "screenshot.png";

	int m_scoreToShare;

    public void ShareScreenshotWithText()
    {
        string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
        if(File.Exists(screenShotPath)) File.Delete(screenShotPath);

        ScreenCapture.CaptureScreenshot(ScreenshotName);

		m_scoreToShare = GameManager.Instance.m_score;

		StartCoroutine(delayedShare(screenShotPath, m_scoreToShare));
    }

    //CaptureScreenshot runs asynchronously, so you'll need to either capture the screenshot early and wait a fixed time
    //for it to save, or set a unique image name and check if the file has been created yet before sharing.
	IEnumerator delayedShare(string screenShotPath, int score)
    {
        while(!File.Exists(screenShotPath)) {
    	    yield return new WaitForSeconds(.05f);
        }

		NativeShare.Share("I just dodged " + score + " Drips playing 'DripDrip'.  try it for yourself  https://itunes.apple.com/app/dripdrip/id1309481917", screenShotPath, "", "", "image/png", true, "");
    }
}
