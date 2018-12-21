using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    // Use this for initialization
    ReissNPCController[] npcList;
    public Text gameStateStatus;
    
    public string[] eventList= { "Hello" };
    public float farThreshold = 5;
	void Start () {
		
	}

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void RefreshNPCList()
    {
        npcList = FindObjectsOfType<ReissNPCController>();
    }

    public ReissNPCController[] GetNPCList()
    {
        return npcList;
    }

    public void RemoveNPC(ReissNPCController npc)
    {
        npc.gameObject.SetActive(false);
        RefreshNPCList();
    }
	
	// Update is called once per frame
    public void UpdateGame(string update)
    {
        if (gameStateStatus != null)
            gameStateStatus.text = update;
    }
	void Update () {

		
	}
}
