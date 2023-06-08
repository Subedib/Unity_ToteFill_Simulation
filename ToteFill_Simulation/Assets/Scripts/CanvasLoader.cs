using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLoader : MonoBehaviour
{
   
    public void ShowCanvas(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void HideCanvas(GameObject obj)
    {
        obj.SetActive(false);
    }
}
