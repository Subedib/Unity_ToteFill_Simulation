using System;
using Unity.VisualScripting;
using UnityEngine;
using EIP.AllenBradley;
using EIP.AllenBradley.Models.Events;
using EIP.AllenBradley.Models;


public class PLCNode : Unit
{

    [DoNotSerialize] // No need to serialize ports.
    public ControlInput inputTrigger; //Adding the ControlInput port variable

    [DoNotSerialize] // No need to serialize ports.
    public ControlOutput outputTrigger;//Adding the ControlOutput port variable.

    [DoNotSerialize] // No need to serialize ports
    public ValueInput myValueA; // Adding the ValueInput variable for myValueA

    [DoNotSerialize] // No need to serialize ports
    public ValueInput myValueB; // Adding the ValueInput variable for myValueB

    [DoNotSerialize] // No need to serialize ports
    public ValueOutput result; // Adding the ValueOutput variable for result

    private string resultValue; // Adding the string variable for the processed result value

    protected override void Definition() //The method to set what our node will be doing.
    {


        //Making the ControlInput port visible, setting its key and running the anonymous action method to pass the flow to the outputTrigger port.
        //inputTrigger = ControlInput("inputTrigger", (flow) => { return outputTrigger; });

        //The lambda to execute our node action when the inputTrigger port is triggered.
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            //Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
            resultValue = flow.GetValue<string>(myValueA) + flow.GetValue<string>(myValueB) + "!!!";
            return outputTrigger;
        });


        //Making the ControlOutput port visible and setting its key.
        outputTrigger = ControlOutput("outputTrigger");

        //Making the myValueA input value port visible, setting the port label name to myValueA and setting its default value to Hello.
        myValueA = ValueInput<string>("myValueA", "Hello ");
        //Making the myValueB input value port visible, setting the port label name to myValueB and setting its default value to an empty string.
        myValueB = ValueInput<string>("myValueB", string.Empty);
        //Making the result output value port visible, setting the port label name to result and setting its default value to the resultValue variable.
        result = ValueOutput<string>("result", (flow) => { return resultValue; });

    }

   
}
