using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DevState
{
    public string type;
    public int lvl;
    public float exp;
    public float energy;
    public Dev.devActivity activity;

    public DevState()
    {
        lvl = 1;
        exp = 0;
    }

    override public string ToString()
    {
        return "Type: " + type + " lvl: " + lvl + " exp: " + exp + " energy: "+ energy + " activity: " + activity.ToString();
    }
}
