using System;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor_Master : MonoBehaviour
{
    public enum VectorDirection
    {
        forward,//back,
        back,//forward,
        right,//left,
        left//right,
    }
    public VectorDirection ChosenVec, initialDirection;
    public bool Reverse = false;
    public bool run = false;
    public float speed;
    public bool reverseDone = false;
    public int status;
    private bool parentPresent = false;
    Rigidbody MyrbBody, MyrbBody1,MyrbBody2,MyrbBody3;
    Material mymaterial, mymaterial1, mymaterial2, mymaterial3;

    List<SerializerInput> rwString = new List<SerializerInput>();

    // Start is called before the first frame update
    void Start()
    {
        rwString.Add(new SerializerInput(this.name + ".PE"));
        rwString.Add(new SerializerInput(this.name + ".Run"));
        rwString.Add(new SerializerInput(this.name + ".Reverse"));
        rwString.Add(new SerializerInput(this.name + ".Status"));
        rwString.Add(new SerializerInput(this.name + ".LiftUp"));
        rwString.Add(new SerializerInput(this.name + ".LiftDown"));
        rwString.Add(new SerializerInput(this.name + ".UpPos"));
        rwString.Add(new SerializerInput(this.name + ".DownPos"));

        if (this.transform.parent == null) parentPresent = false;
        else parentPresent = true;

        initialDirection = ChosenVec;

        //Get Children
        List <Transform> children = GetChildren(transform);
        foreach (Transform child in children)
        {
            
            if (child.name == "OnBelt")
            {
                MyrbBody = child.GetComponent<Rigidbody>();
                mymaterial = child.GetComponent<Renderer>().material;
                
            }
            if (child.name == "UnderBelt")
            {
                MyrbBody1 = child.GetComponent<Rigidbody>();
                mymaterial1 = child.GetComponent<Renderer>().material;

            }

            if (child.name == "OnBelt1")
            {
                MyrbBody2 = child.GetComponent<Rigidbody>();
                mymaterial2 = child.GetComponent<Renderer>().material;

            }
            if (child.name == "UnderBelt1")
            {
                MyrbBody3 = child.GetComponent<Rigidbody>();
                mymaterial3 = child.GetComponent<Renderer>().material;

            }
        }
    }

    List<Transform> GetChildren(Transform parent)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in parent)
        {
            children.Add(child);
        }
        return children; 
    }

    void Update()
    {
        if (PLC_Connect.connectToPLC)
        {
            try
            {
                if (PLC_Connect.sessionRegistered)
                {
                    PLC_Connect.connectionStatus = "PLC Connection Successful";
                    foreach (SerializerInput s in rwString)
                    {
                        object value = new object();
                        PLC_Connect.plc.Read(s.tagName, out value);
                        if (s.tagName == this.name + ".Run") this.run = Convert.ToBoolean(value);
                        if (s.tagName == this.name + ".Reverse") this.Reverse = Convert.ToBoolean(value);
                        if (s.tagName == this.name + ".Status") this.status = Convert.ToInt32(value);
                        if (s.tagName == this.name + ".PE") PLC_Connect.plc.Write(s.tagName, Convert.ToInt32(this.transform.GetChild(12).gameObject.GetComponent<Trigger>().totePresent));

                        if (s.tagName == this.name + ".LiftUp" && parentPresent) this.transform.GetComponentInParent<LiftTable>().liftUp = Convert.ToBoolean(value);
                        if (s.tagName == this.name + ".LiftDown" && parentPresent) this.transform.GetComponentInParent<LiftTable>().liftDown = Convert.ToBoolean(value);
                        if (s.tagName == this.name + ".UpPos" && parentPresent) PLC_Connect.plc.Write(s.tagName, Convert.ToInt32(this.transform.GetComponentInParent<LiftTable>().upPos));
                        if (s.tagName == this.name + ".DownPos" && parentPresent) PLC_Connect.plc.Write(s.tagName, Convert.ToInt32(this.transform.GetComponentInParent<LiftTable>().downPos));

                    }

                }
            }
            catch
            {
                PLC_Connect.connectionStatus = "PLC Connection NOT Successful";
            }

        }

        ScrollUV();
    }

    void FixedUpdate()
    {
        if (run)
        {
            switch (ChosenVec)
            {
                case VectorDirection.forward:
                    Vector3 posB = MyrbBody.position;
                    MyrbBody.position += Vector3.back * speed * Time.fixedDeltaTime;
                    MyrbBody.MovePosition(posB);

                    Vector3 posB1 = MyrbBody1.position;
                    MyrbBody1.position += Vector3.forward * speed * Time.fixedDeltaTime;
                    MyrbBody1.MovePosition(posB1);

                    Vector3 posB2 = MyrbBody2.position;
                    MyrbBody2.position += Vector3.back * speed * Time.fixedDeltaTime;
                    MyrbBody2.MovePosition(posB2);

                    Vector3 posB3 = MyrbBody3.position;
                    MyrbBody3.position += Vector3.forward * speed * Time.fixedDeltaTime;
                    MyrbBody3.MovePosition(posB3);

                    break;


                case VectorDirection.back:
                    Vector3 posU = MyrbBody.position;
                    MyrbBody.position += Vector3.forward * speed * Time.fixedDeltaTime;
                    MyrbBody.MovePosition(posU);

                    Vector3 posU1 = MyrbBody1.position;
                    MyrbBody1.position += Vector3.back * speed * Time.fixedDeltaTime;
                    MyrbBody1.MovePosition(posU1);

                    Vector3 posU2 = MyrbBody2.position;
                    MyrbBody2.position += Vector3.forward * speed * Time.fixedDeltaTime;
                    MyrbBody2.MovePosition(posU2);

                    Vector3 posU3 = MyrbBody3.position;
                    MyrbBody3.position += Vector3.back * speed * Time.fixedDeltaTime;
                    MyrbBody3.MovePosition(posU3);
                    break;

                case VectorDirection.right:
                    Vector3 posL = MyrbBody.position;
                    MyrbBody.position += Vector3.left * speed * Time.fixedDeltaTime;
                    MyrbBody.MovePosition(posL);

                    Vector3 posL1 = MyrbBody1.position;
                    MyrbBody1.position += Vector3.right * speed * Time.fixedDeltaTime;
                    MyrbBody1.MovePosition(posL1);

                    Vector3 posL2 = MyrbBody2.position;
                    MyrbBody2.position += Vector3.left * speed * Time.fixedDeltaTime;
                    MyrbBody2.MovePosition(posL2);

                    Vector3 posL3 = MyrbBody3.position;
                    MyrbBody3.position += Vector3.right * speed * Time.fixedDeltaTime;
                    MyrbBody3.MovePosition(posL3);
                    break;

                case VectorDirection.left:
                    Vector3 posR = MyrbBody.position;
                    MyrbBody.position += Vector3.right * speed * Time.fixedDeltaTime;
                    MyrbBody.MovePosition(posR);

                    Vector3 posR1 = MyrbBody1.position;
                    MyrbBody1.position += Vector3.left * speed * Time.fixedDeltaTime;
                    MyrbBody1.MovePosition(posR1);

                    Vector3 posR2 = MyrbBody2.position;
                    MyrbBody2.position += Vector3.right * speed * Time.fixedDeltaTime;
                    MyrbBody2.MovePosition(posR2);

                    Vector3 posR3 = MyrbBody3.position;
                    MyrbBody3.position += Vector3.left * speed * Time.fixedDeltaTime;
                    MyrbBody3.MovePosition(posR3);
                    break;
            }

        }
    }

    void ScrollUV()
    {
        if (run)
        {
            if (!Reverse)
            {
                ChosenVec = initialDirection;
                reverseDone = false;
                // Debug.Log(mymaterial);
                var material = this.mymaterial;
                Vector2 offset = material.mainTextureOffset;
                offset += Vector2.up * speed * Time.deltaTime / material.mainTextureScale.y;
                material.mainTextureOffset = offset;

                var material1 = this.mymaterial1;
                Vector2 TextureScale1 = this.mymaterial1.mainTextureScale;
                TextureScale1 = new Vector2(1, -3f);
                material1.mainTextureScale = TextureScale1;
                Vector2 offset1 = material1.mainTextureOffset;
                offset1 += Vector2.down * speed * Time.deltaTime / material1.mainTextureScale.y;
                material1.mainTextureOffset = offset1;

                var material2 = this.mymaterial2;
                Vector2 offset2 = material2.mainTextureOffset;
                offset2 += Vector2.up * speed * Time.deltaTime / material2.mainTextureScale.y;
                material2.mainTextureOffset = offset2;

                var material3 = this.mymaterial3;
                Vector2 TextureScale3 = this.mymaterial3.mainTextureScale;
                TextureScale3 = new Vector2(1, -3f);
                material3.mainTextureScale = TextureScale3;
                Vector2 offset3 = material3.mainTextureOffset;
                offset3 += Vector2.down * speed * Time.deltaTime / material3.mainTextureScale.y;
                material3.mainTextureOffset = offset3;
            }

            if (Reverse)
            {
                if (!reverseDone)
                {
                    switch (ChosenVec)
                    {
                        case VectorDirection.forward:
                            ChosenVec = VectorDirection.back;
                            reverseDone = true;
                            break;
                        case VectorDirection.back:
                            ChosenVec = VectorDirection.forward;
                            reverseDone = true;
                            break;
                        case VectorDirection.right:
                            ChosenVec = VectorDirection.left;
                            reverseDone = true;
                            break;
                        case VectorDirection.left:
                            ChosenVec = VectorDirection.right;
                            reverseDone = true;
                            break;
                    }
                }
                if (reverseDone)
                {
                    var material = this.mymaterial;
                    Vector2 TextureScale = this.mymaterial.mainTextureScale;
                    TextureScale = new Vector2(1, -3f);
                    material.mainTextureScale = TextureScale;
                    Vector2 offset = material.mainTextureOffset;
                    offset += Vector2.down * speed * Time.deltaTime / material.mainTextureScale.y;
                    material.mainTextureOffset = offset;

                    var material1 = this.mymaterial1;
                    Vector2 offset1 = material1.mainTextureOffset;
                    offset1 += Vector2.up * speed * Time.deltaTime / material1.mainTextureScale.y;
                    material1.mainTextureOffset = offset1;

                    var material2 = this.mymaterial2;
                    Vector2 TextureScale2 = this.mymaterial2.mainTextureScale;
                    TextureScale2 = new Vector2(1, -3f);
                    material2.mainTextureScale = TextureScale2;
                    Vector2 offset2 = material2.mainTextureOffset;
                    offset2 += Vector2.down * speed * Time.deltaTime / material2.mainTextureScale.y;
                    material2.mainTextureOffset = offset2;

                    var material3 = this.mymaterial3;
                    Vector2 offset3 = material3.mainTextureOffset;
                    offset3 += Vector2.up * speed * Time.deltaTime / material3.mainTextureScale.y;
                    material3.mainTextureOffset = offset3;
                }
            }

        }
    }
}

