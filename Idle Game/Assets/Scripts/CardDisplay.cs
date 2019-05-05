using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public DevData devData;

    public TextMeshProUGUI cost;
    public Image sprite;

    // Start is called before the first frame update
    void Start()
    {
        cost.text = devData.cost.ToString() + " C";
        sprite.sprite = devData.artwork;
    }
}
