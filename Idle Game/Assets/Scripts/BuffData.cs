using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuff", menuName = "Buffer")]

public class BuffData : ScriptableObject
{
    public RuntimeAnimatorController artwork;

    public string buffName;
    public bufftype type;
    public float addValue;

    public enum bufftype
    {
        none,
        initCoinIncr,
        prodIncr,
        rewIncr,
        expIncr
    }
}
