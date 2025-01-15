using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TimeChangeManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private float pressTime = 0;
    public GameObject menu;
    public TextMeshProUGUI text;
    public GameObject mainBtn ;

    private bool down = false;
    private bool up = false;
    void Update()
    {
        TapOrLongTouch();
    }

private void TapOrLongTouch()
{
    if (!down ) return;
    pressTime += Time.deltaTime;
    if (pressTime > 1.5f) {
        down = false;
        up = false;
        menu.SetActive(true);
        mainBtn.SetActive(false);
        pressTime = 0;
    }
    
    if(up){
        text.text = (int.Parse(text.text)+1).ToString();
        down = false;
        up = false;
        pressTime = 0;
        //Change to next year
    }
}

public void changeTime(int year)
{
    text.text = year.ToString();
    menu.SetActive(false);
    mainBtn.SetActive(true);
    //change to selected year
}



    public void OnPointerDown(PointerEventData eventData)
    {
        down = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        up = true;
    }
}
