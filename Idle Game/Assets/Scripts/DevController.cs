using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevController : MonoBehaviour
{
    public GameObject devParent;

    public ArrayList devArray = new ArrayList();             //structure with references to all Devs in scene 

    public int maxDevs = 8;                                      //maximun amount of devs allowed


    void Start()
    {
        devParent = GameObject.Find("DevGrid").gameObject;
    }

    public bool spawnDev(string dev)                 //takes the nave of the prefab as a parameter. Returns bool indicating if the operation was succesful
    {
        bool res = false;
        if(devArray.Count < maxDevs)
        {
            GameObject devObj = getDev(dev);
            devObj = (GameObject)Instantiate(devObj);
            devObj.transform.SetParent(devParent.transform);
            devArray.Add(devObj);
            res = true;
        }
        return res;
    }

    //Provisionally loading prefabs from Resources
    GameObject getDev(string dev)
    {
        return (GameObject)Resources.Load(dev);
    }

    void positionDev(GameObject dev)
    {
        Transform origin = dev.transform.Find("Origin");
        if(devArray.Count == 0)
        {
            origin.transform.position = Vector3.zero;
        }
    }
}
