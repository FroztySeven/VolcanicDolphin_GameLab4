using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpritePulsate : MonoBehaviour
{
    public float timeBetweenPulses = 5;
    public float pulseSpeed = 1;

    public Sprite[] minions;

    private Transform spawnPoint;

    private float pulsateTimer;

    private Color startColor;
    private Color fadeColor;

    private SpriteRenderer theSR;

    private bool isFadingIn;
    private bool isFadingOut;

    private void Start()
    {
        spawnPoint = transform;
        theSR = gameObject.AddComponent<SpriteRenderer>();
        startColor = theSR.color;
        fadeColor = new Vector4(startColor.r, startColor.g, startColor.b, 0f);
        theSR.color = fadeColor;
        theSR.sprite = minions[0];
        isFadingIn = true;
    }

    private void Update()
    {
        ColorFade();
    }

    private void ColorFade()
    {
        if (isFadingIn)
        {
            pulsateTimer += Time.deltaTime;

            if (pulsateTimer >= timeBetweenPulses)
            {
                theSR.color = Vector4.MoveTowards(theSR.color, startColor, Time.deltaTime * pulseSpeed);

                if (theSR.color == startColor)
                {
                    isFadingIn = false;
                    isFadingOut = true;
                    pulsateTimer = 0;
                }
            }
        }

        if (isFadingOut)
        {
            theSR.color = Vector4.MoveTowards(theSR.color, fadeColor, Time.deltaTime * pulseSpeed);

            if (theSR.color == fadeColor)
            {
                if (theSR.sprite == minions[0])
                {
                    theSR.sprite = minions[1];
                }
                else if (theSR.sprite == minions[1])
                {
                    theSR.sprite = minions[0];
                }

                isFadingIn = true;
                isFadingOut = false;
                pulsateTimer = 0;
            }
        }
    }
}
