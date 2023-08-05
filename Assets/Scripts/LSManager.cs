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

        if(PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach(MapPoint point in allPoints)
            {
                if(point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.transform.position = point.transform.position;
                    thePlayer.currentPoint = point;
                }
            }
        }
    }

    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }

    public IEnumerator LoadLevelCo()
    {
        LSUIManager.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / LSUIManager.instance.fadeSpeed) + .25f);

        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
        
    }
}
