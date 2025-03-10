using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager current;

    [SerializeField] TMP_Text textDisplay;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject button;

    public GameObject refPos1;
    public GameObject refPos2;
    public GameObject refPos3;
    public GameObject refPos4;

    public GameObject refPosLand1;
    public GameObject refPosLand2;
    public GameObject refPosLand3;
    public GameObject refPosLand4;

    void Awake()
    {
        current = this;
    }

    void Update()
    {
        if (Screen.orientation == ScreenOrientation.Portrait){
            text1.transform.localPosition = refPos1.transform.localPosition;
            text2.transform.localPosition = refPos2.transform.localPosition;
            text3.transform.localPosition = refPos3.transform.localPosition;
            button.transform.localPosition = refPos4.transform.localPosition;
        }
        else {
            text1.transform.localPosition = refPosLand1.transform.localPosition;
            text2.transform.localPosition = refPosLand2.transform.localPosition;
            text3.transform.localPosition = refPosLand3.transform.localPosition;
            button.transform.localPosition = refPosLand4.transform.localPosition;
        }
    }

    public void SetText(string text)
    {
        textDisplay.text = text;
    }

}
