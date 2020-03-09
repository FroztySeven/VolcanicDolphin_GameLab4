using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueChapterButton : MonoBehaviour
{
    public GameObject continueChapterButton;
    public GameObject Canvas_Loading;
    private int levelToContinue;

    // Start is called before the first frame update
    void Start()
    {
        continueChapterButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //If the player has previously exited one of the levels, then get the PlayerPrefs key that is set from script "PlayerMovementTest"
        if (PlayerPrefs.HasKey("SavedPrefs"))
        {
            continueChapterButton.SetActive(true);
        }
    }

    //Use the continue button from the hierarchy and load the latest level that the player has returned to the menu from
    public void LoadContinue()
    {
        levelToContinue = PlayerPrefs.GetInt("SavedPrefs");

        if (levelToContinue != 0)
        {
            Canvas_Loading.SetActive(true);
            SceneManager.LoadScene(levelToContinue);
        }
    }
}
