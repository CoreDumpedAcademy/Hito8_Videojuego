using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text Coins;
    public Slider GameProgress;

    public long coins;
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
        actualizarSlider(max, progress);
    }

    private void actualizarSlider(float max, float progress)
    {
        float porcentaje;
        porcentaje = progress / max;
        GameProgress.value = porcentaje;
    }
}
