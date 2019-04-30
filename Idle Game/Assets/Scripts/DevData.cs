using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dev", menuName = "Developer")]

public class DevData : ScriptableObject
{
    public Sprite artwork;

    public string devName;
    public int cost;
    public float production;
    public int frequency;
    public int maxExp;
}
