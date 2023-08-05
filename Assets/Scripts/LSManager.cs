using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{
    public LSPlayer thePlayer;

    private MapPoint[] allPoints;

    void Start()
    {
        allPoints = FindObjectsOfType<MapPoint>();

        if(PlayerPrefs.HasKey("CurrentLevel")) //Si se ha guardado algún nivel que tenga la llave de "CurrentLevel"
        {
            foreach(MapPoint point in allPoints) //recorremos todos los mapas 
            {
                if(point.levelToLoad == PlayerPrefs.GetString("CurrentLevel")) //si el nivel a cargar tiene la llave CurrentLevel
                {
                    thePlayer.transform.position = point.transform.position; //colocamos al jugador al final del nivel en el nivel que terminó
                    thePlayer.currentPoint = point; //ahora el punto se vuelve el currentPoint para que el jugador spawnee allí
                }
            }
        }
    }

    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo()); //cargamos la corrutina
    }

    public IEnumerator LoadLevelCo()
    {
        LSUIManager.instance.FadeToBlack(); //colocamos la pantalla negra

        yield return new WaitForSeconds((1f / LSUIManager.instance.fadeSpeed) + .25f); //esperamos un tiempito

        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad); //cargamos la escena
        
    }
}
