using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    public float speed;
    public GameObject player;
    public GameObject protagonistSwitch;
    public GameObject initialDesire;
    Vector3 movement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate() //physics calculations
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        if (protagonistSwitch.activeSelf == true)
            movement = transform.position - initialDesire.transform.position;
        else
            movement = transform.position - player.transform.position;
        if (movement.magnitude > 1)
            transform.position += -1 * speed * movement.normalized;
        // movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // GetComponent<Rigidbody>().AddForce(-1*speed * movement);

    }
}
