using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Levha1 : MonoBehaviour
{
    float openDelay;
    float closedDelay;
    public float open;
    public float closed;

    void Start()
    {
        openDelay = 0;
        closedDelay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (openDelay >= open && transform.eulerAngles == new Vector3(0, 50, 90))
        {
            transform.eulerAngles = new Vector3(0, 90, 90);
            openDelay = 0;
            
        }

      //  openDelay += Time.deltaTime;
       // open+= Time.deltaTime;
        if (closedDelay >= closed && transform.eulerAngles == new Vector3(0, 90, 90))
        {
            transform.eulerAngles = new Vector3(0, 50, 90);
            closedDelay = 0;
            
        }
        // closedDelay += Time.deltaTime;
        // closed+= Time.deltaTime;
        if(transform.eulerAngles == new Vector3(0, 90, 90))
        {
            closedDelay += Time.deltaTime;
        }
        else {
            openDelay += Time.deltaTime;
        }


    }
}
