using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    CentralTimer timer;
    DevController devFunctions;          //reference to script containing dev functions. GameController acts as interface.
    public AudioController audio;
    public DisplayInfo displayInfo;

    public Button resetButton;

    public Text Coins;
    public Slider GameProgress;
    
    //Variables in save data
    public long coins;
    public float progress;                     //game progress points    
    public int gameCounter;                    //How many games have been completed
    public int resets;

    //Calculated in game
    public long reward;
    private float max;                         //progress points to complete a game  
    int resetMinGames;
    public float resetFactor = 1;

    //in game "constants"
    public long initialReward = 50;            //Coin reward for completing game
    private long rewardIncrease = 10;
    private float initialMax = 10;                
    private float maxScaleFactor = 0.5f;
    int rewardThreshold = 5;
    int resetBaseMinGames = 5;
    int resetMinGamesIncrease = 5;
    public float resetIncreaseFactor = 0.5f;
    bool resetAvailable = false;

    public int sessionTimesProduced = 0;
    public long cost = 150;
    public bool loading = false;

    void Start()
    {
        audio = GetComponent<AudioController>();
        timer = GetComponent<CentralTimer>();
        devFunctions = gameObject.GetComponent<DevController>();

        resetMinGames = resetBaseMinGames;

        max = initialMax;
        reward = initialReward;
        coins = 150;
        progress = 0;
        gameCounter = 0; if (LoadState.LoadSituation)
        {
            loadGame();
        }
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
        progress += prog * resetFactor;
        sessionTimesProduced++;

        while (progress > max)
        {
            completeGame();
        }
    }

    private void actualizarSlider(float mx, float prog)
    {
        float porcentaje = prog / mx;
        GameProgress.value = porcentaje;
    }

    //Anything done when completing a game
    private void completeGame()
    {
        coins += reward;
        gameCounter++;
        audio.playSFX( audio.clipNames.completeGameDing );
        audio.playSFX( audio.clipNames.completeGameCoins );
        //string debugMessageInit = "Completed game nº: " + gameCounter + " reached progress: " + progress + "/" + max;

        if (gameCounter >= resetMinGames)
        {
            allowReset(true);
        }

        progress -= max;
        if (progress < 0) progress = 0;

        ScaleFactorAdjust();
        scaleMaxProd();
        if(gameCounter % rewardThreshold == 0)
        {
            scaleReward();
        }

        //string debugMessageFin = "New progress: " + progress + "/" + max + " Reward: " + reward;

        //Debug.Log(debugMessageInit + ";" +debugMessageFin);
    }

    void allowReset(bool available)
    {
        resetAvailable = available;
        resetButton.gameObject.SetActive(available);
    }

    private void ScaleFactorAdjust()
    {
        if (gameCounter % 8 == 0)
        {
            maxScaleFactor = maxScaleFactor / 1.2f;
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
                    audio.playSFX(audio.clipNames.cashRegister);
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

    public void resetProgress()
    {
        //Reset state
        max = initialMax;
        reward = initialReward;
        coins = 150;
        progress = 0;
        gameCounter = 0;

        devFunctions.clearDevs();

        resets++;
        resetFactor = updateResetFactor();
        resetMinGames = updateMinGames();

        allowReset(false);
    }

    float updateResetFactor()
    {
        float factor = 1;
        factor = 1 + resetIncreaseFactor * resets;
        return factor;
    }
    int updateMinGames()
    {
        int minGames = resetMinGames;
        minGames = resetBaseMinGames + resetMinGamesIncrease * resets;
        return minGames;
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
        save.lastLogOut = DateTime.Now;
        save.resets = resets;
        //get dev data

        return save;
    }

    public void loadGame()
    {
        loading = true;
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
        resets = save.resets;

        devFunctions.clearDevs();
        devFunctions.recreateDevs(save.devStateArray);

        //Simulate in-game processes
        simulateInGameProgress();
        timer.simulateOffLineProgress(save.lastLogOut);

        loading = false;
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
        resetFactor = updateResetFactor();
        resetMinGames = updateMinGames();
        resetAvailable = gameCounter >= resetMinGames;
    }
}
