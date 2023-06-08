using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorSimple : MonoBehaviour
{
    public float speed;// m/s

   public enum VectorDirection
   {
       forward,//back,
       back,//forward,
       right,//left,
       left//right,
   }
    public VectorDirection ChosenVec;
    public bool Reverse = false;
    Rigidbody MyrbBody;
    Material mymaterial;

    // Start is called before the first frame update
    void Start()
    {
        this.MyrbBody = this.gameObject.GetComponent<Rigidbody>();
        this.mymaterial = this.gameObject.GetComponent<Renderer>().material;
    }
        // Update is called once per frame
        void Update()
        {
            ScrollUV();
        }

        void FixedUpdate()
        {
            switch (ChosenVec)
            {
                case VectorDirection.forward:
                    Vector3 posB = MyrbBody.position;
                    MyrbBody.position += Vector3.back * speed * Time.fixedDeltaTime;
                    MyrbBody.MovePosition(posB);
                    break;


                case VectorDirection.back:
                    Vector3 posU = MyrbBody.position;
                    MyrbBody.position += Vector3.forward * speed * Time.fixedDeltaTime;
                    MyrbBody.MovePosition(posU);
                    break;

                case VectorDirection.right:
                    Vector3 posL = MyrbBody.position;
                    MyrbBody.position += Vector3.left * speed * Time.fixedDeltaTime;
                    MyrbBody.MovePosition(posL);
                    break;

                case VectorDirection.left:
                    Vector3 posR = MyrbBody.position;
                    MyrbBody.position += Vector3.right * speed * Time.fixedDeltaTime;
                    MyrbBody.MovePosition(posR);
                    break;
            }

        }

        void ScrollUV()
        {
            if (!Reverse)
            {
                var material = this.mymaterial;
                Vector2 offset = material.mainTextureOffset;
                offset += Vector2.up * speed * Time.deltaTime / material.mainTextureScale.y;
                material.mainTextureOffset = offset;
            }


            if (Reverse)
            {
                var material = this.mymaterial;

                Vector2 TextureScale = this.mymaterial.mainTextureScale;
                TextureScale = new Vector2(1, -3f);
                material.mainTextureScale = TextureScale;

                Vector2 offset = material.mainTextureOffset;
                offset += Vector2.down * speed * Time.deltaTime / material.mainTextureScale.y;
                material.mainTextureOffset = offset;
            }
        }
    }

