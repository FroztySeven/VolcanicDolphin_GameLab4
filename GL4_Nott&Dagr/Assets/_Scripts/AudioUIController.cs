using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUIController : MonoBehaviour
{
    //... this script is used to play sounds when interacting with UI.

    public Button buttonStart;
    public Button buttonRestart;
    public Button buttonExitFromMenu;
    public Button buttonExitFromPause;

    public bool useMusic;

    //--------------------------------------------------------------------------------------//

    [FMODUnity.EventRef]
    public string mouseUIClick;

    [FMODUnity.EventRef]
    public string mouseUIHover;

    //--------------------------------------------------------------------------------------//

    // Start is called before the first frame update
    void Awake()
    {   
        //... gets the buttons to use.

        buttonStart = GameObject.Find("StartButton").GetComponent<Button>();
        buttonRestart = GameObject.Find("Restart Button").GetComponent<Button>();
        buttonExitFromMenu = GameObject.Find("ExitButton").GetComponent<Button>();
        buttonExitFromPause = GameObject.Find("Exit Button").GetComponent<Button>();

        Button btn1 = buttonStart.GetComponent<Button>();
        btn1.onClick.AddListener(TaskOnClick);

        Button btn2 = buttonRestart.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClick);

        Button btn3 = buttonExitFromMenu.GetComponent<Button>();
        btn3.onClick.AddListener(TaskOnClick);

        Button btn4 = buttonExitFromPause.GetComponent<Button>();
        btn4.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //--------------------------------------------------------------------------------------//

    void TaskOnClick()
    {
        Invoke("CallMouseClick", 0);

        if (useMusic == true)
        {
            GameObject.Find("AudioController").GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
        }
    }

    public void PointerEnter()
    {
        Invoke("CallMouseHover", 0);
    }

    //--------------------------------------------------------------------------------------//

    void CallMouseClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot(mouseUIClick);
    }

    void CallMouseHover()
    {
        FMODUnity.RuntimeManager.PlayOneShot(mouseUIHover);
    }
}
