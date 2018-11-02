using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ReissNPCController : MonoBehaviour {
    //to remove later
    public string[] categories = { "Food: ", "Exploration: ", "Sleep: ", "Drink: " };

    public Text displayText;

    public float speed;
    //public GameObject player;
    //public GameObject protagonistSwitch;
    public GameObject desire;
    // put some of reiss's stuff here
    public float[] desires; //hunger, curiosity, sleepiness, thirst
    Vector3 movement;
    Affordances[] allAffordances;
    float[] weightmatrix;
    DisplayTime currTime;
    NavMeshAgent agent;

    //stuff used for calculating priorities
    float currWeight;
    float maxweight;
    int maxweightindex;
    float[] testAffordanceDesires;
    float[] postAffordanceDesires;
    float desireWeight;

    public float hunger;
    public float curiosity;
    public float sleepiness;
    public float thirst;

    bool flag = false;

    public DisplayTime clock;

    //for example's sake, putting growths into public values instead of off of a .json
    public float hungergrowth;
    public float curiositygrowth;
    public float sleepinessgrowth;
    public float thirstgrowth;

    public float[] growths;

    //weights and thresholds for varioius states of desirability
    public float[] normalThresholds = { 80, 60, 40 };
    float[] preferredThresholds = { 70, 50, 30 };
    float[] dislikeThresholds = { 90, 70, 50 };

    float needWeight = 1;
    float wantWeight = .5F;
    float mehWeight = .3F;
    float neutralWeight = 0;
    // Use this for initialization
    void Start()
    {
        //displayText.text = "";
        agent = GetComponent<NavMeshAgent>();
        desires = new float[] { hunger, curiosity, sleepiness, thirst };
        //currTime = FindObjectOfType<DisplayTime>();

        //remove later
        growths = new float[] { hungergrowth, curiositygrowth, sleepinessgrowth, thirstgrowth };
        postAffordanceDesires = new float[4];
        StartCoroutine(DecideDesire(clock.timeSpeed));

        //desires = new float[] { 5f, 5f, 5f };
        //for now just using hardcoded stuff
        //desires = new float[] { 5f, 5f, 5f };

        //desires = new float[] {5f,5f,5f};
        

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
    IEnumerator DecideDesire(float time)
    {

       
        WaitForSeconds waitForSeconds = new WaitForSeconds(time);
        for (; ; )
        {
            //Debug.Log("Woah look");
            // to remove later, end up using tags to "differentiate" agents
            for (int i = 0; i < desires.Length; i++)
            {
                desires[i] -= growths[i]; //delete this later

                //postAffordanceDesires[i] = desires[i];
            }
            //Debug.Log("Before normalizing " + FindDesireWeight(desires));
            NormalizeDesires(ref desires);
            currWeight = FindDesireWeight(desires);
            //Debug.Log(currWeight);
            maxweight = currWeight;

            maxweightindex = -1;
            movement.Set(0, 0, 0);
            //Affordances desireAffordance; 
            allAffordances = FindObjectsOfType<Affordances>(); //find all gameobjects that contain affordances
            weightmatrix = new float[allAffordances.Length];
            //Debug.Log("Checkpoint");
            for (int i = 0; i < allAffordances.Length; i++) //logic goes here for calculating affordance weight
            {
                // float[] a = allAffordances[i].getAffordances();
                //Debug.Log(allAffordances[i]);
                weightmatrix[i] = FindDesireWeight(ApplyAffordance(allAffordances[i]));
                


                //Debug.Log("got here");
                /*for (int j=0;j<desires.Length;j++)
                {
                    if (a[j]<0)
                    {
                        //Debug.Log("Got here"+weightmatrix[i]);
                        weightmatrix[i] += desires[j] * a[j] / Vector3.Distance(transform.position, allAffordances[i].transform.position);
                        //Debug.Log("Now " + weightmatrix[i]);
                    }
                    else
                        weightmatrix[i] += Mathf.Pow(Mathf.Min(desires[j] * a[j]) , 2f) / Vector3.Distance(transform.position, allAffordances[i].transform.position);

                    //Debug.Log(weightmatrix[i]);
                }*/
                if (maxweight > weightmatrix[i])
                {
                    maxweight = weightmatrix[i];
                    maxweightindex = i;
                    //desireAffordance
                }
            }
            //Debug.Log(maxweight + " " + currWeight);
            if (maxweight < currWeight) //if there is a affordance desired, work towards it
            {

                //Debug.Log(maxweight);
                //Debug.Log(weightmatrix[0]);
                desire = allAffordances[maxweightindex].gameObject;
                //Debug.Log(desire.name + " " +Vector3.Distance(transform.position, desire.transform.position));
                // Debug.Log(desire.name);

                movement = transform.position - desire.transform.position;
                //afterwards, check if need to apply affordance, at the moment just sees if its a specific distance away
                //Debug.Log(movement.magnitude + " " + desire.name);
                
                if (movement.magnitude < 6)
                {


                    postAffordanceDesires = ApplyAffordance(desire.GetComponent<Affordances>());
                    for (int i = 0; i < desires.Length; i++)
                    {
                        desires[i] = postAffordanceDesires[i];
                    }
                    flag = true;
                    desire.GetComponent<Affordances>().Use(gameObject);
                    // postAffordanceDesires[0] = 151;

                    //NormalizeDesires(ref desires);
                    //Debug.Log(desires[0] + " " + desires[1] + " " + desires[2] + " " + desires[3]);
                }
                else
                    agent.SetDestination(desire.transform.position);
                /*if (desire.name.Equals("sofa_1"))
                    Debug.Log("Oh god");
                else
                    Debug.Log(desire.name);*/
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
        //Debug.Log("Uhm");
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
