using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    DevController devFunctions;          //reference to script containing dev functions. GameController acts as interface.

    public Text Coins;
    public Slider GameProgress;

    public long coins;
    public long cost = 150;
    public long initialReward = 50;             //Coin reward for completing game
    public long reward;
    private long rewardIncrease = 25;

    public float progress;                     //game progress points     
    private float max;                         //progress points to complete a game  
    private float initialMax = 10;                
    private float maxScaleFactor = 0.2f;

    public int gameCounter;                    //How many games have been completed
    // Start is called before the first frame update
    void Start()
    {
        devFunctions = gameObject.GetComponent<DevController>();

        max = initialMax;
        reward = initialReward;
        coins = 150;
        progress = 0;
        gameCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Coins.text = "Coins: " + coins;
        //progress++;
        actualizarSlider(max, progress);
        //spawnDev("Dev");
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

    //Cualquier cosa que hagamos al completar un juego
    private void completeGame()
    {
        coins += reward;
        gameCounter++;
        scaleMaxProd();
        scaleReward();
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

    public bool spawnDev(string dev)              //return bool indicating if the operation was succesful 
    {
        return devFunctions.spawnDev(dev);
    }

    public void BuyDev()
    {
        if (coins >= cost)
        {
            Debug.Log("Bieen tienes dinero!");
            if (spawnDev("Dev"))
            {
                coins -= cost;
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
}
