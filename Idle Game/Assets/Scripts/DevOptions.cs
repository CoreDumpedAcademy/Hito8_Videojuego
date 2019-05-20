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
        { Dev.devActivity.training, "Training" },
        { Dev.devActivity.resting, "Resting" },
        { Dev.devActivity.working,"Working" }
    };

    // Start is called before the first frame update
    void Start()
    {
        dev = gameObject.GetComponent<Dev>();
        controllerObj = GameObject.Find("GameController");
        gameController = controllerObj.GetComponent<GameController>();
        devController = controllerObj.GetComponent<DevController>();
        optionsMenu.SetActive(false);
        sellMenu.SetActive(false);
        devCost.text = "Sell: " + dev.typeData.cost / 2;
        devName.text = dev.typeData.devName;

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

    public void OpenOptions()
    {
        gameController.audio.playSFX(gameController.audio.clipNames.btnClick);
        Debug.Log("marmota");
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

        btn1.text.text = activityOptions[btn1.option];
        btn2.text.text = activityOptions[btn2.option];
    }

    [System.Serializable]
    public struct activityOptionBtn
    {
        [SerializeField]
        public Button btn;
        [SerializeField]
        public Text text;
        [SerializeField]
        public Dev.devActivity option;
    }
}
