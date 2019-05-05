using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitMenuControll : MonoBehaviour
{
    public void LoadSession()
    {
        Debug.Log("Loading last session.");
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
