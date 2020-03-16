using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    private Slider loadingSlider;
    // Start is called before the first frame update
    void Start()
    {
        loadingSlider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        loadingSlider.value += Time.deltaTime;
    }
}
