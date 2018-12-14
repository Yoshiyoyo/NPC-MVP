using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDesireView : MonoBehaviour
{
    public Text displayText;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    private Ray ray;
    RaycastHit hit;
    void Update()
    {
        displayText.text = "";
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out hit))
        //{

        foreach (ReissNPCController character in FindObjectsOfType<ReissNPCController>())
        {
            displayText.text += character.name + ":\n";

            for (int i = 0; i < character.desires.Length; i++)
            {
                displayText.text += character.categories[i];
                if (character.desires[i] >= character.normalThresholds[0])
                {
                    displayText.text += "Satisfied";
                }
                else if (character.desires[i] >= character.normalThresholds[1])
                {
                    displayText.text += "Okay";
                }
                else if (character.desires[i] >= character.normalThresholds[2])
                {
                    displayText.text += "Want";
                }
                else //need
                    displayText.text += "Need";
                displayText.text += "\n";
            }
            displayText.text += "\n";
        }
        //Debug.Log(hit.collider.name);
        // }

    }
    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }
}
