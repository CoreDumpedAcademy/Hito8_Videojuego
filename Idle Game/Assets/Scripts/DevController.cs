using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevController : MonoBehaviour
{
    public GameObject devParent;

    public ArrayList devArray = new ArrayList();             //structure with references to all Devs in scene 

    public int maxDevs = 8;                                      //maximun amount of devs allowed

    string devPrefName = "Dev";

    void Start()
    {
        devParent = GameObject.Find("DevGrid").gameObject;
    }

    public Dev spawnDev(DevData devData)                 //takes the nave of the prefab as a parameter. Returns bool indicating if the operation was succesful
    {
        Dev devScript = null;
        if(devArray.Count < maxDevs)
        {
            GameObject devPrefab = (GameObject)Resources.Load(devPrefName);
            GameObject devObj = (GameObject)Instantiate(devPrefab);
            devObj.transform.SetParent(devParent.transform);
            devScript = devObj.GetComponent<Dev>();
            devScript.startUp(devData);
            devArray.Add(devObj);           
        }
        return devScript;
    }
    /*
    //Provisionally loading prefabs from Resources
    GameObject getDev(string dev)
    {
        return (GameObject)Resources.Load(dev);
    }
    */
    void positionDev(GameObject dev)
    {
        Transform origin = dev.transform.Find("Origin");
        if(devArray.Count == 0)
        {
            origin.transform.position = Vector3.zero;
        }
    }
}
