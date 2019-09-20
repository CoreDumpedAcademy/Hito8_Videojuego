using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TextSource
{
    public string Test;
    public string PercentSymbol;
    public string Yes;
    public string No;
    public SPlayScene PlayScene;
    public SInitialScene InitialScene;
    public Spaths Paths;
    public SSceneNames SceneNames;
}

[Serializable]
public struct SPlayScene
{
    public string Coins;
    public string CurrencySymbol;

    public string Game;

    public SOptions Options;

    public SStore Store;

    public SDevInfo DevInfo;

    public SBuffPanel BuffPanel;

    [Serializable]
    public struct SOptions
    {
        public string Settings;
        public string Save;
        public string SaveAndQuit;
        public string QuitGame;

        public string Reset;
        public SResetPanel ResetPanel;

        [Serializable]
        public struct SResetPanel
        {
            public string Attention;
            public string WarningText;
            public string ProductionSpeed;
            public string GoBack;
        }
    }

    [Serializable]
    public struct SStore
    {
        public string StoreName;
        public string NameIndicator;
        public string PriceIndicator;
        public string Buy;
        public string Exit;
    }

    [Serializable]
    public struct SDevInfo
    {
        public string LevelIndicator;
        public string Exhausted;
        public string MaxLevel;
        public string Energy;
        public string SellDev;
        public string SellWarning;

        public SActivities Activities;

        [Serializable]
        public struct SActivities
        {
            public string Working;
            public string Training;
            public string Resting;
        }
    }

    [Serializable]
    public struct SBuffPanel
    {
        public string Continue;
    }
}

[Serializable]
public struct SInitialScene
{
    public string GameTitle;
    public string Play;
    public string NewGame;
    public string LoadGame;
    public string QuitGame;
}

[Serializable]
public struct Spaths
{
    public string SavePath;
}

[Serializable]
public struct SSceneNames
{
    public string InitScene;
    public string PlayScene;
}

