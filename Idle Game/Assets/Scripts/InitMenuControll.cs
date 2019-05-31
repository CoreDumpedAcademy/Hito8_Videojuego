using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InitMenuControll : MonoBehaviour
{
    public GameObject loadButton;
    public TextMeshProUGUI playButton;

    private void Start()
    {
        //Screen.fullScreen = false;

        LoadState.LoadSituation = false;

        print(Application.persistentDataPath);

        if (!System.IO.File.Exists(Application.persistentDataPath + "/testData.save"))
        {
            loadButton.SetActive(false);
            playButton.text = "Play";
        }
        else
        {
            loadButton.SetActive(true);
            playButton.text = "New Game";
        }
    }

    public void LoadSession()
    {
        Debug.Log("Loading last session.");
        LoadState.LoadSituation = true;
        Play();
    }

    public void Play()
    {
        Debug.Log("Start");
        SceneManager.LoadScene("SampleScene");
    }

    public void  Quit()
    {
        Debug.Log("Quitting.");
        Application.Quit();
    }
}
