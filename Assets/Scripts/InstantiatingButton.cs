using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiatingButton : MonoBehaviour {
    public Text userStockText;
     int userStock;
    public GameObject burgerPrefab;
    public GameObject juicePrefab;
    public GameObject bedPrefab;
    public GameObject moneyPrefab;
    public Vector3 burgerPos, juicePos, bedPos, moneyPos;
    
    GameObject burger;
    GameObject juice;
    GameObject bed;
    GameObject money;


    // Use this for initialization
    void Start () {
        userStock = 0;
        StartCoroutine(StockRefresh(FindObjectOfType<GameTime>()));
            
     
        

    }
    IEnumerator StockRefresh(GameTime time) //get a stock every twenty minutes
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(time.timeSpeed*20);
        while (true)
        {
            userStock++;
            yield return waitForSeconds;
        }
    }

    public void CreateBurger()
    {
        if (userStock>0 && burger == null || !burger.activeInHierarchy)
        {
            burger = GameObject.Instantiate(burgerPrefab);
            burger.transform.position = burgerPos;
            userStock--;
        }
        
        //GameObject.Instantiate(prefab);
    }

    public void CreateDrink()
    {
        if (userStock > 0 && juice == null || !juice.activeInHierarchy)
        {
            juice = GameObject.Instantiate(juicePrefab);
            juice.transform.position = juicePos;
            userStock--;
        }

    }
    public void CreateBed()
    {
        if (userStock > 0 && bed == null || !bed.activeInHierarchy)
        {
            bed = GameObject.Instantiate(bedPrefab);
            bed.transform.position = bedPos;
            userStock--;
        }

    }
    public void CreateMoney()
    {
        if (userStock > 0 && money == null || !money.activeInHierarchy)
        {
            money = GameObject.Instantiate(moneyPrefab);
            money.transform.position = moneyPos;
            userStock--;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        userStockText.text = "Number of Placables: "+userStock;
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
