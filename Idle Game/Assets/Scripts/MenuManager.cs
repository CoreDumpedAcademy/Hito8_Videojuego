using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private bool status;
    public GameObject Menu;
    public GameController controller;
    public TextMeshProUGUI coinsText;

    // Start is called before the first frame update
    void Start()
    {
        status = false;
        Menu.SetActive(false);
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "Coins: " + controller.coins;
    }

    public void OpenShop()
    {
        if(!status)
        {
            Menu.SetActive(true);
            status = true;
        }
        else
        {
            Menu.SetActive(false);
            status = false;
        }
    }
}
