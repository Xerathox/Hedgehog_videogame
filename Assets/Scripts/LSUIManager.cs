using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIManager : MonoBehaviour
{

    public static LSUIManager instance; 

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelInfoPanel;

    public Text levelName, gemsFound, gemsTarget, bestTime, timeTarget;
    
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        FadeFromBlack(); //quitamos la pantalla negra
    }

    void Update()
    {
        if(shouldFadeToBlack) //lógica para alternar la pantalla negra
        {
            fadeScreen.color = new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a, 255f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 255f)
            {
                shouldFadeToBlack = false;
            }
        }

        if(shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }                
    }

    public void FadeToBlack() //pantalla negra
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack() //remover pantalla negra
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }

    public void ShowInfo(MapPoint levelInfo) //muestra la información de cada nivel
    {
       levelName.text = levelInfo.levelName;

       gemsFound.text = "FOUND " + levelInfo.gemsCollected;
       gemsTarget.text = "IN LEVEL: " + levelInfo.totalGems;

       timeTarget.text = "TARGET: " + levelInfo.targetTime + "s";

       if (levelInfo.bestTime == 0)
       {
        bestTime.text = "BEST ---";
       } else 
       {
        bestTime.text = "BEST " + levelInfo.bestTime.ToString("F2") + "s";        
       }


       levelInfoPanel.SetActive(true);
    }

    public void HideInfo() //esconde la información de cada nivel
    {
        levelInfoPanel.SetActive(false);
    }

}
