using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DevData
{
    public string type;
    public int lvl;
    public float exp;

    public DevData()
    {
        lvl = 1;
        exp = 0;
    }

    override public string ToString()
    {
        return "Type: " + type + " lvl: " + lvl + " exp: " + exp;
    }
}
