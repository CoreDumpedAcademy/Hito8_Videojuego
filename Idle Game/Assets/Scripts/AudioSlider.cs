using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public GameObject controller;
    new AudioController audio;

    Slider audioSlider;
    float audioVal;

    void Start()
    {
        audio = controller.GetComponent<AudioController>();
        audioSlider = gameObject.GetComponent<Slider>();
        audioVal = PlayerPrefs.GetFloat(audio.prefMasterVolume, 0);
        audioSlider.value = audioVal;
    }

    void Update()
    {
        audioVal = audioSlider.value;
        PlayerPrefs.SetFloat(audio.prefMasterVolume, audioVal);
    }
}
