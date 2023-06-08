using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using EIP.AllenBradley;
using EIP.AllenBradley.Models.Events;
using EIP.AllenBradley.Models;


[UnitSubtitle("collects Tags Updates")]
public class PLCTagsRead : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;
    [DoNotSerialize]
    public ControlOutput outputTrigger;
    [DoNotSerialize]
    public ValueInput IpAddress;
    [DoNotSerialize]
    public ValueInput CLXTags;
    [DoNotSerialize]
    public ValueInput SlotNum;
    [DoNotSerialize]
    public ValueInput LogicxPLC;
    [DoNotSerialize]
    public ValueInput LogicxTask;
    [DoNotSerialize]
    public ValueOutput result;
    
    private string resultValue;
    
    
    LogixTagHandler MyTagH;
    LogixTask logixTask;
    LogixDevice dev = new LogixDevice("PLC_Name", System.Net.IPAddress.Parse("10.144.5.11"), 0);
    List<LogixTagHandler> tags = new List<LogixTagHandler>();


    protected override void Definition()
    {
        //LogixRadixConvertor.GetBinaryString
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {

            string Text = "";
            if (flow.GetValue<LogixDevice>(LogicxPLC).IsConnected)
            {
                //logixTask = new LogixTask(flow.GetValue<LogixDevice>(LogicxPLC));
                //Text = "===================EIPConnect======================";
               // Text = Text + "PLC Name: " + flow.GetValue<LogixDevice>(LogicxPLC).Name;
                //Text = Text + "\r\n" + "PLC Type: " + flow.GetValue<LogixDevice>(LogicxPLC).Family.ToString();
               // Text = Text + "<<****>>" + "PLC IP-Address: " + BitConverter.ToString(flow.GetValue<LogixDevice>(LogicxPLC).Address.GetAddressBytes()); //NodePLC.Address.Address.ToString();
               // Text = Text + "<<****>>" + "PLC Connected: " + flow.GetValue<LogixDevice>(LogicxPLC).IsConnected;
               // Text = Text + "<<****>>" + "===End===";


                tags = flow.GetValue<List<LogixTagHandler>>(CLXTags);


                //foreach (LogixTag logixTag in tags)
                //{
                //    try
                //    {
                //        if ((bool)logixTag.ReadValue.Report.IsSuccessful)
                //        {
                //            Text = Text + "<*>"  ;
                //            //Text = Text + "<<*>>" + "Value: " + BitConverter.ToInt32(logixTag.ReadValue.Report.Data.ToArray()[0], 0);
                //            Text = Text  + "Type: " + logixTag.Type.Name;

                //            if (logixTag.Type.Name == "DINT" || logixTag.Type.Name == "INT")
                //            {
                //                Text = Text + "val:" +  LogixRadixConvertor.GetNumericString(logixTag.ReadValue.Report.Data[0]);
                //            }

                //            if (logixTag.Type.Name == "BOOL")
                //            {
                //                Text = Text + "val:" +  LogixRadixConvertor.GetBoolString(logixTag.ReadValue.Report.Data[0][0]);
                //            }

                //            //Text = Text + "<<*>>" + "==End==";

                //        }
                //    }

                //    catch
                //    {

                //    }

                //}
                foreach (LogixTagHandler logixTag in tags)
                {
                    try
                    {
                        if ((bool)logixTag.ReadValue.Report.IsSuccessful)
                        {
                            Text = Text + logixTag.Name +":>";
                            //Text = Text + "<<*>>" + "Value: " + BitConverter.ToInt32(logixTag.ReadValue.Report.Data.ToArray()[0], 0);
                            Text = Text + "Type: " + logixTag.Type.Name;

                            if (logixTag.Type.Name == "DINT" || logixTag.Type.Name == "INT")
                            {
                                Text = Text + "val:" + LogixRadixConvertor.GetNumericString(logixTag.ReadValue.Report.Data[0]);
                               
                            }

                            if (logixTag.Type.Name == "BOOL")
                            {
                                Text = Text + "val:" + LogixRadixConvertor.GetBoolString(logixTag.ReadValue.Report.Data[0][0]);
                            }

                            Text = Text + "//" ;

                        }
                    }

                    catch
                    {

                    }

                }
            }
            resultValue = Text;
            //resultListValue = binayakTags;
            //Debug.Log(resultListValue);
            flow.GetValue<LogixTask>(LogicxTask).Begin(tags);
            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");



        CLXTags = ValueInput<List<List<LogixTagHandler>>>("CLXTag", new List<List<LogixTagHandler>>());
        LogicxPLC = ValueInput<LogixDevice>("CLXPlc", new LogixDevice("PLC_Name", System.Net.IPAddress.Parse("10.144.5.11"), 0));
        LogicxTask = ValueInput<LogixTask>("CLXTask", new LogixTask(dev));
        result = ValueOutput<string>("result", (flow) => { return resultValue; });
       
    }
}
