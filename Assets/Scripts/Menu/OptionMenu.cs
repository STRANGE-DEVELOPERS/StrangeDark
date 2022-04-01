using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log(volume) * 20);
    }

    public void Sound ()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}
