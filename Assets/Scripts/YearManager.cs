using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class YearManager : MonoBehaviour {
// Description : Script to manage the year, can be use to implement a changing model according to the year set
    public int year = 2000;
    private int i = -1;

    public List<int> listOfYears = new List<int> {1700,1800,1900,1950,2000};

    public UnityEvent updateYear;


    public void changeYear(int year){

        this.year = year;
        updateYear?.Invoke();

    }

    public void changeYearToNext(){
        i=(i+1)%listOfYears.Count;
        year = listOfYears[i];
        updateYear?.Invoke();
    }

}
