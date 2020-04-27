using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingLevelChecking : MonoBehaviour
{
    public Text loadingText;
    
    // If Scene does not exist in build settings give an error message
    void Update()
    {
        if (SceneUtility.GetBuildIndexByScenePath("Assets/Scenes") == -1)
        {
            loadingText.text = "ERROR: Missing defined level to load! \nReturn to the menu using the Back button.";
        }
    }
}
