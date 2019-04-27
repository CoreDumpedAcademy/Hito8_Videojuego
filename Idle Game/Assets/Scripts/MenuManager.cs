using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private bool status;
    public GameObject Menu;

    // Start is called before the first frame update
    void Start()
    {
        status = false;
        Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenShop()
    {
        if(!status)
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
