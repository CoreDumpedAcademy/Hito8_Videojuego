using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private GameObject controllerObj;
    private GameController controller;
    public GameObject sourceObj;
    public GameObject listenerObj;
    private AudioSource source;
    private AudioListener listener;

    string audioClipsPath = "sfx";
    string musicClipsPath = "music";

    public Object[] audioClips;
    public Object[] musicClips;

    public Dictionary<string, AudioClip> audioClipDic = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> musicClipDic = new Dictionary<string, AudioClip>();

    public audioClipsNames clipNames;

    public string prefMasterVolume = "MasterVolume";

    void Start()
    {
        controllerObj = GameObject.Find("GameController");
        controller = controllerObj.GetComponent<GameController>();
        source = sourceObj.GetComponent<AudioSource>();
        listener = listenerObj.GetComponent<AudioListener>();

        Debug.Log("Start");

        audioClips = Resources.LoadAll(audioClipsPath, typeof(AudioClip));
        musicClips = Resources.LoadAll(musicClipsPath, typeof(AudioClip));

        clipNames = defineClipNames();

        foreach (AudioClip audio in audioClips)
        {
            audioClipDic.Add(audio.name, audio);
        }
        foreach (AudioClip music in musicClips)
        {
            musicClipDic.Add(music.name, music);
        }
    }

    private void Update()
    {
        AudioListener.volume = PlayerPrefs.GetFloat(prefMasterVolume, 0); ;
    }

    public bool playSFX(string clipName)
    {
        if (controller == null || controller.loading) return false;

        AudioClip value;
        bool result = audioClipDic.TryGetValue(clipName, out value);
        if (result)
        {
            source.PlayOneShot(value);
        }
        else
        {
            Debug.Log("Audio not found");
        }
        return result;
    }

    audioClipsNames defineClipNames()
    {
        audioClipsNames names = new audioClipsNames();

        names.btnClick = "sfx_UI_btnClick";
        names.completeGameDing = "sfx_evt_gameComplete";
        names.completeGameCoins = "sfx_evt_getCoins";
        names.cashRegister = "sfx_UI_cashRegister";
        names.devLvlUp = "sfx_evt_devLvlUp";

        return names;
    }

    public struct audioClipsNames
    {
        public string btnClick;
        public string completeGameDing;
        public string completeGameCoins;
        public string cashRegister;
        public string devLvlUp;
    }
}
