using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using EIP.AllenBradley;
using EIP.AllenBradley.Models.Events;
using EIP.AllenBradley.Models;


[UnitSubtitle("convert Tags List to Tag object List")]
//[UnitCategory("FirstLevel\SecondLevel")]
public class PLCTagsList : Unit
{
 
    [DoNotSerialize] 
    public ControlInput inputTrigger; 
    [DoNotSerialize] 
    public ControlOutput outputTrigger;
    [DoNotSerialize] 
    public ValueInput PLCtag;
    [DoNotSerialize]
    public ValueOutput LogicxTags;
    [DoNotSerialize] 
    public ValueOutput result;
   

    private string resultValue;
    
    List<LogixTagHandler> tags = new List<LogixTagHandler>();


    protected override void Definition() 
    {
        LogixTagHandler MyTagH = new LogixTagHandler("From_Conveyor");

        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {

            string Text = "Adding New Tags";

                
                    foreach (string s in flow.GetValue<List<string>>(PLCtag))
                    {
                       LogixTagHandler MyTagH = new LogixTagHandler(s) ;
                        //MyTagH.Name =s;
                        MyTagH.InitState();
                        MyTagH.GetReadedValueText();
                        MyTagH.ReadValue.Report.Init();
                        MyTagH.Radix = TagValueRadix.Decimal;
                        if (!tags.Contains(MyTagH))
                        {
                            tags.Add(MyTagH);
                            Text = Text + "<<****>>" + s + "=Added=";
                        }
                            
                    }
            resultValue = Text;
            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");

      
       
        PLCtag = ValueInput<List<string>>("PLCTag", new List<string>());
        LogicxTags = ValueOutput<List<LogixTagHandler>>("CLXTags", (flow) => { return tags; });
        result = ValueOutput<string>("result", (flow) => { return resultValue; });
        
    }
   
}

