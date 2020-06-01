using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAnimController : MonoBehaviour
{
    private Quaternion rotation;

    private void Start()
    {
        rotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = rotation;
    }
}
