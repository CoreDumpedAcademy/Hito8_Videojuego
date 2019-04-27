using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevController : MonoBehaviour
{
    public GameObject devCanvas;

    public ArrayList devArray = new ArrayList();             //structure with references to all Devs in scene 
    public int maxDevs;                                      //maximun amount of devs allowed
    void Start()
    {
        devCanvas = GameObject.Find("DevCanvas").transform.Find("Canvas").gameObject;

        maxDevs = 5;
    }

    public bool spawnDev(string dev)                 //takes the nave of the prefab as a parameter. Returns bool indicating if the operation was succesful
    {
        bool res = false;
        if(devArray.Count < maxDevs)
        {
            string path = "Devs/" + dev;
            GameObject devObj = (GameObject) Instantiate(Resources.Load(path));

            devObj.transform.parent = devCanvas.transform;

            devArray.Add(devObj);

            devArray[0].ToString();
            res = true;
        }
        return res;
    }
}
