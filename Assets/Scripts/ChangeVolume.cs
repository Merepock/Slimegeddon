using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider MusicSlider;

    void Start()
    {
        MusicSlider = GetComponent<Slider>();           
    }

    public void SetVolume()
    {
        mixer.SetFloat("BGM", Mathf.Log10(MusicSlider.value) * 20); //Equates to a value between 0 and -80 decibels
    }
}
