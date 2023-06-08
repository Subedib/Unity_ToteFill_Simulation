using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using EIP.AllenBradley;
using EIP.AllenBradley.Models.Events;
using EIP.AllenBradley.Models;


[UnitSubtitle("Creat PLC Objet ")]
//[UnitCategory("FirstLevel\SecondLevel")]
public class PLC : Unit
{

    [DoNotSerialize]
    public ControlInput inputTrigger;
    [DoNotSerialize]
    public ControlOutput outputTrigger;
    [DoNotSerialize]
    public ValueInput IpAddress;
    [DoNotSerialize]
    public ValueInput SlotNum;
    [DoNotSerialize]
    public ValueOutput result;
    [DoNotSerialize]
    public ValueOutput CLXPLC;
    [DoNotSerialize]
    public ValueOutput CLXTask;

    private string resultValue;
    private LogixDevice logixDevice;
    private LogixTask logixTask;


    protected override void Definition()
    {


        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {


            String PLC_Name = "LGV_PLC";
            logixDevice = new LogixDevice(PLC_Name,
                                      System.Net.IPAddress.Parse(flow.GetValue<string>(IpAddress)),
                                      BitConverter.GetBytes(flow.GetValue<int>(SlotNum))[0]);
            logixDevice.Family = DeviceFamily.ControlLogix;
            logixDevice.Connect();
            logixDevice.RegisterSession();
            string Text = "";
            //Text = "===================EIPConnect======================";
            Text = Text + "PLC Name: " + logixDevice.Name;
            Text = Text + "\r\n" + "PLC Type: " + logixDevice.Family.ToString();
            Text = Text + "<<****>>" + "PLC IP-Address: " + BitConverter.ToString(logixDevice.Address.GetAddressBytes()); //NodePLC.Address.Address.ToString();
            Text = Text + "<<****>>" + "PLC Connected: " + logixDevice.IsConnected;
            Text = Text + "<<****>>" + "===End===";

            resultValue = Text;

            logixTask = new LogixTask(logixDevice);
            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");


        IpAddress = ValueInput<string>("IPaddress", "10.144.41.33 ");
        SlotNum = ValueInput<int>("SlotNum", 0);
       
        result = ValueOutput<string>("result", (flow) => { return resultValue; });
        CLXPLC = ValueOutput<EIP.AllenBradley.LogixDevice>("CLXPLC", (flow) => { return logixDevice; });
        CLXTask = ValueOutput<LogixTask>("CLXTask", (flow) => { return logixTask; });
    }
   
}


