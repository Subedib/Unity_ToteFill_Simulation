using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTableSideSimple : MonoBehaviour
{
    public float Eulerspeed;
    public GameObject FlatObj;
    public float FlatSpeed;
    Rigidbody Side_rb;
    Material mymaterial;
    public bool Reverse = false;
    // Start is called before the first frame update
    void Start()
    {
        this.FlatSpeed = this.FlatObj.GetComponent<TurnTable_Master>().speed;//t , FlatSpeed = 向こうのspeed
        this.Reverse = this.FlatObj.GetComponent<TurnTable_Master>().Reverse;
        this.Eulerspeed = (1 * Mathf.PI) / (400 * FlatSpeed) ;
        /// <summary>
        /// ConveyorSimple.speed
        /// 9π     1       1
        /// —―― * ―――― = ――――― π
        /// 20t   180     400t
        /// Eulaerspeed
        /// </summary>
        /// <typeparam name="Rigidbody"></typeparam>
        /// <returns></returns>

        this.Side_rb = GetComponent<Rigidbody>();
        this.mymaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //3byougotonikousin
        //if(Time.fixedTime >= 1)UnityEditor.EditorApplication.isPaused = true;
        if (this.FlatObj.GetComponent<TurnTable_Master>().run)
        {
            if (!this.FlatObj.GetComponent<TurnTable_Master>().Reverse)
            {
                var material = this.mymaterial;
                Vector2 offset = material.mainTextureOffset;
                offset += Vector2.right * 1 * Time.deltaTime * Eulerspeed * 0.0000000000000000000000000000000000000000000000000000000000000001f/*0.005f;// */;
                material.mainTextureOffset = offset;
            }
            if (this.FlatObj.GetComponent<TurnTable_Master>().Reverse)
            {
                var material = this.mymaterial;
                Vector2 offset = material.mainTextureOffset;
                offset += Vector2.left * 1 * Time.deltaTime * Eulerspeed * 0.0000000000000000000000000000000000000000000000000000000000000001f/*0.005f;// */;
                material.mainTextureOffset = offset;
            }
        }
    }

    void FixedUpdate()
    {
        Quaternion pos = Side_rb.rotation;
        if (!this.FlatObj.GetComponent<TurnTable_Master>().Reverse && this.FlatObj.GetComponent<TurnTable_Master>().run)
        {
            Quaternion movepos = Quaternion.Euler(0, (400 * FlatSpeed) / (51 * Mathf.PI), 0);
            Side_rb.MoveRotation(pos * movepos);
        }
        if (this.FlatObj.GetComponent<TurnTable_Master>().Reverse && this.FlatObj.GetComponent<TurnTable_Master>().run)
        {
            Quaternion movepos = Quaternion.Euler(0, -(400 * FlatSpeed) / (51 * Mathf.PI), 0);
            Side_rb.MoveRotation(pos * movepos);
        }

    }
}
