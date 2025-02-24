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

    public GameObject logo;
    public GameObject screenshotBtn;

    public GameObject timechangeBtn;

    public GameObject timechangeMenu;
    void Awake()
    {
        current = this;
    }

    void Update()
    {
        if (Screen.orientation == ScreenOrientation.Portrait){
            logo.transform.localScale = new Vector3(1,1,1);
            screenshotBtn.transform.localScale = new Vector3(1,1,1);
            timechangeBtn.transform.localScale = new Vector3(1,1,1);
            timechangeMenu.transform.localScale = new Vector3(1,1,1);      
        }
        else {
            logo.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            screenshotBtn.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
            timechangeBtn.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
            timechangeMenu.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        }
    }

    public void SetText(string text)
    {
        textDisplay.text = text;
    }

}
