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

    DevData typeData;
    public DevData defaultTypeData;

    Image sprite;

    Text lvlText;
    public Slider expBar;
    Text expBarText;

    //Variables that differentiate Devs types
    float baseProd = 1;            //amount of progress it produces initially
    float baseProdFreq = 3f;       //times it generates production by second
    int baseExpGain = 1;           //Starting exp gain each time it generates progress
    float maxExp = 30;             //exp Necessary to level up

    //Variables that define this dev's state
    public float exp = 0;
    public int lvl = 1;                   //current level
    public string type = "Dev";           //hard coded for now     

    //Variables defined in game
    public float prodFreq;
    public float prod;                    //amount it currently produces 
    float prodPeriod;                     //time in seconds between generating production (inverse of baseProdFreq)

    //Dev "constants"
    public int expGain;                   //exp it's gaining right now
    int maxLvl = 50;
    float counter = 0;                    //counts time to know when to produce
    private float increaseExp = 1.15f;
    private float textExp;
    private string lvlString;
    private float increaseProd = 1.3f;

    void Start()
    {
        controllerObj = GameObject.Find("GameController");
        controller = controllerObj.GetComponent<GameController>();

        sprite = transform.Find("Sprite").GetComponent<Image>();
        lvlText = transform.Find("Nivel").GetComponent<Text>();
        expBarText = expBar.transform.Find("Valor").GetComponent<Text>();

        lvlString = "Lvl: " + lvl;
        expBar.value = (float) exp / maxExp;
        expGain = baseExpGain;

        sprite.sprite = typeData.artwork;
    }

    public void startUp(DevData type)
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

    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= prodPeriod)
        {
            controller.addProgress(prod);
            if (lvl < maxLvl) { gainExp(expGain); }
            counter -= prodPeriod;
        }

        lvlText.text = lvlString;
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
        state.type = typeData.devName;
        state.exp = exp;
        state.lvl = lvl;
        return state;
    }

    //set dev state
    public void setState(DevState state)   //set ups a dev using all the state from a dev
    {
        // <-- set type here
        startUp(typeData);

        for(int i = 1; i < state.lvl; i++)
        {
            levelUp();
        }
        exp = state.exp;
    }
}
