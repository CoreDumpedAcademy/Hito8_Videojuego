using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffPanel : MonoBehaviour
{
    public BuffData data;

    public GameObject panel;

    public TextMeshProUGUI buffName;
    public TextMeshProUGUI buffDescription;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame

    public void NewBuff(BuffData buff)
    {
        data = buff;
        panel.SetActive(true);
        buffName.text = data.buffName;
        image.sprite = data.artwork;
        Description(data.type);
    }

    public void GoGame()
    {
        panel.SetActive(false);
    }

    private void Description(BuffData.bufftype type)
    {
        
        switch (data.type)
        {
            case BuffData.bufftype.initCoinIncr:
                buffDescription.text = "Increase of initial coins: " + data.addValue;
                break;

            case BuffData.bufftype.prodIncr:
                buffDescription.text = "Increase of production: " + data.addValue;
                break;

            case BuffData.bufftype.rewIncr:
                buffDescription.text = "Increase of reward: " + data.addValue;
                break;

            case BuffData.bufftype.expIncr :
                buffDescription.text = "Increase of experience: " + data.addValue;
                break;

            default:
                buffDescription.text = "No buff applied";
                break;
        }
    }
}
