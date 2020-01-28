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
    public TileBase roofTile;
    public TileBase aboveTile, leftTile, rightTile;
    public Tilemap waterMap;
    public Tilemap groundMap;

    private int tileSwitch = 0;
    private int tileCounter = 1;

    [SerializeField]
    private LayerMask typeOfGround;

    private Vector3Int previous;

    private Vector3Int currentCell;
    private Vector3Int aboveCell;
    private Vector3Int leftCell;
    private Vector3Int rightCell;

    public Vector3Int hangFromCell;
    public bool hangFromCellSet;

    private void Update()
    {
        if (aboveTile == roofTile && !hangFromCellSet)
        {
            hangFromCell = aboveCell;
            hangFromCell.y -= 1;
            Debug.Log("HangFromCellSet");
            hangFromCellSet = true;
        }

        if ((Input.GetKey("joystick " + GetComponent<PlayerMovementTest>().playerId + " button 5") || Input.GetKey("joystick " + GetComponent<PlayerMovementTest>().playerId + " button 4")) && hangFromCellSet)
        {
            if (hangFromCell != default)
            {
                transform.position = new Vector3(hangFromCell.x, hangFromCell.y, transform.position.z);
                GetComponent<PlayerMovementTest>().theRB.velocity = Vector2.zero;
            }
        }
        else
        {
            hangFromCell = default;
            hangFromCellSet = false;
        }
    }

    // do late so that the player has a chance to move in update if necessary
    private void LateUpdate()
    {
        // get current grid location
        currentCell = waterMap.WorldToCell(transform.position);
        // add one in a direction (you'll have to change this to match your directional control)
        currentCell.y -= 1;

        aboveCell = groundMap.WorldToCell(transform.position);

        aboveCell.y += 1;

        leftCell = waterMap.WorldToCell(transform.position);

        leftCell.x -= 1;

        rightCell = waterMap.WorldToCell(transform.position);

        rightCell.x += 1;

        // if the position has changed
        if (currentCell != previous)
        {
            // set the new tile
            //highlightMap.SetTile(currentCell, highlightTile);

            if (GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                standingOnTile = waterMap.GetTile(currentCell);
                leftTile = waterMap.GetTile(leftCell);
                rightTile = waterMap.GetTile(rightCell);
                aboveTile = groundMap.GetTile(aboveCell);

                if (standingOnTile == waterTile)
                {
                    StartCoroutine(FreezeWater(currentCell));
                    waterMap.SetTile(currentCell, frozenWaterTile);
                    waterMap.gameObject.layer = default;
                }
            }

            if (GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                standingOnTile = waterMap.GetTile(currentCell);
                leftTile = waterMap.GetTile(leftCell);
                rightTile = waterMap.GetTile(rightCell);
                aboveTile = groundMap.GetTile(aboveCell);

                if (waterMap.GetTile(previous) == frozenWaterTile)
                {
                    StartCoroutine(UnfreezeWater(previous));
                    waterMap.SetTile(previous, waterTile);
                }

                if (standingOnTile == frozenWaterTile)
                {
                    waterMap.gameObject.layer = default;
                }
                //else if (standingOnTile == waterTile)
                //{
                //    highlightMap.gameObject.layer = 10;
                //}
                else if (standingOnTile == waterTile)
                {
                    waterMap.gameObject.layer = 10;
                }

                if (leftTile == frozenWaterTile || leftTile == frozenWaterEndTile)
                {
                    waterMap.gameObject.layer = default;
                }
                else if (leftTile == waterTile || leftTile == waterEndTile)
                {
                    waterMap.gameObject.layer = 10;
                }

                if (rightTile == frozenWaterTile || rightTile == frozenWaterEndTile)
                {
                    waterMap.gameObject.layer = default;
                }
                else if (rightTile == waterTile || rightTile == waterEndTile)
                {
                    waterMap.gameObject.layer = 10;
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
        if (waterMap.GetTile(tilePos) == waterTile)
        {
            waterMap.SetTile(tilePos, frozenWaterTile);
            StartCoroutine(FreezeWater(tilePos));
            Debug.Log("Renewed!");
        }

        if (waterMap.GetTile(tilePos) == waterEndTile)
        {
            waterMap.SetTile(tilePos, frozenWaterEndTile);
        }
    }

    private IEnumerator UnfreezeWater(Vector3Int tilePos)
    {
        tilePos.y -= 1;
        yield return new WaitForSeconds(waterFreezeSpeed);
        if (waterMap.GetTile(tilePos) == frozenWaterTile)
        {
            waterMap.SetTile(tilePos, waterTile);
            StartCoroutine(UnfreezeWater(tilePos));
        }

        if (waterMap.GetTile(tilePos) == frozenWaterEndTile)
        {
            waterMap.SetTile(tilePos, waterEndTile);
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
