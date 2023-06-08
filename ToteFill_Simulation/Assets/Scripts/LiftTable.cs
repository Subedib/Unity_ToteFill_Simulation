using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTable : MonoBehaviour
{
    public float offset;
    public float speed;
    public bool liftUp, liftDown, upPos, downPos;
    private Vector3 a;
    private Vector3 b;

    // Start is called before the first frame update
    void Start()
    {
        a = transform.position;
        b = new Vector3(a.x, a.y + offset, a.z);
        //Debug.Log(a);
       // Debug.Log(b);
       // Debug.Log("---------------------");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position.y);
       // Debug.Log(b.y);

        if (transform.position.y <= b.y && liftUp)
        {
            downPos = false;
            transform.position = new Vector3(a.x, transform.position.y + speed/60 ,a.z);
        }
        if (transform.position.y >= b.y)
        {
            upPos = true;
        }

        if (transform.position.y >= a.y && liftDown)
        {
            upPos = false;
            transform.position = new Vector3(a.x, transform.position.y - speed / 60, a.z);
        }
        if (transform.position.y <= a.y )
        {
            downPos = true;
        }

    }
}
