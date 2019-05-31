using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InitMenuControll : MonoBehaviour
{
    public GameObject loadButtonObject;

    public TextMeshProUGUI title;
    public TextMeshProUGUI loadButton;
    public TextMeshProUGUI playButton;
    public TextMeshProUGUI quitButton;

    string textSourceFile = "generalTextSource";

    public TextSource GeneralTextSource;

    SInitialScene sceneText;

    private void Start()
    {
        GameText.text = LoadGameText();
        sceneText = GameText.text.InitialScene;

        Screen.fullScreen = false;
        LoadState.LoadSituation = false;

        print(Application.persistentDataPath);

        Debug.Log(GeneralTextSource.Test);

        title.text = sceneText.GameTitle;
        loadButton.text = sceneText.LoadGame;
        quitButton.text = sceneText.QuitGame;

        if (!System.IO.File.Exists(Application.persistentDataPath + "/" + GameText.text.Paths.SavePath))
        {
            loadButtonObject.SetActive(false);
            playButton.text = sceneText.Play;
        }
        else
        {
            loadButtonObject.SetActive(true);
            playButton.text = sceneText.NewGame;
        }
    }

    public TextSource LoadGameText(){                       //Loads game text
        TextSource textSource = new TextSource();

        var file = Resources.Load<TextAsset>(textSourceFile);
        Debug.Log(file.text);
        textSource = JsonUtility.FromJson<TextSource>(file.text);

        return textSource;
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
        SceneManager.LoadScene(GameText.text.SceneNames.PlayScene);
    }

    public void  Quit()
    {
        Debug.Log("Quitting.");
        Application.Quit();
    }
}
