using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CIPlib;


public class PLC_Connect : MonoBehaviour
{
    public static string plcName = "Test_PLC";
    public static string ipAddress = "10.144.41.33";
    public static int slotNum = 0;
    public static bool sessionRegistered = false;
    public static bool connectToPLC = false;
    public static string connectionStatus = "None";
    public static CIP plc;

    // Start is called before the first frame update
    void Start()
    {
        plc = new CIP(ipAddress);
        sessionRegistered = plc.RegisterSession();
    }

}
