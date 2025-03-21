using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TimeChangeManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
// Description : Script to manage the Time Changer button and its menu
    private float pressTime = 0;
    public GameObject menu;
    public TextMeshProUGUI text;
    public GameObject mainBtn ;

    public UnityEvent<int> yearChanged;

    public UnityEvent yearChangeToNext;

    public YearManager yearManager;


    private bool down = false;
    private bool up = false;

    void Start()
    {
        yearManager.updateYear.AddListener(updateYear);
        yearChanged.AddListener(yearManager.changeYear);
        yearChangeToNext.AddListener(yearManager.changeYearToNext);
    }
    void Update()
    {
        TapOrLongTouch();
    }

private void TapOrLongTouch()
// Check wheter the time button was tapped or long touched, then call the according function
{
    if (!down ) return;
    pressTime += Time.deltaTime;
    if (pressTime > 1.2f) { //long press
        down = false;
        up = false;
        menu.SetActive(true);
        mainBtn.SetActive(false);
        pressTime = 0;
    }
    
    if(up){ //tap
        down = false;
        up = false;
        pressTime = 0;
        yearChangeToNext?.Invoke();
    }
}

    public void OnPointerDown(PointerEventData eventData)
    {
        down = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        up = true;
    }

    private void updateYear(){
        text.text = yearManager.year.ToString();
    }


    public void onChangedYear(int year)
{
    menu.SetActive(false);
    mainBtn.SetActive(true);
    yearChanged?.Invoke(year);
}
}
