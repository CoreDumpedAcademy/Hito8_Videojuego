using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOptions : MonoBehaviour
{
    GameObject objController;
    GameController gameController;

    public GameObject warningPanel;
    public TextMeshProUGUI currProdSpeed;
    public TextMeshProUGUI nextProdSpeed;

    void Start()
    {
        objController = GameObject.Find("GameController");
        gameController = objController.GetComponent<GameController>(); 
    }

    void Update()
    {
        int currSpeed = (int)(gameController.resetFactor * 100);
        int nextSpeed = (int)((gameController.resetFactor + gameController.resetIncreaseFactor) * 100);

        currProdSpeed.text = currSpeed.ToString() + "%";
        nextProdSpeed.text = nextSpeed.ToString() + "%";
    }

    public void panelActive(bool active)
    {
        warningPanel.SetActive(active);
    }
}
