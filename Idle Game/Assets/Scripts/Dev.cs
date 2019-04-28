﻿using System.Collections;
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
    public float baseProd = 2;            //amount of progress it produces initially
    public float baseProdFreq = 1f;       //times it generates production by second
    public int baseExpGain = 1;           //Starting exp gain each time it generates progress
    public float maxExp = 10;               //exp Necessary to level up


    public float prodFreq;
    float prodPeriod;                     //time in seconds between generating production (inverse of baseProdFreq)
    public float prod;                    //amount it currently produces 
    public float exp = 0;
    public int expGain;                   //exp it's gaining right now
    public int lvl = 1;                   //current level
float counter = 0;                    //counts time to know when to produce
    private float increaseExp = 1.1f;
    private float textExp;

    void Start()
    {
        controllerObj = GameObject.Find("GameController");
        controller = controllerObj.GetComponent<GameController>();

        //origin = transform.Find("Origin").gameObject;

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
        if(counter >= prodPeriod)
        {
            controller.addProgress(prod);
            gainExp(expGain);
            counter -= prodPeriod;
        }
    }

    public void gainExp(int gain)
    {
        exp += gain;

        while (exp >= maxExp)
        {
            exp -= maxExp;
            levelUp();           //increase level and scale values
        }

        expBar.value = (float)exp / maxExp;
        textExp = expBar.value * 100;
        expBarText.text = textExp.ToString("f2") + "%"; 
    }

    //mainly scales values related to production
    void levelUp()
    {
        lvl++;
        lvlText.text = "Lvl: " + lvl;
        maxExp *= increaseExp;
    }
}
