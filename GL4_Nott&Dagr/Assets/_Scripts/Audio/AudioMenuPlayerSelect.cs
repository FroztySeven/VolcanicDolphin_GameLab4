using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuPlayerSelect : MonoBehaviour
{
    [FMODUnity.EventRef] public string playerSelect;

    //[HideInInspector]
    public GameObject player1, player2;
    [HideInInspector]
    public bool left1, center1, right1, playLeft1, playCenter1, playRight1;
    [HideInInspector]
    public bool left2, center2, right2, playLeft2, playCenter2, playRight2;
    //[HideInInspector]
    public CharacterSelection _cs1, _cs2;


    // Start is called before the first frame update
    void Start()
    {
        _cs1 = player1.GetComponent<CharacterSelection>();
        _cs2 = player2.GetComponent<CharacterSelection>();

        playLeft1 = true;
        playCenter1 = true;
        playRight1 = true;
        
        playLeft2 = true;
        playCenter2 = true;
        playRight2 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.GetComponent<CharacterSelection>().changedLocation && player1.transform.localPosition.x <= -355)
        {
            left1 = true;

            if (left1 && playLeft1)
            {
                StartCoroutine(PlaySoundLeft1());
                left1 = false;
                playLeft1 = false;
            }
        }

        if (player1.GetComponent<CharacterSelection>().changedLocation && player1.transform.localPosition.x == 0)
        {
            center1 = true;

            if (center1 && playCenter1)
            {
                StartCoroutine(PlaySoundCenter1());
                center1 = false;
                playCenter1 = false;
            }
        }

        if (player1.GetComponent<CharacterSelection>().changedLocation && player1.transform.localPosition.x >= 345)
        {
            right1 = true;

            if (right1 && playRight1)
            {
                StartCoroutine(PlaySoundRight1());
                right1 = false;
                playRight1 = false;
            }
        }

        if (player2.GetComponent<CharacterSelection>().changedLocation && player2.transform.localPosition.x <= -355)
        {
            left2 = true;

            if (left2 && playLeft2)
            {
                StartCoroutine(PlaySoundLeft2());
                left2 = false;
                playLeft2 = false;
            }
        }

        if (player2.GetComponent<CharacterSelection>().changedLocation && player2.transform.localPosition.x == 0)
        {
            center2 = true;

            if (center2 && playCenter2)
            {
                StartCoroutine(PlaySoundCenter2());
                center2 = false;
                playCenter2 = false;
            }
        }

        if (player2.GetComponent<CharacterSelection>().changedLocation && player2.transform.localPosition.x >= 345)
        {
            right2 = true;

            if (right2 && playRight2)
            {
                StartCoroutine(PlaySoundRight2());
                right2 = false;
                playRight2 = false;
            }
        }

    }
    private IEnumerator PlaySoundLeft1()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerSelect);
        yield return new WaitForSeconds(0.5f);
        playLeft1 = true;
    }

    private IEnumerator PlaySoundCenter1()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerSelect);
        yield return new WaitForSeconds(0.5f);
        playCenter1 = true;
    }

    private IEnumerator PlaySoundRight1()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerSelect);
        yield return new WaitForSeconds(0.5f);
        playRight1 = true;
    }

    private IEnumerator PlaySoundLeft2()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerSelect);
        yield return new WaitForSeconds(0.5f);
        playLeft2 = true;
    }

    private IEnumerator PlaySoundCenter2()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerSelect);
        yield return new WaitForSeconds(0.5f);
        playCenter2 = true;
    }

    private IEnumerator PlaySoundRight2()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerSelect);
        yield return new WaitForSeconds(0.5f);
        playRight2 = true;
    }
}
