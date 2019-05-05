using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevController : MonoBehaviour
{
    public GameObject devParent;

    public List<Dev> devArray = new List<Dev>();             //structure with references to all Devs in scene 

    public int maxDevs = 8;                                      //maximun amount of devs allowed


    void Start()
    {
        devParent = GameObject.Find("DevGrid").gameObject;
    }

    public Dev spawnDev(string dev)                 //takes the nave of the type as a parameter. Returns Dev script of the object created
    {
        Dev devScript = null;
        if(devArray.Count < maxDevs)
        {
            GameObject devObj = getDev(dev);
            devObj = (GameObject)Instantiate(devObj);
            devObj.transform.SetParent(devParent.transform);
            devScript = devObj.GetComponent<Dev>();
            //Debug.Log(devScript.getState().ToString());
            devArray.Add(devScript);
        }
        return devScript;
    }

    //Provisionally loading prefabs from Resources
    GameObject getDev(string dev)
    {
        return (GameObject)Resources.Load(dev);
    }

    //To save state
    public List<DevState> getDevState()
    {
        List<DevState> devStateArray = new List<DevState>();
        int count = 0;
        foreach(Dev dev in devArray)
        {
            Debug.Log(++count);
            Debug.Log(dev.getState().ToString());
            devStateArray.Add(dev.getState());
        }
        return devStateArray;
    }
    //To load state
    public void clearDevs()
    {
        foreach(Dev dev in devArray)
        {
            GameObject.Destroy(dev.gameObject);
        }
        devArray.Clear();
        writeDevs();
    }

    public void recreateDevs(List<DevState> devStateArray)
    {
        int count = 0;
        foreach (DevState dev in devStateArray)
        {
            //Debug.Log(++count);
            //Debug.Log(dev.ToString());
            Dev devScript = spawnDev(dev.type);
            devScript.setState(dev);
        }
    }

    public void writeDevs()
    {
        Debug.Log("Count: " + devArray.Count);
        int count = 0;
        foreach(Dev dev in devArray)
        {
            Debug.Log("Element: " + ++count);
            Debug.Log(dev.getState().ToString());
        }
    }
}
