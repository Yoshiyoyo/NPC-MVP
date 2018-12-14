using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    // Use this for initialization
    ReissNPCController[] npcList;
    public string[] eventList= { "Hello" };
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
	void Update () {
		
	}
}
