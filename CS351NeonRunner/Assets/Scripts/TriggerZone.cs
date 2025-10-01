using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class TriggerZone : MonoBehaviour
{

    public TMP_Text output;

    public string textToDisplay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger zone entered by " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            output.text = textToDisplay;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
