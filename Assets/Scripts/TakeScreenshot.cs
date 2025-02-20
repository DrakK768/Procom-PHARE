using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{

    public GameObject canvas;
    public void OnClickScreenCaptureButton()
    {
        StartCoroutine(CaptureScreen());
    }

    public IEnumerator CaptureScreen()
    {
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        canvas.SetActive(false);

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        string data = DateTime.Now.ToString();
        data = data.Replace("/", "_");
        data = data.Replace(" ", "_");
        data = data.Replace(":","_");

        // Take screenshot
        ScreenCapture.CaptureScreenshot(data+".png");
        //TODO : change name of the captured screenshot

        // Show UI after we're done
        canvas.SetActive(true);
    }
}
