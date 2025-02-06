using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YearManager : MonoBehaviour
{
    public int year {get ; private set;}

    public List<int> listOfYears = new List<int> {1700,1800,1900,1950,2000};

    public UnityEvent updateYear;


    public void changeYear(int year){

        this.year = year;
        updateYear?.Invoke();

    }

    public void changeYearToNext(){
        this.year = listOfYears[(listOfYears.FindIndex(a => a == year)+1)%listOfYears.Count];
        updateYear?.Invoke();
    }
    

}
