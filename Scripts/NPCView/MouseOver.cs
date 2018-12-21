using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour {

    GameObject hoveredItem;
    Ray ray;
    RaycastHit hit;
    Text text;

    GameObject temp;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {

        ray=Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            temp = hit.transform.gameObject;
            if (temp.CompareTag("Child"))
            {
                hoveredItem = temp;
                
                GenerateNPCInformation(hoveredItem.GetComponent<ReissNPCController>());
            }
            else if (temp.CompareTag("Adult"))
            {
                hoveredItem = temp;
               
                GenerateNPCInformation(hoveredItem.GetComponent<ReissNPCController>());
            }
            else if (hoveredItem!=null)
            {
                GenerateNPCInformation(hoveredItem.GetComponent<ReissNPCController>());
            }

        }


    }
    void GenerateNPCInformation(ReissNPCController npc)
    {
        if (npc.CompareTag("Child"))
        {
            text.text="Child\n";
        }
        else if (npc.CompareTag("Adult"))
        {
            text.text = "Adult\n";
        }

        float happiness = npc.getHappiness();
        float[] desires = npc.desires;
        float[] desireDecays = npc.decays;
        if (happiness > 75) //happiness stuff
        {
            text.text += "Status: Happy";
        }
        else if (happiness > 40)
        {
            text.text += "Status: Normal";
        }
        else
            text.text += "Status: Critical";
        text.text += "\nCritical Desires:\n";

        if (desires[0]<30)
        {
            text.text += "Hunger\n";
        }
        if (desires[1]<30)
        {
            text.text += "Curiosity\n";
        }
        if (desires[2]<30)
        {
            text.text += "Sleepiness\n";
        }
        if (desires[3]<30)
        {
            text.text += "Thirst\n";
        }

        text.text += "Interests:\n";
        if (desireDecays[0]>.2)
        {
            text.text += "Eating\n";
        }
        if (desireDecays[1]>.2)
        {
            text.text += "Entertainment\n";
        }
        if (desireDecays[2]>.2)
        {
            text.text += "Relaxing\n";
        }
        if (desireDecays[3]>.2)
        {
            text.text += "Drinking";
        }



    }
}
