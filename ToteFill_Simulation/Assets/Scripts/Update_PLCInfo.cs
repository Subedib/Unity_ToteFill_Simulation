using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Update_PLCInfo : MonoBehaviour
{
    [SerializeField] GameObject go;

    public void ReadPLCName(string s)
    {
        PLC_Connect.plcName = s;
    }
    public void ReadIPAdd(string s)
    {
        PLC_Connect.ipAddress = s;
    }
    public void ReadSlotNo(int i)
    {
        PLC_Connect.slotNum = i;
    }
    public void ReadConnect2PLC(bool b)
    {
        PLC_Connect.connectToPLC = b;
    }

    private void Update()
    {
        go.GetComponent<Text>().text = PLC_Connect.connectionStatus;
    }

}
