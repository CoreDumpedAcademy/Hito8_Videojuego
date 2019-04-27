using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevController : MonoBehaviour
{
    public GameObject devParent;

    public ArrayList devArray = new ArrayList();             //structure with references to all Devs in scene 
    public int maxDevs;                                      //maximun amount of devs allowed
    void Start()
    {
        devParent = GameObject.Find("Devs").gameObject;

        maxDevs = 5;
    }

    public bool spawnDev(string dev)                 //takes the nave of the prefab as a parameter. Returns bool indicating if the operation was succesful
    {
        bool res = false;
        if(devArray.Count < maxDevs)
        {
            string path = dev;
            GameObject devObj = (GameObject) Instantiate(Resources.Load(path));

            devObj.transform.SetParent(devParent.transform, true);

            devArray.Add(devObj);
            
            res = true;
        }
        return res;
    }
}
