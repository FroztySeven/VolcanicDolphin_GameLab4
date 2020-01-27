using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStoredInfo : MonoBehaviour
{
    public static CharacterStoredInfo instance;

    public int night, day;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Character Selection Test");
        }
    }
}
