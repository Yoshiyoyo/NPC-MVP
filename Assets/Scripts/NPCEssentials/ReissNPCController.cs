using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ReissNPCController : MonoBehaviour {
    //to remove later
    public string[] categories = { "Food: ", "Exploration: ", "Sleep: ", "Drink: " };

    public Text displayText;
    public GameObject idlePosition;

    public float speed;
    public GameObject desire;
    // put some of reiss's stuff here
    public float[] desires; //hunger, curiosity, sleepiness, thirst
    Vector3 movement;
    Affordances[] allAffordances;
    float[] weightmatrix;
    GameTime currTime;
    NavMeshAgent agent;

    //stuff used for calculating priorities
    public float currWeight;
    public float maxweight;
    int maxweightindex;
    float[] testAffordanceDesires;
    float[] postAffordanceDesires;
    float desireWeight;

    public float happiness = 100; //time-based progressive value for desires
    public float desireGoal = 1; //desire weight, below generates unhappiness
    public float attention_Span = 1; //affects rate of happiness changing
    bool inTask = false; //whether or not in the state of applying an affordance, currently not acutally used

    public float hunger=75;
    public float curiosity=75;
    public float sleepiness=75;
    public float thirst=75;

    public float farThreshold = 5; //if item is too far, less desire to go to it. this is the threshold
    public Affordances lastAffordanceUsed; //last affordance used has less weight when deciding what afforadance to go to

    bool flag = false;

    public GameTime clock;

    //for example's sake, putting growths into public values instead of off of a .json
    public float hungergrowth;
    public float curiositygrowth;
    public float sleepinessgrowth;
    public float thirstgrowth;
    

    public float[] growths;

    //weights and thresholds for varioius states of desirability, not implemented yet
     public float[] normalThresholds = { 80, 60, 40 };
   /*/float[] preferredThresholds = { 70, 50, 30 };
    float[] dislikeThresholds = { 90, 70, 50 };*/

    float needWeight = 1;
    float wantWeight = .5F;
    float mehWeight = .3F;
    float neutralWeight = 0;
    // Use this for initialization
    void Start()
    {
        //displayText.text = "";
        agent = GetComponent<NavMeshAgent>();
        clock = FindObjectOfType<GameTime>();
        desires = new float[] { hunger, curiosity, sleepiness, thirst };
        //currTime = FindObjectOfType<GameTime>();

        //remove later
        growths = new float[] { hungergrowth, curiositygrowth, sleepinessgrowth, thirstgrowth };
        postAffordanceDesires = new float[4];
        StartCoroutine(ProgressDesires(clock.timeSpeed));
        StartCoroutine(DecideAffordance(clock.timeSpeed));
        

    }
    IEnumerator ProgressDesires(float time) //progress desire changes as well as happiness
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(time);
        
        // to remove later, end up using tags to "differentiate" agents
        for (; ; )
        {
            for (int i = 0; i < desires.Length; i++)
            {
                desires[i] -= growths[i]; //delete this later

                //postAffordanceDesires[i] = desires[i];
            }
            NormalizeDesires(ref desires);
            currWeight = FindDesireWeight(desires);
            happiness += (desireGoal-currWeight)*attention_Span;
            if (happiness > 100)
                happiness = 100;
            else if (happiness < 0)
                happiness = 0;
            
            yield return waitForSeconds;
        }
    }
    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        //displayText.text = "";
        
        //Debug.Log("Mouse is over GameObject.");
    }
    void OnMouseExit()
    {
        //displayText.text = "";
    }


    IEnumerator DecideAffordance(float time)
    {

       
        WaitForSeconds waitForSeconds = new WaitForSeconds(time);
        for (; ; )
        {
            while (happiness>50) //content, no need to decide on anything
            {
                agent.SetDestination(idlePosition.transform.position);
                yield return waitForSeconds;
            }
            
            inTask = false;
            NormalizeDesires(ref desires);
            maxweight = 0;

            maxweightindex = -1;
            movement.Set(0, 0, 0);
            allAffordances = FindObjectsOfType<Affordances>(); //find all gameobjects that contain affordances
            weightmatrix = new float[allAffordances.Length];
            for (int i = 0; i < allAffordances.Length; i++) //logic goes here for calculating affordance weight
            {
                weightmatrix[i] = currWeight-FindDesireWeight(ApplyAffordance(allAffordances[i])); //see weight after applying affordance
               
                if (weightmatrix[i] >0)
                {
                   
                    movement = transform.position - allAffordances[i].transform.position;
                    if (movement.magnitude > farThreshold) //if too far, apply a penalty, to be slightly complicated further in future
                        weightmatrix[i] /= 2;
                    if (allAffordances[i] == lastAffordanceUsed) //if last affordance used, apply a penalty, to be a buffer of N affordances in the future
                        weightmatrix[i] /= 4;

                    Debug.Log(maxweight + " " + weightmatrix[i]);


                    if (maxweight <weightmatrix[i])
                    {
                        maxweight = weightmatrix[i];
                        maxweightindex = i;
                        //desireAffordance
                    }
                }
            }
            //Debug.Log(maxweight + " " + currWeight);
            if (maxweightindex>-1) //if there is a affordance desired, work towards it
            {

                //Debug.Log(maxweight);
                //Debug.Log(weightmatrix[0]);
                desire = allAffordances[maxweightindex].gameObject;

                movement = transform.position - desire.transform.position;

                //afterwards, check if need to apply affordance, at the moment just sees if its a specific distance away
                Debug.Log(movement.magnitude);
                if (movement.magnitude < 1)
                {

                    
                    postAffordanceDesires = ApplyAffordance(desire.GetComponent<Affordances>());
                    Debug.Log("Used " + desire.name);
                    
                    for (int i = 0; i < desires.Length; i++)
                    {
                        desires[i] = postAffordanceDesires[i];
                    }
                    flag = true;
                    desire.GetComponent<Affordances>().Use(gameObject);
                    inTask = true;
                    lastAffordanceUsed = desire.GetComponent<Affordances>();
                    yield return new WaitForSeconds(desire.GetComponent<Affordances>().duration * clock.timeSpeed);
                    // postAffordanceDesires[0] = 151;

                    //NormalizeDesires(ref desires);
                    //Debug.Log(desires[0] + " " + desires[1] + " " + desires[2] + " " + desires[3]);
                }
                else
                    agent.SetDestination(desire.transform.position);
                //Debug.Log(gameObject.name);
                //Debug.Log(agent.destination);
            }
            yield return waitForSeconds;
        }
    }
    float FindDesireWeight (float[] desirematrix)
    {
        desireWeight = 0;
        foreach (float parameter in desirematrix)
        {
            if (parameter >= normalThresholds[0]) 
            {
                desireWeight += neutralWeight ;
            }
            else if (parameter >= normalThresholds[1])
            {
                desireWeight += mehWeight ;
            }
            else if (parameter >= normalThresholds[2])
            {
                desireWeight += wantWeight ;
            }
            else //need
                desireWeight += needWeight ;
        }
        return desireWeight;

    }

    void NormalizeDesires(ref float[] matrix) //if any desires above 100, set to 100, if below 0 set to 0
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            if (matrix[i] > 100)
                matrix[i] = 100;
             else if (matrix[i] < 0)
                matrix[i] = 0;
        }
    }
    public float[] ApplyAffordance(Affordances affordance) //returns desire matrix with affordance applied
    {
        float[] affordances = affordance.getAffordances();
        if (affordances.Length!=desires.Length)
        {
            Debug.Log("Uh, something's wrong with desire lengths");
        }
        else
        {
            for (int i=0;i<affordances.Length;i++)
            {
                postAffordanceDesires[i] = desires[i]+affordances[i];


            }
            NormalizeDesires(ref postAffordanceDesires);
            //affordance.Use(gameObject);
            //Debug.Log(affordance.stock);
            
        }
        return postAffordanceDesires;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate() //physics calculations
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Debug.Log("got here");
        /*if (movement.magnitude > 0) //perform momvement
        {
            //Debug.Log("got here");
            transform.position += -1*speed * movement;
            //transform.position += -1 * speed * movement.normalized;
        }*/
        // movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // GetComponent<Rigidbody>().AddForce(-1*speed * movement);

    }
}
