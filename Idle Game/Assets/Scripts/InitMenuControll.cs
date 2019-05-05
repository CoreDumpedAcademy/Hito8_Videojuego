using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitMenuControll : MonoBehaviour
{
    private void Start()
    {
        LoadState.LoadSituation = false;
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
