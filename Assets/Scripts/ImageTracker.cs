using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ImageTracker : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Vector3 offsetPrefabPosition = Vector3.zero;
    [SerializeField] Vector3 offsetPrefabRotation = Vector3.zero;

    [SerializeField] List<Slider> sliders = new List<Slider>();

    ARTrackedImageManager trackedImageManager;
    GameObject instance;

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        if (prefab != null)
        {
            instance = Instantiate(prefab);
            instance.SetActive(false);
        }
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
            Debug.Log($"Added {args.added.Count} elements\nPrefab pos: {instance.transform.position}\nImg pos: {trackedImage.transform.position}\n" +
                $"Offset pos:{offsetPrefabPosition}\nOffset rot: {offsetPrefabRotation}");
        }

        foreach (var trackedImage in args.updated)
        {
            UpdatePrefabPosition(trackedImage);
            Debug.Log($"Updated {args.updated.Count} elements\nPrefab pos: {instance.transform.position}\nImg pos: {trackedImage.transform.position}\n" +
                $"Offset pos:{offsetPrefabPosition}\nOffset rot: {offsetPrefabRotation}");
        }
    }

    void PlacePrefab(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;
        if (instance != null)
        {
            instance.transform.SetParent(trackedImage.transform, false);
            instance.SetActive(true);
            switch (imageName)
            {
                case "oui":
                    instance.transform.localPosition = offsetPrefabPosition;
                    instance.transform.localEulerAngles = offsetPrefabRotation;
                    //instance.GetComponent<Renderer>().material.color = Color.green;
                    break;
                case "non":
                    instance.transform.localPosition = offsetPrefabPosition;
                    instance.transform.localEulerAngles = offsetPrefabRotation;
                    //instance.GetComponent<Renderer>().material.color = Color.red;
                    break;
                default:
                    instance.transform.localPosition = Vector3.zero;
                    break;
            }
        }
    }

    void UpdatePrefabPosition(ARTrackedImage trackedImage)
    {
    }

    public void OnSliderPositionChanged()
    {
        offsetPrefabPosition = new Vector3(sliders[0].value, sliders[1].value, sliders[2].value);
        instance.transform.position = offsetPrefabPosition;
    }

    public void OnSliderRotationChanged()
    {
        offsetPrefabRotation = new Vector3(sliders[3].value, sliders[4].value, sliders[5].value);
        instance.transform.localEulerAngles = offsetPrefabRotation;
    }
}