using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelFromInspector : MonoBehaviour
{
    public GameObject Canvas_Loading;
    
    //Attach to a level button in the inspector
    public void _LoadLevelByName(string level)
    {
        Canvas_Loading.SetActive(true);
        SceneManager.LoadScene(level);
    }
    public void _LoadLevelByIndex(int level)
    {
        Canvas_Loading.SetActive(true);
        SceneManager.LoadScene(level);
    }
}
