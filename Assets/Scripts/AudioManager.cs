using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{    
    public static AudioManager instance;

    public AudioSource[] soundEffects;

    public AudioSource bgm, levelEndMusic;

    //version 2.0

    private void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();

        soundEffects[soundToPlay].pitch = Random.Range(0.9f,1.1f);

        soundEffects[soundToPlay].Play();
    }

    public void PlayLevelVictory()
    {
        bgm.Stop();
        levelEndMusic.Play();
    }

}
