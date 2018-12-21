using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiatingButton : MonoBehaviour {
    //public Text userStockText;
     //int userStock;
    public GameObject itemPrefab;
    public Vector3 itemPos;
    
    GameObject item;


    // Use this for initialization
    void Start () {
        //userStock = 0;
        StartCoroutine(StockRefresh(FindObjectOfType<GameTime>()));
            
     
        

    }
    IEnumerator StockRefresh(GameTime time) //get a stock every twenty minutes
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(time.timeSpeed*20);
        while (true)
        {
            //userStock++;
            yield return waitForSeconds;
        }
    }

    public void Createitem()
    {
        if (item == null || !item.activeInHierarchy)
        {
            item = GameObject.Instantiate(itemPrefab);
            item.transform.position = itemPos;
            //userStock--;
        }
        /*if (userStock>0 && item == null || !item.activeInHierarchy)
        {
            item = GameObject.Instantiate(itemPrefab);
            item.transform.position = itemPos;
            //userStock--;
        }*/
        
        //GameObject.Instantiate(prefab);
    }
    public void Createitem(GameObject itemToCreate)
    {
        if (item == null || !item.activeInHierarchy)
            GameObject.Instantiate(itemToCreate);
        /*if (userStock>0 && item == null || !item.activeInHierarchy)
        {
            item = GameObject.Instantiate(itemPrefab);
            item.transform.position = itemPos;
            //userStock--;
        }*/

        //GameObject.Instantiate(prefab);
    }


    // Update is called once per frame
    void Update ()
    {
        //userStockText.text = "Number of Placables: "+userStock;
        /*if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

            }
        }*/


            //if (Input.getKeyDown)

    }
}
