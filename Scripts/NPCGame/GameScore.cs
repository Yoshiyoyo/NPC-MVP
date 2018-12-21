using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {
    GameTime clock;
    private float score=0;
    ReissNPCController[] currentNPCs;
    private bool gameOngoing = true; //whether the game is over
    public Text gameScoreText;
    public int timeLimit = 1000;


	// Use this for initialization
	void Start () {
        clock = GetComponent<GameTime>();
        StartCoroutine(UpdateScore(clock.timeSpeed*10));
        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator UpdateScore(float time)
    {
        float currNPCHappiness;
        WaitForSeconds waitForSeconds = new WaitForSeconds(time);
        while (gameOngoing)
        {
            timeLimit -= 10;
            currentNPCs = FindObjectsOfType<ReissNPCController>();
            foreach (ReissNPCController npc in currentNPCs)
            {
                currNPCHappiness = npc.getHappiness();
                score += currNPCHappiness;
                if (currNPCHappiness == 0) //game over condition

                    gameOngoing = false;
            }
            if (gameScoreText != null)
            {
                if (timeLimit>0)
                {
                    gameScoreText.text = "Time remaining: " + timeLimit+"\n";
                }
                else
                {
                    gameOngoing = false;

                }
                gameScoreText.text += "Score: " + score;
            }
            
            

            yield return waitForSeconds;
        }
        if (timeLimit == 0)// success
        {
            gameScoreText.text = "Time limit over. Success!\n";
        }
        else
            gameScoreText.text = "Game over due to Unhappiness\n";
        gameScoreText.text += "Final Score: " + score+"\n";
        gameScoreText.text += " Budget: ";
        int size = FindObjectsOfType<Affordances>().Length;
        if (size < 10)
        {
            gameScoreText.text += "Low";
        }
        else if (size < 20)
        {
            gameScoreText.text += "Medium";
        }
        else
        {
            gameScoreText.text += "High";
        }
        foreach (ReissNPCController a in GameObject.FindObjectsOfType<ReissNPCController>())
        {
            a.gameObject.SetActive(false);
        }
    }
    public float getScore()
    {
        return score;
    }
}
