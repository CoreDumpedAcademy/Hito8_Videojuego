using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffPanel : MonoBehaviour
{
    public BuffData data;

    public GameObject panel;

    public TextMeshProUGUI buffName;
    public TextMeshProUGUI buffDescription;
    public Animator sprite;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewBuff()
    {
        panel.SetActive(true);
        buffName.text = data.buffName;
        sprite.runtimeAnimatorController = data.artwork;
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
                buffDescription.text = "Increase of initial rew??: " + data.addValue;
                break;

            default :
                buffDescription.text = "Increase of experience: " + data.addValue;
                break;
        }
    }
}
