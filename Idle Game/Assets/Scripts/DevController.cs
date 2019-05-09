using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevController : MonoBehaviour
{
    public GameObject devParent;

    public List<Dev> devArray = new List<Dev>();             //structure with references to all Devs in scene 

    public int maxDevs = 8;                                      //maximun amount of devs allowed

    string devPrefName = "Dev";
    string devTypePath = "DevTypes";                       //Path in the resources folder to the stored DevData scriptable objects

    float counter;
    float cleaningTime = 0.5f;                           //Time in seconds it will check if any reference in devArray is null

    void Start()
    {
        devParent = GameObject.Find("DevGrid").gameObject;
    }

    private void Update() 
    {
        counter += Time.deltaTime;                  //Periodically checks if there are null references in devArray
        if(counter >= cleaningTime)
        {
            cleanDevArray();
            counter -= cleaningTime;
        }
    }

    void cleanDevArray()
    {
        for(int i = 0; i < devArray.Count; i++)
        {
            if(devArray[i] == null)
            {
                devArray.RemoveAt(i);
            }
        }
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
            //Debug.Log(devScript.getState().ToString());
            devScript.startUp(devData);
            devArray.Add(devScript);
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
            Debug.Log(++count);
            //Debug.Log(dev.ToString());
            DevData devType = findTypeByName(dev.type);
            //Debug.Log(devType.name);
            Dev devScript = spawnDev(devType);
            devScript.setState(dev);
        }
    }

    DevData findTypeByName(string name)
    {
        DevData devType;
        string path = devTypePath + "/" + name;
        Debug.Log(path);
        devType = Resources.Load<DevData>(path);
        return devType;
    }

    public void simulateOffLineWork(TimeSpan span)
    {
        double seconds = span.TotalSeconds;
        foreach (Dev dev in devArray)
        {
            int counter = 0;
            double simTime = 0;
            while (simTime < seconds)
            {
                dev.makeProgress();
                simTime += dev.prodPeriod;
                counter++;
            }
            Debug.Log("Veces producidas para " + dev.name + ": " + counter);
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
