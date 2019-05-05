using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public bool status;
    public GameObject Menu;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        status = false;
        Menu.SetActive(false);
    }

    // Update is called once per frame
    public void Guardar ()
    {
        Debug.Log("Guardando partida");
        gameController.saveGame();
    }

    public void SalirGuardar()
    {
        Guardar();
        SceneManager.LoadScene("InitialScene");
    }

    public void Salir()
    {
        SceneManager.LoadScene("InitialScene");
    }

    public void OpenOptions()
    {
        if (!status)
        {
            Menu.SetActive(true);
            status = true;
        }
        else
        {
            Menu.SetActive(false);
            status = false;
        }
    }
}
