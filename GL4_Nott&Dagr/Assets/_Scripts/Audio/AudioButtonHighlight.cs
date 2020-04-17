using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioButtonHighlight : MonoBehaviour, ISelectHandler
{
    [FMODUnity.EventRef]
    public string buttonHighlight;

    public bool play;

    // Start is called before the first frame update
    void Start()
    {
        play = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Submit"))
        {
            play = false;
        }

        if (Input.GetAxisRaw("HorizontalP1") < 0 || Input.GetAxisRaw("VerticalP1") < 0 || Input.GetAxisRaw("HorizontalP1") > 0 || Input.GetAxisRaw("VerticalP1") > 0)
        {
            play = true;
        }
        if (Input.GetAxisRaw("HorizontalP2") < 0 || Input.GetAxisRaw("VerticalP2") < 0 || Input.GetAxisRaw("HorizontalP2") > 0 || Input.GetAxisRaw("VerticalP2") > 0)
        {
            play = true;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (this.gameObject)
        {
            if (play)
            {
                FMODUnity.RuntimeManager.PlayOneShot(buttonHighlight);
            }
        }
    }

    
}
