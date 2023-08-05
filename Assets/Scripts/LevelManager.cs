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
        timeInLevel = 0f;
    }

    void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f);
        UIController.instance.FadeFromBlack();
        PlayerController.instance.gameObject.SetActive(true);
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;
        UIController.instance.UpdateHealthDisplay(); 
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo() 
    {
        AudioManager.instance.PlayLevelVictory();

        PlayerController.instance.stopInput = true;

        CameraController.instance.stopFollow = true;

        UIController.instance.levelCompleteText.SetActive(true);

        yield return new WaitForSeconds(1.5f);        

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .25f);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel",SceneManager.GetActiveScene().name);
        

        if (gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems", 0))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected); //guardar los valores de las gemas en el nivel que estamos jugando
        }

        if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time") || timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel); //guardar los valores del tiempo en el nivel que estamos jugando
        }


        SceneManager.LoadScene(levelToLoad);
    }
}
