using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnTable_Master : MonoBehaviour
{
    public enum VectorDirection
    {
        forward,//back,
        back,//forward,
        right,//left,
        left//right,
    }
    public VectorDirection ChosenVec, initialDirection;
    public GameObject RotateObj;
    public bool Reverse = false;
    public float speed;
    public bool reverseDone = false;
    public bool run = false;
    public int status;
    private bool parentPresent = false;
    Rigidbody MyrbBody, MyrbBody1;
    Material mymaterial, mymaterial1;

    List<SerializerInput> rwString = new List<SerializerInput>();

    // Start is called before the first frame update
    void Start()
    {
        rwString.Add(new SerializerInput(this.name + ".PE1"));
        rwString.Add(new SerializerInput(this.name + ".PE2"));
        rwString.Add(new SerializerInput(this.name + ".Run"));
        rwString.Add(new SerializerInput(this.name + ".Reverse"));
        rwString.Add(new SerializerInput(this.name + ".Status"));
        rwString.Add(new SerializerInput(this.name + ".LiftUp"));
        rwString.Add(new SerializerInput(this.name + ".LiftDown"));
        rwString.Add(new SerializerInput(this.name + ".UpPos"));
        rwString.Add(new SerializerInput(this.name + ".DownPos"));
        rwString.Add(new SerializerInput(this.name + ".Rotate90"));
        rwString.Add(new SerializerInput(this.name + ".Rotate0"));
        rwString.Add(new SerializerInput(this.name + ".Pos90"));
        rwString.Add(new SerializerInput(this.name + ".Pos0"));

        if (this.transform.parent == null) parentPresent = false;
        else parentPresent = true;
        print(parentPresent.ToString());

        initialDirection = ChosenVec;

        //Get Children
        List<Transform> children = GetChildren(transform);
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
                    foreach (SerializerInput s in rwString)
                    {
                        //print(s.tagName);
                        object value = new object();
                        PLC_Connect.plc.Read(s.tagName, out value);
                        if (s.tagName == this.name + ".Run") this.run = Convert.ToBoolean(value);
                        if (s.tagName == this.name + ".Reverse") this.Reverse = Convert.ToBoolean(value);
                        if (s.tagName == this.name + ".Status") this.status = Convert.ToInt32(value);
                        if (s.tagName == this.name + ".PE1") PLC_Connect.plc.Write(s.tagName, Convert.ToInt32(this.transform.GetChild(8).gameObject.GetComponent<Trigger>().totePresent));
                        if (s.tagName == this.name + ".PE2") PLC_Connect.plc.Write(s.tagName, Convert.ToInt32(this.transform.GetChild(9).gameObject.GetComponent<Trigger>().totePresent));

                        if (s.tagName == this.name + ".LiftUp" && parentPresent) this.transform.GetComponentInParent<LiftTable>().liftUp = Convert.ToBoolean(value);
                        if (s.tagName == this.name + ".LiftDown" && parentPresent) this.transform.GetComponentInParent<LiftTable>().liftDown = Convert.ToBoolean(value);
                        if (s.tagName == this.name + ".UpPos" && parentPresent) PLC_Connect.plc.Write(s.tagName, Convert.ToInt32(this.transform.GetComponentInParent<LiftTable>().upPos));
                        if (s.tagName == this.name + ".DownPos" && parentPresent) PLC_Connect.plc.Write(s.tagName, Convert.ToInt32(this.transform.GetComponentInParent<LiftTable>().downPos));
                        if (s.tagName == this.name + ".Rotate90" && parentPresent) this.transform.GetComponentInParent<Rotate>().rotate90 = Convert.ToBoolean(value);
                        if (s.tagName == this.name + ".Rotate0" && parentPresent) this.transform.GetComponentInParent<Rotate>().rotate0 = Convert.ToBoolean(value);
                        if (s.tagName == this.name + ".Pos90" && parentPresent) PLC_Connect.plc.Write(s.tagName, Convert.ToInt32(this.transform.GetComponentInParent<Rotate>().rotate90Done));
                        if (s.tagName == this.name + ".Pos0" && parentPresent) PLC_Connect.plc.Write(s.tagName, Convert.ToInt32(this.transform.GetComponentInParent<Rotate>().rotate0Done));

                    }

                }
            }
            catch
            {
                print("TT Not connected to PLC");
            }

        }

        ScrollUV();
    }

    void FixedUpdate()
    {
        if (run)
        {
            if (this.RotateObj.GetComponent<Rotate>().rotate0Done)
            ChosenVec = VectorDirection.back;
            if (this.RotateObj.GetComponent<Rotate>().rotate90Done)
            ChosenVec = VectorDirection.left;
           
            switch (ChosenVec)
            {
                case VectorDirection.forward:
                    Vector3 posB = MyrbBody.position;
                    MyrbBody.position += Vector3.back * speed * Time.fixedDeltaTime;
                    MyrbBody.MovePosition(posB);

                    Vector3 posB1 = MyrbBody1.position;
                    MyrbBody1.position += Vector3.forward * speed * Time.fixedDeltaTime;
                    MyrbBody1.MovePosition(posB1);

                    break;


                case VectorDirection.back:
                    Vector3 posU = MyrbBody.position;
                    MyrbBody.position += Vector3.forward * speed * Time.fixedDeltaTime;
                    MyrbBody.MovePosition(posU);

                    Vector3 posU1 = MyrbBody1.position;
                    MyrbBody1.position += Vector3.back * speed * Time.fixedDeltaTime;
                    MyrbBody1.MovePosition(posU1);

                    break;

                case VectorDirection.right:
                    Vector3 posL = MyrbBody.position;
                    MyrbBody.position += Vector3.left * speed * Time.fixedDeltaTime;
                    MyrbBody.MovePosition(posL);

                    Vector3 posL1 = MyrbBody1.position;
                    MyrbBody1.position += Vector3.right * speed * Time.fixedDeltaTime;
                    MyrbBody1.MovePosition(posL1);

                    break;

                case VectorDirection.left:
                    Vector3 posR = MyrbBody.position;
                    MyrbBody.position += Vector3.right * speed * Time.fixedDeltaTime;
                    MyrbBody.MovePosition(posR);

                    Vector3 posR1 = MyrbBody1.position;
                    MyrbBody1.position += Vector3.left * speed * Time.fixedDeltaTime;
                    MyrbBody1.MovePosition(posR1);

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
                }

            }
        }
    }
}

