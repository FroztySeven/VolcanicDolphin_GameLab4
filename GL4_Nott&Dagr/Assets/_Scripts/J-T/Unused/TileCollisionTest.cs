using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCollisionTest : MonoBehaviour
{
    public TileBase waterTile, foamTile, newWaterTile, newFoamTile;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Tilemap>().SwapTile(waterTile, newWaterTile);
            gameObject.GetComponent<Tilemap>().SwapTile(foamTile, newFoamTile);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Tilemap>().SwapTile(newWaterTile, waterTile);
            gameObject.GetComponent<Tilemap>().SwapTile(newFoamTile, foamTile);
        }
    }
}
