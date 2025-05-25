using System;
using UnityEngine;

public class AudioManagerService : Singleton<AudioManagerService>
{

    public AudioSource bgMusic;
    public AudioSource sfx;

    public AudioClip shift;
    public AudioClip flip;
    public AudioClip addCake;
    public AudioClip successCake;
    public AudioClip fail;

    public void PlayAudio(string audioName)
    {
        switch (audioName)
        {
            case "shift":
                sfx.PlayOneShot(shift);
                break;
            case "addCake":
                sfx.PlayOneShot(addCake);
                break;
            case "successCake":
                sfx.PlayOneShot(successCake);
                break;
            case "flip":
                sfx.PlayOneShot(flip);
                break;
            case "fail":
                bgMusic.Stop();
                sfx.PlayOneShot(fail);
                break;
            
        }
    }
}
