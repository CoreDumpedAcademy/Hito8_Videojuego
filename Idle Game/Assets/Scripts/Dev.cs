using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dev : MonoBehaviour
{

    //References to other game elements
    GameObject controllerObj;
    GameController controller;

    GameObject origin;

    public DevData defaultTypeData;

    Animator sprite;

    Text lvlText;
    public Slider expBar;
    public Slider energyBar;
    Text expBarText;

    //Variables that differentiate Devs types
    float baseProd = 1;            //amount of progress it produces initially
    float baseProdFreq = 3f;       //times it generates production by second
    int baseExpGain = 1;           //Starting exp gain each time it generates progress
    float maxExp = 30;             //exp Necessary to level up

    //Variables that define this dev's state
    public float exp = 0;
    public int lvl = 1;                                        //current level
    public DevData typeData;                                   //DevData object that defines the dev type
    public devActivity currActivity = devActivity.training;
    public float energy;

    //Variables defined in game
    public float prodFreq;
    public float prod;                           //amount it currently produces 
    public float prodPeriod;                     //time in seconds between generating production (inverse of baseProdFreq)
    float energyFactor;                       //Inrease factor of production based on energy;
    float energyGain;
    float energyLoss;                                       // depend's on activity
    public int expGain;                                    //exp it's gaining right now
    float textExp;
    string lvlString;

    //Dev "constants"
    int maxLvl = 50;
    float restPeriod = 0.5f;                               //seconds between resting steps
    float trainPeriod = 0.5f;
    public float maxEnergy = 100;
    TimeSpan maxRestTime = new TimeSpan(0, 01, 00);        //Time it takes to go from 0 to full energy
    TimeSpan maxWorkTime = new TimeSpan(0, 00, 30);        //Time it takes to go from full to 0 energy while working
    TimeSpan maxTrainTime = new TimeSpan(0, 00, 10);       //Same, but while training
    double localCounter = 0;                               //counts time to know when to produce
    float increaseExp = 1.15f;
    float increaseProd = 1.3f;
    float trainExpFactor = 5;                              //Factor by which exp gain increases while training
    float[,] energyFactorTable = new float[,] {              //Relates a fraction of energy to a factor to multiply 
        { 0.98f, 3 },                                      //the production
        { 0.25f, 1 },
        { 0.00f, 0.1f }
    };
        
    //In game state
    public bool active = false;
    public int producedInSession = 0;

    void Start()
    {
        if (!active)                                  //If Dev is not set up, set it up
        {
            if (typeData == null)                      //If no type is assigned, give it the default
            {
                typeSetUp(defaultTypeData);
            }
            baseSetUp();

            active = true;
        } 
    }

    void baseSetUp()
    {
        controllerObj = GameObject.Find("GameController");
        controller = controllerObj.GetComponent<GameController>();

        sprite = transform.Find("Sprite").GetComponent<Animator>();
        lvlText = transform.Find("Nivel").GetComponent<Text>();
        expBarText = expBar.transform.Find("Valor").GetComponent<Text>();

        lvlString = "Lvl: " + lvl;
        expBar.value = (float)exp / maxExp;
        expGain = baseExpGain;

        energy = 100;

        energyGain = setEnergyGain();
        energyLoss = setEnergyLoss();
        sprite.runtimeAnimatorController = typeData.artwork;
    }

    float setEnergyGain()
    {
        int totalSteps = (int)(maxRestTime.TotalSeconds / restPeriod);
        float gain = maxEnergy / totalSteps;
        return gain;
    }

    float setEnergyLoss()
    {
        int totalSteps = 1;   
        float loss = 0;
        if (currActivity == devActivity.working)
        {
            totalSteps = (int)(maxWorkTime.TotalSeconds / prodPeriod);
            loss = maxEnergy / totalSteps;
        } else if(currActivity == devActivity.training)
        {
            totalSteps = (int)(maxTrainTime.TotalSeconds / trainPeriod);
            loss = maxEnergy / totalSteps;
        }

        return loss;
    }

    void typeSetUp(DevData type)
    {
        if (type != null)
        {
            typeData = type;
        }
        else
        {
            typeData = defaultTypeData;
        }
        prod = typeData.production;
        prodFreq = typeData.frequency;
        prodPeriod = 1 / prodFreq;
        maxExp = typeData.maxExp;
    }

    //Used to set up dev before time
    public void startUp(DevData type)
    {
        if (!active)
        {
            typeSetUp(type);
            baseSetUp();
            active = true;
        }
    }

    void Update()
    {
        lvlText.text = lvlString;
        energyBar.value = energy / maxEnergy;
    }

    public int elapsedTime(double timeCounter)             //Returns the amount of times produced in the elapsed time
    {
        int count = 0;
        if(active)
        {
            localCounter += timeCounter;
            count = inGameSteps();
            energyFactor =  updateEnergyFactor();
        }
        return count;
    }

    int inGameSteps()
    {
        int count = 0;
        switch (currActivity)
        {
            case devActivity.working:
                count = workingSteps();
                break;
            case devActivity.resting:
                count = restingSteps();
                break;
            case devActivity.training:
                count = trainingSteps();
                break;
        }
        return count;
    }

    int workingSteps()
    {
        int count = 0;
        while (localCounter >= prodPeriod && energy > 0)
        {
            controller.addProgress(prod * energyFactor);
            energy -= energyLoss ;
            if (energy < 0) energy = 0;
            if (lvl < maxLvl) { gainExp(expGain * (int)energyFactor); }
            localCounter -= prodPeriod;
            producedInSession++;
            count++;
        }
        return count;
    }

    int restingSteps()
    {
        int count = 0;

        while (localCounter >= restPeriod && energy <= maxEnergy)
        {
            energy += energyGain;
            if (energy > maxEnergy) energy = maxEnergy;
            localCounter -= restPeriod;
            count++;
        }

        return count;
    }

    int trainingSteps()
    {
        int count = 0;

        while (localCounter > trainPeriod && energy > 0)
        {
            energy -= energyLoss;
            if (energy < 0) energy = 0;
            if (lvl < maxLvl) { gainExp(expGain * (int)trainExpFactor); }
            localCounter -= trainPeriod;
            count++;
        }

        return count;
    }

    public void changeActivity(devActivity activity)
    {
        currActivity = activity;
        energyLoss = setEnergyLoss();
    }

    float updateEnergyFactor()
    {
        float factor = 1;
        if (energy <= 0)
        {
            factor = 0;
        }
        else
        {
            float percent = energy / maxEnergy;
            int i = energyFactorTable.GetLength(0) - 1;
            while (i >= 0 && percent >= energyFactorTable[i, 0])
            {
                factor = energyFactorTable[i, 1];
                i--;
            }
        }
        return factor;
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
        controller.audio.playSFX(controller.audio.clipNames.devLvlUp);

        if (lvl < maxLvl)
        {
            lvlString = "Lvl: " + lvl;
        }
        else
        {
            lvlString = "Lvl: MX";
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

    //get dev state
    public DevState getState()
    {
        DevState state = new DevState();
        state.type = typeData.name;
        state.exp = exp;
        state.lvl = lvl;
        return state;
    }

    //set dev state
    public void setState(DevState state)   //set ups a dev using all the state from a dev
    {
        // <-- set type here
        //startUp(typeData);

        for(int i = 1; i < state.lvl; i++)
        {
            levelUp();
        }
        exp = state.exp;
    }

    public int correctProduction(int count, double seconds)            //Return how far production deviates from expected productions
    {
        int expectedCount = (int)Math.Floor( seconds * prodFreq);
        return count - expectedCount;
    }

    private void OnDestroy()
    {
        active = false;
    }

    public enum devActivity
    {
        working,
        resting,
        training
    }
}
