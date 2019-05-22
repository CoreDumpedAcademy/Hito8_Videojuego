using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    string resetBuffsPath = "BuffTypes";
    BuffData[] resetBuffs;
    int resetBuffNumer;

    public buffValues buffs;
    public List<BuffData> activeBuffs = new List<BuffData>();

    void Awake()
    {
        resetBuffs = Resources.LoadAll<BuffData>(resetBuffsPath);
        resetBuffNumer = resetBuffs.Length;
        
        //Default buff values
        buffs.initCoinIncr = 0;
        buffs.rewardCoinIncr = 0;
        buffs.prodIncr = 1;
        buffs.expIncr = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            applyBuff( getRandomResetBuff() );
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(buffs.ToString());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            writeActiveBuffs();
        }
    }

    public void writeActiveBuffs()
    {
        Debug.Log("Active buffs:");
        foreach( BuffData buff in activeBuffs)
        {
            Debug.Log(" " + buff.name);
        }
    }
    public BuffData getRandomResetBuff()
    {
        BuffData buff = resetBuffs[0];    //Initiated to default Buff
        int buffInd = Random.Range(1, resetBuffNumer);
        buff = resetBuffs[buffInd];
        return buff;
    }

    public void applyBuff( BuffData buff )
    {
        Debug.Log("Buff applied: " + buff.name);

        activeBuffs.Add(buff);

        switch (buff.type){
            case BuffData.bufftype.initCoinIncr:
                buffs.initCoinIncr += (int)buff.addValue;
                break;

            case BuffData.bufftype.rewIncr:
                buffs.rewardCoinIncr += (int)buff.addValue;
                break;

            case BuffData.bufftype.prodIncr:
                buffs.prodIncr += buff.addValue;
                break;

            case BuffData.bufftype.expIncr:
                buffs.expIncr += buff.addValue;
                break;

            default:
                Debug.Log("No buff type");
                break;
        }
    }

    public struct buffValues
    {
        public int rewardCoinIncr;
        public int initCoinIncr;
        public float prodIncr;
        public float expIncr;

        public override string ToString()
        {
            string str = "";

            str = str + "Extra reward coins: " + rewardCoinIncr;
            str = str + " Extra init coins: " + initCoinIncr;
            str = str + " Extra production factor: " + prodIncr;
            str = str + " Extra experience factor: " + expIncr;

            return str;
        }
    }
}
