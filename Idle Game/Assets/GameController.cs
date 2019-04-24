using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text Coins;
    public Slider GameProgress;

    public long coins;
    public long reward = 50;             //Coin reward for completing game

    public float progress;
    private float max = 100;

    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Coins.text = "Coins: " + coins;
        progress++;
        actualizarSlider(max, progress);
    }

    private void actualizarSlider(float mx, float prog)
    {
        float porcentaje;
        porcentaje = prog / mx;
        if (porcentaje >= 1)
        {
            do
            {
                completeGame();
                progress = 0;              //Variable stored, not the parameter
                porcentaje -= 1;
            } while (porcentaje >= 1);
        }
        GameProgress.value = porcentaje;
    }

    //Cualquier cosa que hagamos al completar un juego
    private void completeGame()
    {
        coins += reward;
    }
}
