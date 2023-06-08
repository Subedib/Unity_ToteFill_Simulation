using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SerializerInput
{
    public string tagName;

    public SerializerInput(string tagName)
    {
        this.tagName = tagName;
    }
}

