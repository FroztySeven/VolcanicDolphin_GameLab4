using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingPlant : MonoBehaviour
{
    public Sprite bottom, top;
    public Sprite[] middle;

    public GameObject plantPrefab;

    public List<GameObject> plantList;

    public int growHeight;
    public float growSpeed;

    private int growCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                //Debug.Log("Something hit me!");
                StartCoroutine(PlantGrow());
                gameObject.GetComponent<SpriteRenderer>().sprite = null;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    IEnumerator PlantGrow()
    {
        if (growCount <= growHeight)
        {
            GameObject plant = Instantiate(plantPrefab, transform.position + new Vector3(0f, growCount, 0f), transform.rotation);
            plant.transform.parent = gameObject.transform;
            plantList.Add(plant);
            plant.GetComponent<Plant>().plantId = growCount;

            if (growCount == 0)
            {
                plant.GetComponent<SpriteRenderer>().sprite = bottom;
            }
            else
            {
                plant.GetComponent<SpriteRenderer>().sprite = middle[Random.Range(0, middle.Length)];
            }

            if (growCount == growHeight)
            {
                plant.GetComponent<SpriteRenderer>().sprite = top;
            }


            yield return new WaitForSeconds(growSpeed);
            growCount++;
            StartCoroutine(PlantGrow());
        }
    }

    public IEnumerator FreezePlant(int plantIdUp, int plantIdDown)
    {
        if (!plantList[plantIdUp].GetComponent<Plant>().isFrozen)
        {
            yield return new WaitForSeconds(growSpeed);
            plantList[plantIdUp].GetComponent<Plant>().isFrozen = true;

            if (plantIdUp > 0)
            {
                plantIdUp -= 1;
                StartCoroutine(FreezePlant(plantIdUp, plantIdUp));
            }

            if (plantIdDown < plantList.Count - 1)
            {
                plantIdDown += 1;
                StartCoroutine(FreezePlant(plantIdUp, plantIdUp));
            }
        }

        if (!plantList[plantIdDown].GetComponent<Plant>().isFrozen)
        {
            yield return new WaitForSeconds(growSpeed);
            plantList[plantIdDown].GetComponent<Plant>().isFrozen = true;

            if (plantIdUp > 0)
            {
                plantIdUp -= 1;
                StartCoroutine(FreezePlant(plantIdDown, plantIdDown));
            }

            if (plantIdDown < plantList.Count - 1)
            {
                plantIdDown += 1;
                StartCoroutine(FreezePlant(plantIdDown, plantIdDown));
            }
        }
    }

    public IEnumerator UnFreezePlant(int plantIdUp, int plantIdDown)
    {
        if (plantList[plantIdUp].GetComponent<Plant>().isFrozen)
        {
            yield return new WaitForSeconds(growSpeed);
            plantList[plantIdUp].GetComponent<Plant>().isFrozen = false;

            if (plantIdUp > 0)
            {
                plantIdUp -= 1;
                StartCoroutine(UnFreezePlant(plantIdUp, plantIdUp));
            }

            if (plantIdDown < plantList.Count - 1)
            {
                plantIdDown += 1;
                StartCoroutine(UnFreezePlant(plantIdUp, plantIdUp));
            }
        }

        if (plantList[plantIdDown].GetComponent<Plant>().isFrozen)
        {
            yield return new WaitForSeconds(growSpeed);
            plantList[plantIdDown].GetComponent<Plant>().isFrozen = false;

            if (plantIdUp > 0)
            {
                plantIdUp -= 1;
                StartCoroutine(UnFreezePlant(plantIdDown, plantIdDown));
            }

            if (plantIdDown < plantList.Count - 1)
            {
                plantIdDown += 1;
                StartCoroutine(UnFreezePlant(plantIdDown, plantIdDown));
            }
        }
    }
}
