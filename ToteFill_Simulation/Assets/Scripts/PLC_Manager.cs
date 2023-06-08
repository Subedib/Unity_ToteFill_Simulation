using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using EIP.AllenBradley;

public class PLC_Manager : MonoBehaviour
{
    public static string plcName = "Test_PLC";
    public static string ipAddress = "10.144.41.33";
    public static  int slotNum  = 0;
    public static bool connectToPLC = false;
    public static string connectionStatus = "None";

    private LogixDevice logixDevice;
    private LogixTask logixTask;
    [SerializeField] string readTag1 = "Read_Tag1", readTag2 = "Read_Tag2", readTag3 = "Read_Tag3", readTag4 = "Read_Tag4", readTag5 = "Read_Tag5";
    [SerializeField] string writeTag1 = "Write_Tag1", writeTag2 = "Write_Tag2", writeTag3 = "Write_Tag3", writeTag4 = "Write_Tag4", writeTag5 = "Write_Tag5";
    List<string> rwString = new List<string>();
    List<LogixTagHandler> rwtags = new List<LogixTagHandler>();
    
    private void Start()
    {
        rwString.Add(readTag1);
        rwString.Add(readTag2);
        rwString.Add(readTag3);
        rwString.Add(readTag4);
        rwString.Add(readTag5);

        rwString.Add(writeTag1);
        rwString.Add(writeTag2);
        rwString.Add(writeTag3);
        rwString.Add(writeTag4);
        rwString.Add(writeTag5);
        

        logixDevice = new LogixDevice("plcName", System.Net.IPAddress.Parse(ipAddress), BitConverter.GetBytes(slotNum)[0]);
        logixDevice.Family = DeviceFamily.ControlLogix;
        logixDevice.Connect();
        logixDevice.RegisterSession();
        logixTask = new LogixTask(logixDevice);

        foreach (string s in rwString)
        {
            LogixTagHandler myTag = new(s);
            myTag.InitState();
            myTag.GetReadedValueText();
            myTag.ReadValue.Report.Init();
            myTag.Radix = TagValueRadix.Decimal;
            if (!rwtags.Contains(myTag)) rwtags.Add(myTag);
        }
        logixTask.Begin(rwtags);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(ipAddress);
        if (connectToPLC)
        {
            try
            {
                if (logixDevice.IsConnected)
                {

                    connectionStatus = "PLC Connection Successful";
                    foreach (LogixTagHandler logixTag in rwtags)
                    {
                        try
                         {
                             if ((bool)logixTag.ReadValue.Report.IsSuccessful)
                             {
                                print(logixTag.Name + " :");
                                print(logixTag.Type.Name + " :");
                                print(logixTag.ReadValue.Report.Data[0][0]);
                                
                             }
                         }
                         catch
                         {
                             print("Read not sucessful !!!");
                         }
                    }

                }
            }
            catch
            {
                connectionStatus = "PLC Connection Successful";
            }
            
        }
           

        }
    
}
