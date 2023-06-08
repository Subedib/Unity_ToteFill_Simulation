using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventClick : MonoBehaviour
{
    [SerializeField] Text displayName, displayStatus;
    [SerializeField] Dropdown changeStatus;
    private GameObject selectedObj;

    private void Start()
    {
        changeStatus.onValueChanged.AddListener(delegate
        {
            StatusChanged(changeStatus, selectedObj);
        });
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform)
                {
                    if (hit.transform.GetComponent<Collider>().isTrigger)
                    {
                        PrintStation(hit.transform.gameObject);
                        selectedObj = hit.transform.gameObject;
                    }
                        
                }
            }
        }
        
    }

    private void PrintStation(GameObject go)
    {
        //print(go.name);
        displayName.text = go.name;
        // print(go.GetComponent<Conveyor_Master>().status);
        if (go.GetComponent<Conveyor_Master>().status == 0) displayStatus.text = "None";
        if (go.GetComponent<Conveyor_Master>().status == 1) displayStatus.text = "Empty";
        if (go.GetComponent<Conveyor_Master>().status == 2) displayStatus.text = "Filled";
        if (go.GetComponent<Conveyor_Master>().status == 3) displayStatus.text = "Rejected";

    }

    private void StatusChanged(Dropdown dropdown, GameObject go)
    {
        print(go.transform.name);
        print(dropdown.value);
        if (dropdown.value == 0) go.transform.gameObject.GetComponent<Conveyor_Master>().status = 0;
        if (dropdown.value == 1) go.transform.gameObject.GetComponent<Conveyor_Master>().status = 1;
        if (dropdown.value == 2) go.transform.gameObject.GetComponent<Conveyor_Master>().status = 2;
        if (dropdown.value == 3) go.transform.gameObject.GetComponent<Conveyor_Master>().status = 3;
    }
}
