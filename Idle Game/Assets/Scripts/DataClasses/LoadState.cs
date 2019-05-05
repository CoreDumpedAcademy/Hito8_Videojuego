using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadState
{
    private static bool loadState;

    public static bool LoadSituation
    {
        get 
        {
            return loadState;
        }
        set 
        {
            loadState = value;
        }
    }
}
