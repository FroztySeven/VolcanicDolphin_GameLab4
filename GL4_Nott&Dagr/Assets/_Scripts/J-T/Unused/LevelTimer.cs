using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    public bool timerActive;

    public float levelLength;

    private float levelTimer = 0;

    private void Update()
    {
        if (timerActive)
        {
            levelTimer += Time.deltaTime;

            if (levelTimer >= levelLength)
            {
                levelTimer = 0;
                timerActive = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
