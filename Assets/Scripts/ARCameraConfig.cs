using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARCameraConfig : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        // Get available camera configurations
        ARCameraManager cameraManager = GetComponent<ARCameraManager>();
        using var configs = cameraManager.GetConfigurations(Allocator.Temp);

        // Switch to the first available HD camera configuration
        Vector2Int HDResolution = new Vector2Int(1920, 1080);
        int framerate = 30;
        Debug.Log($"Base config:\n{cameraManager.currentConfiguration}");
        Debug.Log($"Configs length:\n{configs.Length}");
        foreach (XRCameraConfiguration c in configs)
            Debug.Log(c); // No configs for my phone
        foreach (XRCameraConfiguration c in configs)
        {
            if (c.resolution == HDResolution && c.framerate == framerate)
            {
                Debug.Log($"Base camera configuration:\n{cameraManager.currentConfiguration}");
                Debug.Log($"Switching camera configuration:\n{c}");
                cameraManager.currentConfiguration = c;
                break;
            }
        }
    }

    void Update()
    {
        
    }
}
