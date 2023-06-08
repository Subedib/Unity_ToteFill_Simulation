using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMPZ : MonoBehaviour
{
    public GameObject parentModel;

    public float rotationspeed = 400.0f;

    private Vector3 mouseWorldPosStart;
    public float zoomscale = 10.0f;
    private float maxFieldOfView = 160.0f;
    private float minFieldOfView = 0.0f;
    private float defaultFieldOfView = 60.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Mouse2))
        {
            CamOrbit();
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F))
        {
            FitToScreen();
        }
        if (!Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(2))
        {
            mouseWorldPosStart = GetPerspectivePos();
        }
        if (!Input.GetMouseButton(2) && !Input.GetKey(KeyCode.LeftShift))
        {
            Pan();
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void CamOrbit()
    {
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            float verticalInput = Input.GetAxis("Mouse Y") * rotationspeed * Time.deltaTime;
            float horizontalInput = Input.GetAxis("Mouse X") * rotationspeed * Time.deltaTime;
            transform.Rotate(Vector3.right, -verticalInput);
            transform.Rotate(Vector3.up, horizontalInput, Space.World);
        }
    }

    private Bounds GetBound(GameObject parentGameObj)
    {
        Bounds bound = new Bounds(parentGameObj.transform.position, Vector3.zero);
        var rList = parentGameObj.GetComponentsInChildren(typeof(Renderer));
        foreach (Renderer r in rList)
        {
            bound.Encapsulate(r.bounds);
        }
        return bound;
    }

    public void FitToScreen()
    {
        Camera.main.fieldOfView = defaultFieldOfView;
        Bounds bound = GetBound(parentModel);
        Vector3 boundSize = bound.size;
        float boundDiagonal = Mathf.Sqrt((boundSize.x * boundSize.x) + (boundSize.y * boundSize.y) + (boundSize.z * boundSize.z));
        float camDistanceToBoundCenter = boundDiagonal / 2.0f / (Mathf.Tan(Camera.main.fieldOfView / 2.0f * Mathf.Deg2Rad));
        float camDistanceToBoundWithOffset = camDistanceToBoundCenter + boundDiagonal/2.0f- (Camera.main.transform.position - transform.position).magnitude;
        transform.position = bound.center + (-transform.forward * camDistanceToBoundWithOffset);
    }
    private void Pan()
    {
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            Vector3 mouseWorldPosDiff = mouseWorldPosStart - GetPerspectivePos();
            transform.position += mouseWorldPosDiff;
        }
    }

    private void Zoom(float zoomDiff)
    {
        if (zoomDiff != 0)
        {
            mouseWorldPosStart = GetPerspectivePos();
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - zoomDiff * zoomscale, minFieldOfView, maxFieldOfView);
            Vector3 mouseWorldPosDiff = mouseWorldPosStart - GetPerspectivePos();
            transform.position += mouseWorldPosDiff;
        }

    }

    private Vector3 GetPerspectivePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(transform.forward, 0.0f);
        float dist;
        plane.Raycast(ray, out dist);
        return ray.GetPoint(dist);
    }
}

