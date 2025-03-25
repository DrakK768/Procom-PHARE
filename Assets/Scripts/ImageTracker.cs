using System;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    [SerializeField] GameObject gardienPrefab;
    [SerializeField] GameObject hotelPrefab;
    [Header("'poster' position")]
    [SerializeField] Vector3 posterOffsetPrefabPosition = Vector3.zero;
    [SerializeField] Vector3 posterOffsetPrefabRotation = Vector3.zero;
    [SerializeField] bool posterHotel = false;
    [Header("'poster2' position")]
    [SerializeField] Vector3 poster2OffsetPrefabPosition = Vector3.zero;
    [SerializeField] Vector3 poster2OffsetPrefabRotation = Vector3.zero;
    [SerializeField] bool poster2Hotel = true;
    [Header("'poster3' position")]
    [SerializeField] Vector3 poster3OffsetPrefabPosition = Vector3.zero;
    [SerializeField] Vector3 poster3OffsetPrefabRotation = Vector3.zero;
    [SerializeField] bool poster3Hotel = false;

    public static ImageTracker current;

    public GameObject startMenu ;
    public GameObject sideMenuButton;
    public GameObject bottomPannel;

    ARTrackedImageManager trackedImageManager;
    GameObject instanceGardien;
    GameObject instanceHotel;
    [HideInInspector]
    public GameObject currentInstance;
    [HideInInspector]
    public Camera arCamera;

    void Awake()
    {
        current = this;
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void Start()
    {
        XROrigin sessionOrigin = FindObjectOfType<XROrigin>();
        if (sessionOrigin != null)
        {
            arCamera = sessionOrigin.Camera;
            arCamera.farClipPlane = 100;
        }

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        if (gardienPrefab != null)
        {
            instanceGardien = Instantiate(gardienPrefab);
            instanceGardien.SetActive(false);
        }
        if (hotelPrefab != null)
        {
            instanceHotel = Instantiate(hotelPrefab);
            instanceHotel.SetActive(false);
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

    void OnDestroy()
    {
        current = null;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        startMenu.SetActive(false);
        sideMenuButton.SetActive(true);
        bottomPannel.SetActive(true);
        foreach (var trackedImage in args.added)
        {
            PlacePrefab(trackedImage);
            //Debug.Log($"Added {args.added.Count} elements\nPrefab pos: {instanceGardien.transform.position}\nImg pos: {trackedImage.transform.position}\n" +
            //    $"Offset pos:{posterOffsetPrefabPosition}\nOffset rot: {posterOffsetPrefabRotation}");
        }

        foreach (var trackedImage in args.updated)
        {
            UpdatePrefabPosition(trackedImage);
            //Debug.Log($"Updated {args.updated.Count} elements\nPrefab pos: {instanceGardien.transform.position}\nImg pos: {trackedImage.transform.position}\n" +
            //    $"Offset pos:{posterOffsetPrefabPosition}\nOffset rot: {posterOffsetPrefabRotation}");
        }
    }

    void PlacePrefab(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;
        instanceGardien?.SetActive(false);
        instanceHotel?.SetActive(false);
        switch (imageName)
        {
            case "poster":
                SetInstance(posterHotel, posterOffsetPrefabPosition, posterOffsetPrefabRotation, trackedImage);
                break;
            case "poster2":
                SetInstance(poster2Hotel, poster2OffsetPrefabPosition, poster2OffsetPrefabRotation, trackedImage);
                break;
            case "poster3":
                SetInstance(poster3Hotel, poster3OffsetPrefabPosition, poster3OffsetPrefabRotation, trackedImage);
                break;
            default:
                SetInstance(false, Vector3.zero, new Vector3(-90, 0, -180), trackedImage);
                break;
        }
    }

    void SetInstance(bool hotel, Vector3 position, Vector3 rotation, ARTrackedImage parent)
    {
        GameObject instance = hotel ? instanceHotel : instanceGardien;
        if (instance != null)
        {
            instance.transform.SetParent(parent.transform, false);
            instance.SetActive(true);
            instance.transform.localPosition = position;
            instance.transform.localEulerAngles = rotation;
            currentInstance = instance;
        }
    }

    void UpdatePrefabPosition(ARTrackedImage trackedImage)
    {
    }

    //public void OnSliderPositionChanged()
    //{
    //    posterOffsetPrefabPosition = new Vector3(sliders[0].value, sliders[1].value, sliders[2].value);
    //    instanceGardien.transform.position = posterOffsetPrefabPosition;
    //}

    //public void OnSliderRotationChanged()
    //{
    //    posterOffsetPrefabRotation = new Vector3(sliders[3].value, sliders[4].value, sliders[5].value);
    //    instanceGardien.transform.localEulerAngles = posterOffsetPrefabRotation;
    //}
}
