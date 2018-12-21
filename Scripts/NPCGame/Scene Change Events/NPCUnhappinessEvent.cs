using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCUnhappinessEvent : MonoBehaviour {

    GameTime gameClock;
    public ReissNPCController[] NPCPrecon; //npcs that can execute this event
    public string scenePostcon;

    private bool condition;
    
    // Use this for initialization
    void Start()
    {
        gameClock = FindObjectOfType<GameTime>();
    }

    // Update is called once per frame
    void Update()
    {
        condition = false;
        Debug.Log(gameObject.name);

         foreach (ReissNPCController a in NPCPrecon)
            {
                if (a.getHappiness()<50)
                {
                    condition = true;
                    break;
                }
            }
       

        if (condition)
        { 
            SceneManager.LoadScene(scenePostcon);
        }


    }
}
