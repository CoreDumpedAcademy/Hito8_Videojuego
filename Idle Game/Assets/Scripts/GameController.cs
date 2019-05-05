using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    DevController devFunctions;          //reference to script containing dev functions. GameController acts as interface.
    public DisplayInfo displayInfo;

    public Text Coins;
    public Slider GameProgress;
    
    //Variables in save data
    public long coins;
    public float progress;                     //game progress points    
    public int gameCounter;                    //How many games have been completed

    //Calculated in game
    public long reward;
    private float max;                         //progress points to complete a game  

    //in game "constants"
    public long initialReward = 50;             //Coin reward for completing game
    private long rewardIncrease = 10;
    private float initialMax = 10;                
    private float maxScaleFactor = 0.5f;
    int rewardThreshold = 5;

    public long cost = 150;

    void Start()
    {
        devFunctions = gameObject.GetComponent<DevController>();

        max = initialMax;
        reward = initialReward;
        coins = 150;
        progress = 0;
        gameCounter = 0;
    }
    void Update()
    {
        Coins.text = "Coins: " + coins;
        //progress++;
        actualizarSlider(max, progress);
        //spawnDev("Dev");

        if (Input.GetButtonDown("Submit")) { saveGame(); }
        else if (Input.GetButtonDown("Cancel")) { loadGame(); }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) { devFunctions.writeDevs(); }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) { devFunctions.clearDevs(); }
    }

    public void addProgress(float prog)
    {
        progress += prog;
    }

    private void actualizarSlider(float mx, float prog)
    {
        float porcentaje = prog / mx;
        while (porcentaje >= 1)
        {
            porcentaje -= 1;
            progress = porcentaje;              //Variable stored, not the parameter
            completeGame();
        }
        GameProgress.value = porcentaje;
    }

    //Anything done when completing a game
    private void completeGame()
    {
        coins += reward;
        gameCounter++;
        scaleMaxProd();
        if(gameCounter % rewardThreshold == 0)
        {
            scaleReward();
        }      
    }

    private void scaleMaxProd()
    {
        if(max <= 1)
        {
            max = initialMax;
        }
        else
        {
            max += max * maxScaleFactor;
        }
    }

    private void scaleReward()
    {
       reward += rewardIncrease;
    }


    public bool spawnDev(DevData dev)              //return bool indicating if the operation was succesful 
    {
        return devFunctions.spawnDev(dev) != null;
    }

    public void BuyDev()
    {
        DevData dev = displayInfo.GiveDev();
        if (dev.devName != "Empty")
        {
            if (coins >= dev.cost)
            {
                Debug.Log("Bieen tienes dinero!");
                if (spawnDev(dev))
                {
                    coins -= dev.cost;
                    Debug.Log("Amazon le enviara su pedido en brevas.");
                }
                else
                {
                    Debug.Log("Amazon se niega a darte lo que le has pedido.");
                }
            }
            else
            {
                Debug.Log("pobreton!!");
            }
        }
        else
        {
            Debug.Log("No compras nada.");
        }
    }

    //Saving game stuff
    public void saveGame()
    {
        SaveData save = generateSaveData();
        Debug.Log("Save data created");
        save.saveInLocal();
    }

    public SaveData generateSaveData()
    {
        SaveData save = new SaveData();
        save.coins = coins;
        save.progress = progress;
        save.gameCounter = gameCounter;
        save.devStateArray = devFunctions.getDevState();
        //get dev data

        return save;
    }

    public void loadGame()
    {
        //get data
        SaveData save = new SaveData();
        save = save.getFromLocal();       //test data is saved in local
        Debug.Log("Save data retrived");

        //reset variables
        max = initialMax;
        reward = initialReward;
        
        //set saved values
        coins = save.coins;
        progress = save.progress;
        gameCounter = save.gameCounter;

        devFunctions.clearDevs();
        devFunctions.recreateDevs(save.devStateArray);

        //Simulate in-game processes
        simulateInGameProgress();
    }

    void simulateInGameProgress()
    {
        for (int i = 0; i < gameCounter; i++)
        {
            scaleMaxProd();
            if (i % rewardThreshold == 0 && i != 0)
            {
                scaleReward();
            }
        }
    }
}
