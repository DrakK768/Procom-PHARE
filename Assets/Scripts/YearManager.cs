using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class YearManager : MonoBehaviour
{
    public int year = 2000;
    private int i = -1;

    public List<int> listOfYears = new List<int> {1700,1800,1900,1950,2000};

    public UnityEvent updateYear;

    public  TextMeshProUGUI debugText;

    void Start()
    {
        //updateYear.AddListener(debug); 
    }


    public void changeYear(int year){

        this.year = year;
        debugText.text = "menu ok";
        updateYear?.Invoke();

    }

    public void changeYearToNext(){
        i=(i+1)%listOfYears.Count;
        year = listOfYears[i];
        debugText.text = "tap ok";
        updateYear?.Invoke();
    }
    
    public void debug(){
        debugText.text = "event ok";
    }

}
