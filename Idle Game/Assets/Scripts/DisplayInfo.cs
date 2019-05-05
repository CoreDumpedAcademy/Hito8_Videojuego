using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayInfo : MonoBehaviour
{
    public DevData devDataShow;

    public TextMeshProUGUI nameDev;
    public TextMeshProUGUI cost;
    public Image sprite;

    // Start is called before the first frame update
    void Start()
    {
        cost.text = devDataShow.cost.ToString() + " C";
        nameDev.text = devDataShow.devName;
        sprite.sprite = devDataShow.artwork;
    }

    public void LoadDev(DevData devData)
    {
        cost.text = devData.cost.ToString() + " C";
        nameDev.text = devData.devName;
        sprite.sprite = devData.artwork;
        devDataShow = devData;
    }
    
    public DevData GiveDev()
    {
        return devDataShow;
    }
}