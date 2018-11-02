using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affordances : MonoBehaviour {
    public int stock;
    public bool infiniteUse; //if using the item reduces its stock
    public int maxusers; //the amount of users it can have simultaneously 
    public float hunger;
    public float curiosity;
    public float sleepiness;
    public float thirst;

    public bool isTrapped;
    public bool canBeTrapped;

    public DisplayTime clock;
    public bool isConsumable;
    private float[] affordances;
    public int restockFrequency;
    private int currRestock;
	// Use this for initialization
	void Awake () {
        affordances = new float[] { hunger, curiosity, sleepiness, thirst };
        clock = FindObjectOfType<DisplayTime>();
        currRestock = 1;
    }

    void Start()
    {
        isTrapped = false;
    }
    public void SetTrap(int cycles)
    {
        if (canBeTrapped)
        {
            isTrapped = true;
           // Debug.Log(gameObject.name + "is now trapped");
            StartCoroutine(ActiveTrap(cycles));
        }

    }
    public IEnumerator ActiveTrap (int cycles)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(clock.timeSpeed);
        for (int i=1;i<=cycles;i++)
        {
            yield return waitForSeconds;
        }
     //   Debug.Log(gameObject.name + " is no longer trapped.");
        isTrapped = false;
    }
    /*public IEnumerator SetTrap (int cycles)
    {
        for (int i = 0; i < cycles; i++)
        {
            Debug.Log("Hi");
            isTrapped = true;
            Debug.Log(gameObject.name + "is now trapped");
            yield return new WaitForSeconds(cycles * clock.timeSpeed);
        }
        Debug.Log(gameObject.name + " is no longer trapped.");
        isTrapped = false;
    }*/
    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        //Debug.Log("Mouse is over GameObject.");
    }

    /// <summary>
    /// finds the affordances in the format hunger, curiosity, sleepiness
    /// </summary>
    public float[] getAffordances() 
    {
        if (stock > 0|| infiniteUse)
            return affordances;
        else
            return new float[affordances.Length];
    }
            
    public void Use(GameObject user) //Reduces stock if applicable, otherwise does nothing
    {
        //Debug.Log(gameObject.name);
        if (!infiniteUse)
        {
            stock--;
            if (stock <= 0 && isConsumable)
                gameObject.SetActive(false);
        }
        if (isTrapped)
        {
            user.SetActive(false);
        }
    }

    public void ReStock(int stocks)
    {
        stock += stocks;
    }
	
	// Update is called once per frame
	void Update () {
        if (restockFrequency > 0)
        {
            if (currRestock == restockFrequency)
            {
                stock++;
                currRestock = 0;
            }
            else
                currRestock++;
        }
        if (!infiniteUse && stock==0)
            //Debug.Log("Out of Stock");
        if (isConsumable && stock <= 0) //if consumable out of stock, remove it
            gameObject.SetActive(false);
        

        
		
	}

    void OnCollisionEnter(Collision collide)
    {
        /*ReissNPCController recipient= collide.collider.GetComponent<ReissNPCController>();
        if (recipient)
        {
            if (stock > 0)
            {
                recipient.ApplyAffordance(affordances);
                stock--;
            }
            else
                Debug.Log("No stock Left.");
        }*/
       /* if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }*/
    }
}
