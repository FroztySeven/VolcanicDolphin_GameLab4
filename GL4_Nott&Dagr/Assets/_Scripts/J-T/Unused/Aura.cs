using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    public GameObject auraPrefab;

    public Transform auraPos;

    public float auraOffset;

    public Transform[] auraRight, auraLeft, auraTop;

    private GameObject player;

    private bool spawnedRightAura = false;
    private bool spawnedLeftAura = false;
    private bool spawnedTopAura = false;

    private List<GameObject> extraAuras = new List<GameObject>();

    private void Start()
    {
        player = gameObject;
    }

    private void Update()
    {
        auraPos.position = player.transform.position;

        if (Input.GetAxis("RStickHorizontal") > 0)
        {
            if (!spawnedLeftAura)
            {
                foreach (GameObject aura in extraAuras)
                {
                    Destroy(aura);
                }

                for (int i = 0; i < auraLeft.Length; i++)
                {
                    GameObject newAura = Instantiate(auraPrefab, new Vector3(auraLeft[i].transform.position.x - auraOffset, auraLeft[i].transform.position.y, auraLeft[i].transform.position.z), auraRight[i].rotation);
                    newAura.transform.parent = auraPos;
                    extraAuras.Add(newAura);
                }
                spawnedLeftAura = true;
                spawnedRightAura = false;
                spawnedTopAura = false;
            }
        }
        else if (Input.GetAxis("RStickHorizontal") < 0)
        {

            if (!spawnedRightAura)
            {
                foreach (GameObject aura in extraAuras)
                {
                    Destroy(aura);
                }

                for (int i = 0; i < auraRight.Length; i++)
                {
                    GameObject newAura = Instantiate(auraPrefab, new Vector3(auraRight[i].transform.position.x + auraOffset, auraRight[i].transform.position.y, auraRight[i].transform.position.z), auraRight[i].rotation);
                    newAura.transform.parent = auraPos;
                    extraAuras.Add(newAura);
                }
                spawnedRightAura = true;
                spawnedLeftAura = false;
                spawnedTopAura = false;
            }
        }
        else if (Input.GetAxis("RStickVertical") > 0)
        {
            if (!spawnedTopAura)
            {
                foreach (GameObject aura in extraAuras)
                {
                    Destroy(aura);
                }

                for (int i = 0; i < auraTop.Length; i++)
                {
                    GameObject newAura = Instantiate(auraPrefab, new Vector3(auraTop[i].transform.position.x, auraTop[i].transform.position.y + auraOffset, auraTop[i].transform.position.z), auraRight[i].rotation);
                    newAura.transform.parent = auraPos;
                    extraAuras.Add(newAura);
                }
                spawnedTopAura = true;
                spawnedRightAura = false;
                spawnedLeftAura = false;
            }
        }
        else
        {
            foreach (GameObject aura in extraAuras)
            {
                Destroy(aura);
            }
            spawnedRightAura = false;
            spawnedLeftAura = false;
            spawnedTopAura = false;
        }
    }
}
