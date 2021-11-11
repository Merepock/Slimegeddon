using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider VolumeSlider;

    void Start()
    {
        VolumeSlider = GetComponent<Slider>();           
    }

    public void SetBGMVolume()
    {
        mixer.SetFloat("BGM", Mathf.Log10(VolumeSlider.value) * 20); //Equates to a value between 0 and -80 decibels
    }

    public void setSFXVolune()
    {
        mixer.SetFloat("SFX", Mathf.Log10(VolumeSlider.value) * 20);
    }
}
