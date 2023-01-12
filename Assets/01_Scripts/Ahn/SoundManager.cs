using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource musicsource;

    public AudioSource btnsource;
    public AudioMixer masterMixer;
    public Slider slider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }

    public void ControlVolume()
    {
        float sound = slider.value;
        if (sound == -40f)
        {
            masterMixer.SetFloat("Master", -80f);
        }
        else
        {
            masterMixer.SetFloat("Master", 0f);
        }
    }


    public void OnSfx()
    {
        btnsource.Play();
    }
}
