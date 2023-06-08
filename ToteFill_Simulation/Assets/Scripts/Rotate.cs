using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 rotation;
    public bool rotate90, rotate0, rotate90Done, rotate0Done;
    private GameObject collidedObj;

    void Start()
    {
        rotate90 = false;
        rotate0 = false;
        rotate90Done = false;
        rotate0Done = true;
    }
    void OnTriggerEnter(Collider collision)
    {
        collidedObj= collision.gameObject;
        collidedObj.transform.parent = transform;


    }

    private void OnTriggerExit(Collider other)
    {
        collidedObj.transform.parent = null;
    }



    // Update is called once per frame
    void Update()
    {
        if (rotate90)
        {
            rotate0Done = false;
            transform.Rotate(0, 10 * Time.deltaTime, 0);
            //print(collidedObj.transform.name);
            //if(collide)
            //collidedObj.transform.Rotate(0, 10 * Time.deltaTime, 0);
            if (transform.rotation.eulerAngles.y >= 90)
            {
                rotate90 = false;
                rotate90Done = true;      
            }
            //Debug.Log(transform.rotation.eulerAngles.y);
        }
        if (rotate0)
        {
            rotate90Done = false;
            //transform.Rotate(0, -10 * Time.deltaTime, 0);
            //print(collidedObj.transform.name);
            collidedObj.transform.Rotate(0, 10 * Time.deltaTime, 0);
            if (transform.rotation.eulerAngles.y <= 0 || transform.rotation.eulerAngles.y >= 91)
            {
                rotate0 = false;
                rotate0Done = true;
            } 
            //Debug.Log(transform.rotation.eulerAngles.y);
        }
    }
}

