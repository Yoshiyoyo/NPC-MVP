using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTime : MonoBehaviour {

    public GameObject newPlotLocation;

    public int minute, hour, timeLimit;
    public float timeSpeed;
    public Text timeText;
    public Text trappedObjectText;
    public int trapDuration;
    float trapChance = 5;
    Affordances[] listOfAffordances;

	// Use this for initialization
	void Start () {
        minute = 0;
        hour = 0;
        StartCoroutine(ProgressTime(timeSpeed));
        
		
	}
	
	// Update is called once per frame
	void Update () {
        trappedObjectText.text = "Trapped objects: \n";
        foreach (Affordances a in FindObjectsOfType<Affordances>())
        {
            if (a.isTrapped)
                trappedObjectText.text += a.name + "\n";
        }
		
	}

    IEnumerator ProgressTime(float time)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(time);

        for (; ; ) 
        {
            listOfAffordances = FindObjectsOfType<Affordances>();
            timeText.text = hour + ":" + minute;
           // Debug.Log(hour + ":" + minute);
            //increment the minute
            minute++;
            if (minute == 60)
            {
                hour++;
                //userStock++;
                minute = 0;
            }
            if (hour == 24)
            {
                hour = 0;
                trapChance *= 1.05F;
            }
            if (hour==timeLimit)
            {
                int survivors = FindObjectsOfType<ReissNPCController>().Length;
                timeText.text = survivors+" survived to the next day. What now?";
                if (survivors>=2)
                    newPlotLocation.SetActive(true);
                break;
            }

            //do checks to see if to make a trap here
            if (trapChance>=Random.Range(0,100))
            {
                //spawn a trap
                //Debug.Log("Trap made");
                listOfAffordances[Random.Range(0, listOfAffordances.Length - 1)].SetTrap(trapDuration);
                
            }

            yield return waitForSeconds;
        }
    }

    
}
