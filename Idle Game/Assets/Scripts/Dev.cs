using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dev : MonoBehaviour
{
    //All other dev prefabs will inherit from this script

    GameObject controllerObj;
    GameController controller;

    GameObject origin;

    Sprite sprite;
    Text lvlText;
    public Slider expBar;
    Text expBarText;

    //Variables that differentiate behaivour of different Devs
    float baseProd = 1;            //amount of progress it produces initially
    float baseProdFreq = 3f;       //times it generates production by second
    int baseExpGain = 1;           //Starting exp gain each time it generates progress
    float maxExp = 30;             //exp Necessary to level up

    public float prodFreq;
    float prodPeriod;                     //time in seconds between generating production (inverse of baseProdFreq)
    public float prod;                    //amount it currently produces 
    public float exp = 0;
    public int expGain;                   //exp it's gaining right now
    public int lvl = 1;                   //current level
    int maxLvl = 50;
    float counter = 0;                    //counts time to know when to produce
    private float increaseExp = 1.15f;
    private float textExp;
    private float increaseProd = 1.1f;


    void Start()
    {
        controllerObj = GameObject.Find("GameController");
        controller = controllerObj.GetComponent<GameController>();

        sprite = transform.Find("Sprite").GetComponent<Sprite>();
        lvlText = transform.Find("Nivel").GetComponent<Text>();
        expBarText = expBar.transform.Find("Valor").GetComponent<Text>();

        lvlText.text = "Lvl: " + lvl;
        expBar.value = (float) exp / maxExp;

        prod = baseProd;
        prodPeriod = 1 / baseProdFreq;
        expGain = baseExpGain;
    }

    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= prodPeriod)
        {
            controller.addProgress(prod);
            if (lvl < maxLvl) { gainExp(expGain); }
            counter -= prodPeriod;
        }
    }

    public void gainExp(int gain)
    {
        exp += gain;

        while (exp >= maxExp && lvl < maxLvl)
        {
            exp -= maxExp;
            levelUp();           //increase level and scale values
        }

        if(lvl < maxLvl)
        {
            expBar.value = (float)exp / maxExp;
        }
        else
        {
            expBar.value = 1f;
        }
        textExp = expBar.value * 100;
        expBarText.text = textExp.ToString("f2") + "%"; 
    }

    //mainly scales values related to production
    void levelUp()
    {
        lvl++;
        if (lvl < maxLvl)
        {
            lvlText.text = "Lvl: " + lvl;
        }
        else
        {
            lvlText.text = "Lvl: MX";
        }
        
        scaleExp();
        scaleProduction();
    }

    void scaleExp()
    {
        maxExp *= increaseExp;
    }

    void scaleProduction()
    {
        prod *= increaseProd;
    }
}
