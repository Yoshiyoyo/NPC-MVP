using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollText : MonoBehaviour
{
    public string[] text;
    public Text textBox;
    public float scrollSpeed=.1f;

    // Use this for initialization
    void Start()
    {
        textBox.text = "";
        text = FindObjectOfType<GameState>().eventList;
        foreach (string line in text)
        {
            textBox.text += line;
        }
        StartCoroutine(ProgressText(scrollSpeed));
        

    }

    // Update is called once per frame
    void Update()
    {


    }

    IEnumerator ProgressText(float time)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(time);
        for (; ; )
        {
            // Get the current transform position of the panel
            Vector3 _currentUIPosition = gameObject.transform.position;
           // Debug.Log("Current Positon: " + _currentUIPosition);

            // Increment the Y value of the panel 
            Vector3 _incrementYPosition =
             new Vector3(_currentUIPosition.x,
                         _currentUIPosition.y + 1f,
                        _currentUIPosition.z);

            // Change the transform position to the new one
            // Debug.Log("New Position: " + _incrementYPosition);
            gameObject.transform.position = _incrementYPosition;
            yield return waitForSeconds;
        }
    }
}
