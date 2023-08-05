using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint up, down, left, right;
    public bool isLevel, isLocked;    

    public string levelToLoad, levelToCheck, levelName;

    public int gemsCollected, totalGems; 
    public float bestTime, targetTime;

    public GameObject gemBadge, timeBadge;


    void Start()
    {
        if (isLevel && levelToLoad != null) //si estamos en un nivel
        {
            if(PlayerPrefs.HasKey(levelToLoad + "_gems"))  //y las preferencias tiene la llave de _gems
            {
                gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems"); //guardar las gemas de ese nivel
            }

            if(PlayerPrefs.HasKey(levelToLoad + "_time")) //y las preferencias tiene la llave de _time            
            {
                bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time"); //guardar el tiempo de ese nivel
            }

            if(gemsCollected >= totalGems && totalGems != 0) //si las gemas coleccionadas son mayores o igual al total de gemas de ese nivel
            {
                gemBadge.SetActive(true); //activa el ícono de la gema de ese nivel
            }

            if(bestTime < targetTime && bestTime != 0) //si el tiempo que tardaste en completar el nivel es menor o igual al tiempo record que tuviste antes
            {
                timeBadge.SetActive(true); //activa el ícono de tiempo de ese nivel
            }

            isLocked = true; //el nivel está bloquedo
            
            if (levelToCheck != null) //si el nivel que necesita chequear al nivel actual, no es nulo
            {
                if (PlayerPrefs.HasKey(levelToCheck + "_unlocked")) //verifica si existe una clave en PlayerPrefs que coincida con el nombre del nivel más "_unlocked".
                {
                    if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1) //Aquí, se obtiene el valor asociado a la clave del nivel y se compara con 1. Si el valor es igual a 1, significa que el nivel está desbloqueado.
                    {
                        isLocked = false; //desbloquea el nivel
                    }
                }
            }
            

            if (levelToLoad == levelToCheck)
            {
                isLocked = false;
            }
            
        }        
    }

    void Update()
    {
        
    }
}
