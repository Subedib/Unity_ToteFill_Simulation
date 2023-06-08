using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool totePresent;
    public GameObject sensorLED;
    // Start is called before the first frame update
    void Start()
    {
        totePresent = false;
        sensorLED.GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        totePresent = true;
        sensorLED.GetComponent<Renderer>().material.color = Color.green;
    }
    private void OnTriggerExit (Collider other)
    {
        totePresent = false;
        sensorLED.GetComponent<Renderer>().material.color = Color.red;
    }
}
