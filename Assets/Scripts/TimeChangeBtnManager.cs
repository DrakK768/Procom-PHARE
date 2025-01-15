using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChangeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                
            }
            
        }
    }

  float pressTime = 0;

void TapOrLongTouch()
{
    if (Input.touchCount <= 0) return;
    
    var touch = Input.GetTouch(0);

    switch(touch.phase)
    {
        // Maybe you also want to reset when the touch was moved?
        //case TouchPhase.Moved:
        case TouchPhase.Began:
            pressTime = 0;
            break;

        case TouchPhase.Stationary:
            pressTime += Time.deltaTime;
            break;

        case TouchPhase.Ended:
        case TouchPhase.Canceled:
            if (pressTime < 0.5f)
            {
                //Do something;
            }
            else 
            {
                //Do something;
            }
            pressTime = 0;
            break;
    }
}
}
