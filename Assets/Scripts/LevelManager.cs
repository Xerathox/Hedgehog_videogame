using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitToRespawn;
    public int gemsCollected;
    public float timeInLevel;
    public string levelToLoad;

    public void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        timeInLevel = 0f; //seteamos el tiempo en 0 para el contador po nivel
    }

    void Update()
    {
        timeInLevel += Time.deltaTime; // el contador de tiempo comienza a avanzar 
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo()); //cuando el jugador spawnea, comienza la corrutina
    }

    IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false); //el jugador va a ser desactivado cuando muere
        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed)); //segundos que esperamos para hacer respawn
        UIController.instance.FadeToBlack(); //prendemos la pantalla de negro
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f); //esperamos un tiempo
        UIController.instance.FadeFromBlack(); //apagamos la pantalla de negro
        PlayerController.instance.gameObject.SetActive(true); //el jugador es nuevamenet activado
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth; //le llenamos la vida al jugador 
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint; //respawnea en el último checkpoint tocado
        UIController.instance.UpdateHealthDisplay(); //le hacemos un refresheo a la vida para poder visualizarla  
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo()); //empezamos con la corrutina cuando terminamos el nivel
    }

    public IEnumerator EndLevelCo() 
    {
        AudioManager.instance.PlayLevelVictory(); //Llamamos a la función que reproduce la música de victoria

        PlayerController.instance.stopInput = true; //Le quitamos los inputs al jugador para que el jugador no lo pueda mover más

        CameraController.instance.stopFollow = true; //La cámara se quedará estática

        UIController.instance.levelCompleteText.SetActive(true); //Aparecerá en pantalla el texto de que ganaste el vneil

        yield return new WaitForSeconds(1.5f); //Esperamos unos segundos

        UIController.instance.FadeToBlack(); //La pantalla se volverá negra

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .25f); //Esperamos un ratito más

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1); //Desbloqueamos el siguiente nivel y lo guardamos en las preferencias del jugador

        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name); //Guardamos en las preferencias del jugador en nivel en el que estamos
        

        if (gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems", 0)) //si recolectamos más gemas que la partida pasada
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected); //guardar los valores de las gemas en el nivel que estamos jugando
        }

        if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time") || timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time")) //si logramos completar el nivel en menos tiempo que la anterior vez
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel); //guardar los valores del tiempo en el nivel que estamos jugando
        }


        SceneManager.LoadScene(levelToLoad); //Cargamos la selección de niveles
    }
}
