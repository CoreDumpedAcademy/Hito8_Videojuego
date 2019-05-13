﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevOptions : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject sellMenu;
    public Text devName;
    public Text devCost;
    public Dev dev;
    GameObject controllerObj;
    GameController gameController;
    DevController devController;

    // Start is called before the first frame update
    void Start()
    {
        dev = gameObject.GetComponent<Dev>();
        controllerObj = GameObject.Find("GameController");
        gameController = controllerObj.GetComponent<GameController>();
        devController = controllerObj.GetComponent<DevController>();
        optionsMenu.SetActive(false);
        sellMenu.SetActive(false);
        devCost.text = "Sell: " + dev.typeData.cost / 2;
        devName.text = dev.typeData.devName;
    }

    public void OpenOptions()
    {
        gameController.audio.playSFX(gameController.audio.clipNames.btnClick);
        Debug.Log("marmota");
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        gameController.audio.playSFX(gameController.audio.clipNames.btnClick);
        optionsMenu.SetActive(false);
    }
    public void PressSell()
    {
        gameController.audio.playSFX(gameController.audio.clipNames.btnClick);
        if (devController.devArray.Count > 1) {
            sellMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }

    public void returnToPanel()
    {
        gameController.audio.playSFX(gameController.audio.clipNames.btnClick);
        sellMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void SellDev()
    {
        sellMenu.SetActive(false);
        gameController.coins += (dev.typeData.cost / 2);
        gameController.audio.playSFX( gameController.audio.clipNames.cashRegister );
        Destroy(gameObject);        
    }

}
