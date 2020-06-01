using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStoredInfo : MonoBehaviour
{
    public static CharacterStoredInfo instance;

    public int night, day;

    public bool singlePlayer;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
