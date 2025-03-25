using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CanvasManager : MonoBehaviour
//Description : script to change position of text accord to the screen orientation
//Currently, it's a "bruteforce" way to do it, please don't mind it
{
    public static CanvasManager current;

    [SerializeField] TMP_Text textDisplay;
    public GameObject text1;
    public GameObject text2;

    public GameObject refPos1;
    public GameObject refPos2;
    public GameObject refPosLand1;
    public GameObject refPosLand2;

    void Awake()
    {
        current = this;
    }

    void Update()
    {
        if (Screen.orientation == ScreenOrientation.Portrait){
            text1.transform.localPosition = refPos1.transform.localPosition;
            text2.transform.localPosition = refPos2.transform.localPosition;
        }
        else {
            text1.transform.localPosition = refPosLand1.transform.localPosition;
            text2.transform.localPosition = refPosLand2.transform.localPosition;
        }
    }

    public void SetText(string text)
    {
        textDisplay.text = text;
    }

}
