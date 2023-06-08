using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraCont : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private bool useEdgeScrolling = false;
    [SerializeField] private bool useDragPan = false;
    [SerializeField] private float fieldOfViewMax = 50;
    [SerializeField] private float fieldOfViewMin = 10;
    [SerializeField] private float zoomSpeed = 10f;
    private bool dragPanMoveActive;
    private Vector2 lastMousePosition;
    private float targetFOV = 50;

    // Update is called once per frame
    void Update()
    {

        HandleCameraMovement();
        if(useDragPan) HandleCameraMovementDragPan();
        if(useEdgeScrolling) HandleCameraMovementEdgeScrolling();
        HandleCameraRotation();
        HandleCameraZoom();

    }
    private void HandleCameraMovement()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = +1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = +1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = -1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        float moveSpeed = 50f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    private void HandleCameraMovementEdgeScrolling()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);
       
        int edgeScrollSize = 20;
        if (Input.mousePosition.x < edgeScrollSize) inputDir.x = -1f;
        if (Input.mousePosition.y < edgeScrollSize) inputDir.z = -1f;
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDir.x = +1f;
        if (Input.mousePosition.y > Screen.width - edgeScrollSize) inputDir.z = +1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        float moveSpeed = 50f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraMovementDragPan()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);
        if (Input.GetMouseButtonDown(1))
        {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1)) dragPanMoveActive = false;

        Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;
        float dragPanSpeed = 1.5f;
        inputDir.x = mouseMovementDelta.x * dragPanSpeed;
        inputDir.z = mouseMovementDelta.y * dragPanSpeed;
        lastMousePosition = Input.mousePosition;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        float moveSpeed = 50f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    
    private void HandleCameraRotation()
    {
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

        float rotateSpeed = 100f;
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }

    private void HandleCameraZoom()
    {
        if (Input.mouseScrollDelta.y > 0) targetFOV -= 5;
        if (Input.mouseScrollDelta.y < 0) targetFOV += 5;
        targetFOV = Mathf.Clamp(targetFOV, fieldOfViewMin, fieldOfViewMax);
        cinemachineVirtualCamera.m_Lens.FieldOfView= Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
        
    }
}
