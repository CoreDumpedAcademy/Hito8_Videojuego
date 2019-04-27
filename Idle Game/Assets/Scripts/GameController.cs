﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    DevController devFunctions;          //reference to script containing dev functions. GameController acts as interface.

    public Text Coins;
    public Slider GameProgress;

    public long coins;
    public long reward = 50;             //Coin reward for completing game

    public float progress;
    private float max = 100;

    // Start is called before the first frame update
    void Start()
    {
        devFunctions = gameObject.GetComponent<DevController>();
        coins = 0;
        progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Coins.text = "Coins: " + coins;
        //progress++;
        actualizarSlider(max, progress);
        spawnDev("Dev");
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
    }

    public bool spawnDev(string dev)              //return bool indicating if the operation was succesful 
    {
        return devFunctions.spawnDev(dev);
    }
}
