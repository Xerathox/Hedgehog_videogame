using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startScene, continueScene;

    public GameObject continueButton;

    private void Start()
    {
        if(PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            continueButton.SetActive(true); //podemos continuar si el nivel tiene la key _unlocked
        }
        else 
        {
            continueButton.SetActive(false); //no podemos continuar si el nivel no tiene la key _unlocked
        }
    }

    public void StartGame() //Si presionamos el primer botón del menu principal
    {
        SceneManager.LoadScene(startScene); //Cargamos el primer nivel
        PlayerPrefs.DeleteAll(); //borramos todas las preferencias
    }
    
    public void ContinueGame() //Si presionamos el segundo botón del menu principal
    {
        SceneManager.LoadScene(continueScene); //Cargamos desde donde lo dejamos 
    }

    public void QuitGame() //Si presionamos el tercer botón del menu principal
    {
        Application.Quit(); //cerramos el juego
    }

}
