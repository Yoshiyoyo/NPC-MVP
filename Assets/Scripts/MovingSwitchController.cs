using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSwitchController : MonoBehaviour {

    public Vector3 toPosition;
    public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            target.transform.position = toPosition;
        }
    }
}
