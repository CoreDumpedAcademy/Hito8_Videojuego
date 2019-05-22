using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    string resetBuffsPath = "BuffTypes";
    BuffData[] resetBuffs;
    int resetBuffNumer;

    void Start()
    {
        resetBuffs = Resources.LoadAll<BuffData>(resetBuffsPath);
        resetBuffNumer = resetBuffs.Length;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) )
        {
            Debug.Log( getRamdomResetBuff().name );
        }
    }
    public BuffData getRamdomResetBuff()
    {
        BuffData buff = resetBuffs[0];    //Initiated to default Buff
        int buffInd = Random.Range(1, resetBuffNumer);
        buff = resetBuffs[buffInd];
        return buff;
    }


    public struct buffValues
    {
        int rewardCoinIncr;
        int initCoinIncr;
        float prodIncr;
        float expIncr;
    }
}
