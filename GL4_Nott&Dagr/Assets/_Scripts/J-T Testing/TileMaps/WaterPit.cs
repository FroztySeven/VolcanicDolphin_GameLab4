using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPit : MonoBehaviour
{
    public float freezeSpeed;

    public int waterLength;

    public GameObject waterPrefab;

    public List<SpriteRenderer> waterTiles;

    private void Start()
    {
        //for (int i = 0; i < waterTiles.Count; i++)
        //{
        //    waterTiles[i].GetComponent<Water>().waterId = i;
        //}

        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<BoxCollider2D>().enabled = false;

        for (int i = 0; i < waterLength; i++)
        {
            GameObject water = Instantiate(waterPrefab, transform.position + new Vector3(i, 0f, 0f), transform.rotation);
            water.GetComponent<Water>().waterId = i;
            water.transform.parent = gameObject.transform;
            waterTiles.Add(water.GetComponent<SpriteRenderer>());

        }
    }

    public IEnumerator FreezeWater(int waterIdLeft, int waterIdRight)
    {
        if (!waterTiles[waterIdLeft].GetComponent<Water>().isFrozen)
        {
            yield return new WaitForSeconds(freezeSpeed);
            waterTiles[waterIdLeft].GetComponent<Water>().isFrozen = true;

            if (waterIdLeft > 0)
            {
                waterIdLeft -= 1;
                StartCoroutine(FreezeWater(waterIdLeft, waterIdLeft));
            }

            if (waterIdRight < waterTiles.Count - 1)
            {
                waterIdRight += 1;
                StartCoroutine(FreezeWater(waterIdLeft, waterIdLeft));
            }
        }

        if (!waterTiles[waterIdRight].GetComponent<Water>().isFrozen)
        {
            yield return new WaitForSeconds(freezeSpeed);
            waterTiles[waterIdRight].GetComponent<Water>().isFrozen = true;

            if (waterIdLeft > 0)
            {
                waterIdLeft -= 1;
                StartCoroutine(FreezeWater(waterIdRight, waterIdRight));
            }

            if (waterIdRight < waterTiles.Count - 1)
            {
                waterIdRight += 1;
                StartCoroutine(FreezeWater(waterIdRight, waterIdRight));
            }
        }
    }

    public IEnumerator UnFreezeWater(int waterIdLeft, int waterIdRight)
    {
        if (waterTiles[waterIdLeft].GetComponent<Water>().isFrozen)
        {
            yield return new WaitForSeconds(freezeSpeed);
            waterTiles[waterIdLeft].GetComponent<Water>().isFrozen = false;
            waterTiles[waterIdLeft].GetComponent<Water>().unfreeze = false;
            waterTiles[waterIdLeft].GetComponent<Water>().unfreezeTimer = 0;
            waterTiles[waterIdLeft].GetComponent<Water>().unfreezeNumber = 0;

            if (waterIdLeft > 0)
            {
                waterIdLeft -= 1;
                StartCoroutine(UnFreezeWater(waterIdLeft, waterIdLeft));
            }

            if (waterIdRight < waterTiles.Count - 1)
            {
                waterIdRight += 1;
                StartCoroutine(UnFreezeWater(waterIdLeft, waterIdLeft));
            }
        }

        if (waterTiles[waterIdRight].GetComponent<Water>().isFrozen)
        {
            yield return new WaitForSeconds(freezeSpeed);
            waterTiles[waterIdRight].GetComponent<Water>().isFrozen = false;
            waterTiles[waterIdRight].GetComponent<Water>().unfreeze = false;
            waterTiles[waterIdLeft].GetComponent<Water>().unfreezeTimer = 0;
            waterTiles[waterIdLeft].GetComponent<Water>().unfreezeNumber = 0;

            if (waterIdLeft > 0)
            {
                waterIdLeft -= 1;
                StartCoroutine(UnFreezeWater(waterIdRight, waterIdRight));
            }

            if (waterIdRight < waterTiles.Count - 1)
            {
                waterIdRight += 1;
                StartCoroutine(UnFreezeWater(waterIdRight, waterIdRight));
            }
        }
    }
}
