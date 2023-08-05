using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{    
    public static AudioManager instance;

    public AudioSource[] soundEffects;

    public AudioSource bgm, levelEndMusic, bossMusic;

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
        soundEffects[soundToPlay].Stop(); //paramos el sondio

        soundEffects[soundToPlay].pitch = Random.Range(0.9f,1.1f); //Hacemos que la frecuencia de pitch varie entre .9 y 1.1

        soundEffects[soundToPlay].Play(); //reproducimos el sonido
    }

    public void PlayLevelVictory()
    {
        bgm.Stop(); //paramos el backgroundmusic (bgm)
        levelEndMusic.Play(); //empezamos a reproducir la música de final de nivel
    }

    public void PlayBossMusic()
    {
        bgm.Stop();
        bossMusic.Play();
    }

    public void StopBossMusic()
    {
        bossMusic.Stop();
        bgm.Play();
    }


}
