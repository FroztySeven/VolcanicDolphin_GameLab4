using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightInFront : MonoBehaviour
{
    public float waterFreezeSpeed = 0.5f;

    public TileBase frozenWaterTile;
    public TileBase frozenWaterEndTile;
    public TileBase waterTile;
    public TileBase waterEndTile;
    public TileBase standingOnTile;
    public TileBase leftTile, rightTile;
    public Tilemap highlightMap;

    private int tileSwitch = 0;
    private int tileCounter = 1;

    [SerializeField]
    private LayerMask typeOfGround;

    private Vector3Int previous;

    private Vector3Int currentCell;
    private Vector3Int leftCell;
    private Vector3Int rightCell;

    // do late so that the player has a chance to move in update if necessary
    private void LateUpdate()
    {
        // get current grid location
        currentCell = highlightMap.WorldToCell(transform.position);
        // add one in a direction (you'll have to change this to match your directional control)
        currentCell.y -= 1;

        leftCell = highlightMap.WorldToCell(transform.position);

        leftCell.x -= 1;

        rightCell = highlightMap.WorldToCell(transform.position);

        rightCell.x += 1;

        // if the position has changed
        if (currentCell != previous)
        {
            // set the new tile
            //highlightMap.SetTile(currentCell, highlightTile);

            if (GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                standingOnTile = highlightMap.GetTile(currentCell);
                leftTile = highlightMap.GetTile(leftCell);
                rightTile = highlightMap.GetTile(rightCell);

                if (standingOnTile == waterTile)
                {
                    StartCoroutine(FreezeWater(currentCell));
                    highlightMap.SetTile(currentCell, frozenWaterTile);
                    highlightMap.gameObject.layer = default;
                }

            }

            if (GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                standingOnTile = highlightMap.GetTile(currentCell);
                leftTile = highlightMap.GetTile(leftCell);
                rightTile = highlightMap.GetTile(rightCell);

                if (highlightMap.GetTile(previous) == frozenWaterTile)
                {
                    StartCoroutine(UnfreezeWater(previous));
                    highlightMap.SetTile(previous, waterTile);
                }

                if (standingOnTile == frozenWaterTile)
                {
                    highlightMap.gameObject.layer = default;
                }
                //else if (standingOnTile == waterTile)
                //{
                //    highlightMap.gameObject.layer = 10;
                //}
                else if (standingOnTile == waterTile)
                {
                    highlightMap.gameObject.layer = 10;
                }

                if (leftTile == frozenWaterTile || leftTile == frozenWaterEndTile)
                {
                    highlightMap.gameObject.layer = default;
                }
                else if (leftTile == waterTile || leftTile == waterEndTile)
                {
                    highlightMap.gameObject.layer = 10;
                }

                if (rightTile == frozenWaterTile || rightTile == frozenWaterEndTile)
                {
                    highlightMap.gameObject.layer = default;
                }
                else if (rightTile == waterTile || rightTile == waterEndTile)
                {
                    highlightMap.gameObject.layer = 10;
                }
            }

            // erase previous
            //highlightMap.SetTile(previous, null);

            // save the new position for next frame
            previous = currentCell;
        }
    }

    private IEnumerator FreezeWater(Vector3Int tilePos)
    {
        Debug.Log("IEnumerator Activated");
        tilePos.y -= 1;
        yield return new WaitForSeconds(waterFreezeSpeed);
        if (highlightMap.GetTile(tilePos) == waterTile)
        {
            highlightMap.SetTile(tilePos, frozenWaterTile);
            StartCoroutine(FreezeWater(tilePos));
            Debug.Log("Renewed!");
        }

        if (highlightMap.GetTile(tilePos) == waterEndTile)
        {
            highlightMap.SetTile(tilePos, frozenWaterEndTile);
        }
    }

    private IEnumerator UnfreezeWater(Vector3Int tilePos)
    {
        tilePos.y -= 1;
        yield return new WaitForSeconds(waterFreezeSpeed);
        if (highlightMap.GetTile(tilePos) == frozenWaterTile)
        {
            highlightMap.SetTile(tilePos, waterTile);
            StartCoroutine(UnfreezeWater(tilePos));
        }

        if (highlightMap.GetTile(tilePos) == frozenWaterEndTile)
        {
            highlightMap.SetTile(tilePos, waterEndTile);
        }
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Water")
    //    {
    //        highlightMap = other.gameObject.GetComponent<Tilemap>();

    //        if (GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
    //        {
    //            standingOnTile = highlightMap.GetTile(currentCell);

    //            if (standingOnTile == waterTile)
    //            {
    //                StartCoroutine(FreezeWater(currentCell));
    //                highlightMap.SetTile(currentCell, frozenWaterTile);
    //                highlightMap.gameObject.layer = default;
    //            }

    //        }
    //    }
    //}
}
