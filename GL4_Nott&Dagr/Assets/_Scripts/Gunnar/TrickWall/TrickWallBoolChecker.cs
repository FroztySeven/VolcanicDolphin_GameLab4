using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickWallBoolChecker : MonoBehaviour
{
    // This is used to tell all the wall gameobjects in the trick wall prefab if they should or shouldn't be hidden.

    public TrickWallWallController _twwc;

    public bool imHidden;

    // Start is called before the first frame update
    void Start()
    {
        _twwc = this.gameObject.GetComponentInParent<TrickWallWallController>();
        if (_twwc.hideMe == true)
        {
            imHidden = true;
        }
        else
        {
            imHidden = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
