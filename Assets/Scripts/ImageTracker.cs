using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ImageTracker : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    ARTrackedImageManager trackedImageManager;
    Dictionary<ARTrackedImage, GameObject> instances;

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        instances = new Dictionary<ARTrackedImage, GameObject>();
    }
    
    void OnEnable()
    {
        if (trackedImageManager != null)
            trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    
    void OnDisable()
    {
        if (trackedImageManager != null)
            trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            PlacePrefab(trackedImage);
            CanvasManager.current.SetText($"Added {args.added.Count} elements\nPrefab pos: {instances[trackedImage].transform.position}\nImg pos: {trackedImage.transform.position}");
        }
        
        foreach (var trackedImage in args.updated)
        {
            UpdatePrefabPosition(trackedImage);
            CanvasManager.current.SetText($"Added {args.updated.Count} elements\nPrefab pos: {instances[trackedImage].transform.position}\nImg pos: {trackedImage.transform.position}");
        }
    }

    void PlacePrefab(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;
        GameObject prefabToInstanciate = prefab;
        if (prefabToInstanciate != null)
        {
            GameObject instance = Instantiate(prefabToInstanciate, trackedImage.transform);
            instances.Add(trackedImage, instance);

            switch (imageName)
            {
                case "oui":
                    instance.transform.localPosition = new Vector3(-0.2f, 0, 0);
                    instance.GetComponent<Renderer>().material.color = Color.green;
                    break;
                case "non":
                    instance.transform.localPosition = new Vector3(0.2f, 0, 0);
                    instance.GetComponent<Renderer>().material.color = Color.red;
                    break;
                default:
                    instance.transform.localPosition = Vector3.zero;
                    break;
            }
        }
    }

    private GameObject GetPrefabForImage(string imageName)
    {
        switch (imageName)
        {
            case "oui":
                prefab.transform.position = new Vector3(0,0.2f,0);
                prefab.GetComponent<Renderer>().material.color = Color.green;
                break;
            case "non":
                prefab.transform.position = new Vector3(0.2f,0,0);
                prefab.GetComponent<Renderer>().material.color = Color.red;
                break;
            default:
                prefab.transform.position = Vector3.zero;
                break;
        }

        return prefab;
    }

    void UpdatePrefabPosition(ARTrackedImage trackedImage)
    {
    }
}