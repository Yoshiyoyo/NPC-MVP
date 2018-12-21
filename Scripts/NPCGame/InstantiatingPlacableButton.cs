using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatingPlacableButton : MonoBehaviour {
    public float moveSpeed = .1f;
    GameObject placedObject;
    public Vector3 position;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (placedObject != null)
        {
           
            if (Input.GetKey("up"))
            {
                placedObject.transform.position += new Vector3(moveSpeed, 0, 0);
                
            }
            if (Input.GetKey("right"))
            {
                placedObject.transform.position += new Vector3(0, 0, -moveSpeed);
                //placedObject.transform.Translate(new Vector3(0, 0, moveSpeed));
            }
            if (Input.GetKey("left"))
            {
                placedObject.transform.position += new Vector3(0, 0, moveSpeed);
            }
            if (Input.GetKey("down"))
            {
                placedObject.transform.position += new Vector3(-moveSpeed, 0, 0);

            }
            if (Input.GetKey("return"))
            {
                placedObject = null;
            }
        }




    }
    public void PlaceObject(GameObject obj)
    {

        placedObject=GameObject.Instantiate(obj);
        placedObject.transform.position = position;
     

    }
}
