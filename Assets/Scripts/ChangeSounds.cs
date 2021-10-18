using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeSounds : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider SoundSlider;

    void Start()
    {
        SoundSlider = GetComponent<Slider>();   
    }

    public void setSounds()
    {
        mixer.SetFloat("SFX", Mathf.Log10(SoundSlider.value) * 20);
    }
}
