using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCGameEvent : MonoBehaviour {

    GameTime gameClock;
    public int hourPrecon;
    public ReissNPCController[] NPCPrecon;
    public string scenePostcon;

    private bool condition;

	// Use this for initialization
	void Start () {
        gameClock = FindObjectOfType<GameTime>();
		
	}
	
	// Update is called once per frame
	void Update () {
        condition = true;
        Debug.Log(gameObject.name);
        if (hourPrecon==gameClock.hour)
        {
            foreach (ReissNPCController a in NPCPrecon)
            {
                if (!a.gameObject.activeInHierarchy)
                {
                    condition = false;
                    break;
                }
            }
        }
        else
            condition = false;

        if (condition)
        { 

            
            SceneManager.LoadScene(scenePostcon);
        }

		
	}
}
