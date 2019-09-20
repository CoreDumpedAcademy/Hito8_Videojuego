using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DevOptions : MonoBehaviour
{ 
    public GameObject optionsMenu;
    public GameObject sellMenu;

    SPlayScene.SDevInfo devText;

    public TextMeshProUGUI devName;
    public TextMeshProUGUI sellText;
    public TextMeshProUGUI sellWarning;
    public TextMeshProUGUI sellYes;
    public TextMeshProUGUI sellNo;
    public TextMeshProUGUI devCost;
    public TextMeshProUGUI workText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI exhaustedText;

    public Dev dev;
    GameObject controllerObj;
    GameController gameController;
    DevController devController;
    /*
    public Button btn1;
    public Dev.devActivity btn1Option;
    public Text btn1Text;
    public Button btn2;
    public Dev.devActivity btn2Option;
    public Text btn2Text;
    */

    Dev.devActivity[] defaultActivityOptions = new Dev.devActivity[2] {
        Dev.devActivity.training,
        Dev.devActivity.resting
    };

    public activityOptionBtn btn1;
    public activityOptionBtn btn2;

    Dictionary<Dev.devActivity, string> activityOptions = new Dictionary<Dev.devActivity, string>
    {
        { Dev.devActivity.training, "" },
        { Dev.devActivity.resting, "" },
        { Dev.devActivity.working, "" }
    };

    // Start is called before the first frame update
    void Start()
    {
        dev = gameObject.GetComponent<Dev>();
        controllerObj = GameObject.Find("GameController");
        gameController = controllerObj.GetComponent<GameController>();
        devController = controllerObj.GetComponent<DevController>();

        devText = GameText.text.PlayScene.DevInfo;

        sellText.text = devText.SellDev;
        sellWarning.text = devText.SellWarning;
        sellYes.text = GameText.text.Yes;
        sellNo.text = GameText.text.No;
        energyText.text = devText.Energy;        
        exhaustedText.text = devText.Exhausted;
        exhaustedText.faceColor = new Color32(255, 0, 156, 255);

        activityOptions[Dev.devActivity.training] = devText.Activities.Training;
        activityOptions[Dev.devActivity.resting] = devText.Activities.Resting;
        activityOptions[Dev.devActivity.working] = devText.Activities.Working;
     
        devCost.text = dev.typeData.cost / 2 + GameText.text.PlayScene.CurrencySymbol;
        devName.text = dev.typeData.devName;
        workText.text = activityOptions[dev.currActivity];
        workText.faceColor = new Color32(0, 52, 183, 255);

        optionsMenu.SetActive(false);
        sellMenu.SetActive(false);

        if (dev.currActivity == defaultActivityOptions[0])
        {
            btn1.option = Dev.devActivity.working;
            btn2.option = defaultActivityOptions[1];
        } else if (dev.currActivity == defaultActivityOptions[1])
        {
            btn1.option = defaultActivityOptions[0];
            btn2.option = Dev.devActivity.working;
        }

        btn1.text.text = activityOptions[btn1.option];
        btn2.text.text = activityOptions[btn2.option];
    }

    private void Update()
    {
        exhaustedText.gameObject.SetActive(dev.energy == 0);
    }

    public void OpenOptions()
    {
        gameController.audio.playSFX(gameController.audio.clipNames.btnClick);
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

    public void btn1Click()
    {
        dev.changeActivity(btn1.option);

        if (btn1.option == defaultActivityOptions[0])
        {
            btn1.option = Dev.devActivity.working;
            btn2.option = defaultActivityOptions[1];
        } else
        {
            btn1.option = defaultActivityOptions[0];
        }
        workText.text = activityOptions[dev.currActivity];
        btn1.text.text = activityOptions[btn1.option];
        btn2.text.text = activityOptions[btn2.option];
    }

    public void btn2Click()
    {
        dev.changeActivity(btn2.option);

        if (btn2.option == defaultActivityOptions[1])
        {
            btn1.option = defaultActivityOptions[0];
            btn2.option = Dev.devActivity.working;
        }
        else
        {
            btn2.option = defaultActivityOptions[1];
        }

        workText.text = activityOptions[dev.currActivity];
        btn1.text.text = activityOptions[btn1.option];
        btn2.text.text = activityOptions[btn2.option];
    }

    [System.Serializable]
    public struct activityOptionBtn
    {
        [SerializeField]
        public Button btn;
        [SerializeField]
        public TextMeshProUGUI text;
        [SerializeField]
        public Dev.devActivity option;
    }
}
