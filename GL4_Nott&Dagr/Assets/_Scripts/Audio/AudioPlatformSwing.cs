using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlatformSwing : MonoBehaviour
{
    [FMODUnity.EventRef] public string ropeTwist, ropeWoosh;

    private bool isSwinging;
    private Rigidbody2D platformRB;

    // Start is called before the first frame update
    void Start()
    {
        platformRB = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (platformRB.velocity.x > -7.55f && platformRB.velocity.x < -7.45f || platformRB.velocity.x < 7.55f && platformRB.velocity.x > 7.45f)
        {
            isSwinging = true;
            if (isSwinging)
            {
                StartCoroutine(SwingWooshSound());
            }
        }
    }

    private IEnumerator SwingWooshSound()
    {
        isSwinging = false;
        yield return new WaitForSeconds(0.1f);
        FMODUnity.RuntimeManager.PlayOneShot(ropeWoosh);
        yield return new WaitForSeconds(0.5f);
        FMODUnity.RuntimeManager.PlayOneShot(ropeTwist);
    }
}
