using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffordanceRestock : MonoBehaviour {
    bool cooldown = false;
    float cooldownTime;

	// Use this for initialization
	void Start () {
        cooldownTime = FindObjectOfType<GameTime>().timeSpeed * 100;

        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ResetCooldown()
    {
        cooldown = false;
    }
    public void RestockAffordnces()
    {
        if (cooldown == false)
        {
            Debug.Log("Restocked");
            foreach (Affordances a in FindObjectsOfType<Affordances>())
            {
                a.ReStock();
            }
            cooldown = true;
            Invoke("ResetCooldown", cooldownTime);
            
        }

    }
}
