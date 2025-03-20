using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TakeScreenshot : MonoBehaviour
//Description : Script to take a screenshot of the app without the UI and save it in the phone's gallery
{

    public GameObject canvas;
    private string album = "Pictures";

    private async void RequestPermissionAsynchronously( NativeGallery.PermissionType permissionType, NativeGallery.MediaType mediaTypes )
    //Get permission to save to gallery
    {
        NativeGallery.Permission permission = await NativeGallery.RequestPermissionAsync( permissionType, mediaTypes );
        Debug.Log( "Permission result: " + permission );
    }
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

        RequestPermissionAsynchronously(NativeGallery.PermissionType.Write, NativeGallery.MediaType.Image);

        string data = DateTime.Now.ToString();
        data = data.Replace("/", "_");
        data = data.Replace(" ", "_");
        data = data.Replace(":","_");

        // Take screenshot
        Texture2D temp = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
	    temp.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
	    temp.Apply();

	    // Save the screenshot to Gallery/Photos
	    NativeGallery.Permission permission = NativeGallery.SaveImageToGallery( temp, album, data, ( success, path ) => Debug.Log( "Media save result: " + success + " " + path ) );

	    Debug.Log( "Permission result: " + permission );

	    // To avoid memory leaks
	    Destroy( temp );

        // Show UI after we're done
        canvas.SetActive(true);

        

    }
}
