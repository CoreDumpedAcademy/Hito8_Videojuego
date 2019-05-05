using System.Collections;
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
    GameController controller;

    // Start is called before the first frame update
    void Start()
    {
        dev = gameObject.GetComponent<Dev>();
        controllerObj = GameObject.Find("GameController");
        controller = controllerObj.GetComponent<GameController>();
        optionsMenu.SetActive(false);
        sellMenu.SetActive(false);
        devCost.text = "Sell: " + dev.typeData.cost / 2;
        devName.text = dev.typeData.name;
    }

    public void OpenOptions()
    {
        Debug.Log("marmota");
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }
    public void PressSell()
    {
        if (controller.countDevs > 1) {
            sellMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }

    public void returnToPanel()
    {
        sellMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void SellDev()
    {
        sellMenu.SetActive(false);
        Destroy(gameObject);
        controller.coins += (dev.typeData.cost / 2);
        controller.countDevs--;
    }

}
