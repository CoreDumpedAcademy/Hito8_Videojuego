using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource source;

    string audioClipsPath = "sfx";
    string musicClipsPath = "music";

    public Object[] audioClips;
    public Object[] musicClips;

    public Dictionary<string, AudioClip> audioClipDic;
    public Dictionary<string, AudioClip> musicClipDic;

    public audioClipsNames clipNames;

    void Start()
    {        
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

    public bool playSFX(string clipName)
    {
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
