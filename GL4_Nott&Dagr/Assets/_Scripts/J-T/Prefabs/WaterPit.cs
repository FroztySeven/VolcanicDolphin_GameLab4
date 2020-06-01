using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPit : MonoBehaviour
{
    public float freezeSpeed;

    public int waterLength;

    public bool isBehindWall = false;

    public bool isAnimated;
    public float animationRate;

    public GameObject waterPrefab;

    public List<SpriteRenderer> waterTiles;

    private float spriteTimer = 0;
    [HideInInspector]
    public int spriteNumber = 0;
    [HideInInspector]
    public int animationFrames;

    private float waterFreezeTimer = 0;
    private bool isFreezing;
    private int waterTileFreezeLeft, waterTileFreezeRight;

    private float waterUnfreezeTimer = 0;
    private bool isUnfreezing;
    private int waterTileUnfreezeLeft, waterTileUnfreezeRight;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<BoxCollider2D>().enabled = false;

        for (int i = 0; i < waterLength; i++)
        {
            GameObject water = Instantiate(waterPrefab, transform.position + new Vector3(i, 0f, 0f), transform.rotation);
            water.GetComponent<Water>().waterId = i;
            water.transform.parent = gameObject.transform;
            waterTiles.Add(water.GetComponent<SpriteRenderer>());

            if (isBehindWall && i == 0 || isBehindWall && i == waterLength - 1)
            {
                water.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    private void Update()
    {
        if (isAnimated)
        {
            if (spriteTimer < 1)
            {
                spriteTimer += Time.deltaTime * animationRate;
            }
            if (spriteTimer >= 1)
            {
                spriteNumber++;
                spriteTimer = 0;
            }
            if (spriteNumber == animationFrames)
            {
                spriteNumber = 0;
            }
        }

        if (isFreezing)
        {
            if (waterFreezeTimer < freezeSpeed)
            {
                waterFreezeTimer += Time.deltaTime;
            }
            else
            {
                waterFreezeTimer = 0;
                if (waterTileFreezeLeft >= 0)
                {
                    waterTiles[waterTileFreezeLeft].GetComponent<Water>().Frozen();
                    waterTileFreezeLeft--;
                }
                if (waterTileFreezeRight < waterLength)
                {
                    waterTiles[waterTileFreezeRight].GetComponent<Water>().Frozen();
                    waterTileFreezeRight++;
                }

                if (waterTileFreezeLeft < 0 && waterTileFreezeRight == waterLength)
                {
                    waterFreezeTimer = 0;
                    isFreezing = false;
                }
            }
        }

        if (isUnfreezing)
        {
            if (waterUnfreezeTimer < freezeSpeed)
            {
                waterUnfreezeTimer += Time.deltaTime;
            }
            else
            {
                waterUnfreezeTimer = 0;
                if (waterTileUnfreezeLeft >= 0)
                {
                    waterTiles[waterTileUnfreezeLeft].GetComponent<Water>().Unfrozen();
                    waterTileUnfreezeLeft--;
                }
                if (waterTileUnfreezeRight < waterLength)
                {
                    waterTiles[waterTileUnfreezeRight].GetComponent<Water>().Unfrozen();
                    waterTileUnfreezeRight++;
                }

                if (waterTileUnfreezeLeft < 0 && waterTileUnfreezeRight == waterLength)
                {
                    waterUnfreezeTimer = 0;
                    isUnfreezing = false;
                }
            }
        }
    }

    public void FreezeWater(int waterId, bool freezing)
    {
        waterTileFreezeLeft = waterId - 1;
        waterTileFreezeRight = waterId + 1;

        waterTiles[waterId].GetComponent<Water>().Frozen();

        isFreezing = freezing;
    }

    public void UnfreezeWater(int waterId, bool unfreezing)
    {
        waterTileUnfreezeLeft = waterId - 1;
        waterTileUnfreezeRight = waterId + 1;

        waterTiles[waterId].GetComponent<Water>().Unfrozen();

        isUnfreezing = unfreezing;
    }
}
