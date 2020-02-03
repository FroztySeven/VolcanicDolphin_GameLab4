using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightInFront : MonoBehaviour
{
    public float waterFreezeSpeed = 0.5f;
    public int ladderGrowAmount = 10;

    public TileBase frozenWaterTile;
    public TileBase frozenWaterEndTile;
    public TileBase frozenMovingWater;
    public TileBase waterTile;
    public TileBase waterEndTile;
    public TileBase movingWater;
    public TileBase standingOnTile;
    public TileBase roofTile;
    public TileBase leftTile, rightTile, groundAboveTile, groundBelowTile, groundLeftTile, groundRightTile;
    public TileBase ladderBottom, ladderMiddle, ladderTop;
    public TileBase frozenLadderBottom, frozenLadderMiddle, frozenLadderTop;
    public Tilemap waterMap;
    public Tilemap groundMap;

    private int tileSwitch = 0;
    private int tileCounter = 1;

    [SerializeField]
    private LayerMask typeOfGround;

    private Vector3Int previous;

    private Vector3Int currentCell;
    private Vector3Int leftCell;
    private Vector3Int rightCell;

    private Vector3Int groundAboveCell;
    private Vector3Int groundBelowCell;
    private Vector3Int groundLeftCell;
    private Vector3Int groundRightCell;

    public Vector3Int hangFromCell;
    public bool hangFromCellSet;
    public bool growingLadder = false;

    private void Awake()
    {
        groundMap = GameObject.Find("TM_Ground").GetComponent<Tilemap>();
        waterMap = GameObject.Find("TM_Water").GetComponent<Tilemap>();
    }

    private void Update()
    {
        if (groundAboveTile == roofTile && !hangFromCellSet)
        {
            hangFromCell = groundAboveCell;
            hangFromCell.y -= 1;
            Debug.Log("HangFromCellSet");
            hangFromCellSet = true;
        }

        if ((Input.GetKey("joystick " + GetComponent<PlayerMovementTest>().playerId + " button 5") || Input.GetKey("joystick " + GetComponent<PlayerMovementTest>().playerId + " button 4")) && hangFromCellSet)
        {
            if (hangFromCell != default)
            {
                transform.position = new Vector3(hangFromCell.x + 0.5f, hangFromCell.y + 0.5f, transform.position.z);
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

        leftCell = waterMap.WorldToCell(transform.position);

        leftCell.x -= 1;

        rightCell = waterMap.WorldToCell(transform.position);

        rightCell.x += 1;

        groundAboveCell = groundMap.WorldToCell(transform.position);

        groundAboveCell.y += 1;

        groundBelowCell = groundMap.WorldToCell(transform.position);

        groundBelowCell.y -= 1;

        groundLeftCell = groundMap.WorldToCell(transform.position);

        groundLeftCell.x -= 1;

        groundRightCell = groundMap.WorldToCell(transform.position);

        groundRightCell.x += 1;


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
                groundAboveTile = groundMap.GetTile(groundAboveCell);
                groundBelowTile = groundMap.GetTile(groundBelowCell);
                groundLeftTile = groundMap.GetTile(groundLeftCell);
                groundRightTile = groundMap.GetTile(groundRightCell);

                if (standingOnTile == waterTile)
                {
                    StartCoroutine(FreezeWater(currentCell, waterTile, frozenWaterTile));
                    waterMap.SetTile(currentCell, null);
                    waterMap.SetTile(currentCell, frozenWaterTile);
                    waterMap.gameObject.layer = default;
                }

                if (standingOnTile == movingWater)
                {
                    StartCoroutine(FreezeWater(currentCell, movingWater, frozenMovingWater));
                    waterMap.SetTile(currentCell, null);
                    waterMap.SetTile(currentCell, frozenMovingWater);
                    waterMap.gameObject.layer = default;
                }

                if (groundMap.GetTile(previous) == ladderMiddle)
                {                   
                    groundMap.SetTile(previous, null);
                    groundMap.SetTile(previous, frozenLadderMiddle);
                }

                if (groundMap.GetTile(previous) == ladderBottom)
                {
                    groundMap.SetTile(previous, null);
                    groundMap.SetTile(previous, frozenLadderBottom);
                }

                if (groundMap.GetTile(previous) == ladderTop)
                {
                    groundMap.SetTile(previous, null);
                    groundMap.SetTile(previous, frozenLadderTop);
                }

                if (groundAboveTile == ladderMiddle || groundAboveTile == ladderTop)
                {
                    GetComponent<PlayerMovementTest>().isOnLadder = true;
                }
                else
                {
                    GetComponent<PlayerMovementTest>().isOnLadder = false;
                }
            }

            if (GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                standingOnTile = waterMap.GetTile(currentCell);
                leftTile = waterMap.GetTile(leftCell);
                rightTile = waterMap.GetTile(rightCell);
                groundAboveTile = groundMap.GetTile(groundAboveCell);
                groundBelowTile = groundMap.GetTile(groundBelowCell);
                groundLeftTile = groundMap.GetTile(groundLeftCell);
                groundRightTile = groundMap.GetTile(groundRightCell);

                if (waterMap.GetTile(previous) == frozenWaterTile)
                {
                    StartCoroutine(UnfreezeWater(previous, frozenWaterTile, waterTile));
                    waterMap.SetTile(previous, null);
                    waterMap.SetTile(previous, waterTile);
                }

                if (waterMap.GetTile(previous) == frozenMovingWater)
                {
                    StartCoroutine(UnfreezeWater(previous, frozenMovingWater, movingWater));
                    waterMap.SetTile(previous, null);
                    waterMap.SetTile(previous, movingWater);
                }

                if (standingOnTile == frozenWaterTile || standingOnTile == frozenMovingWater)
                {
                    waterMap.gameObject.layer = default;
                }
                //else if (standingOnTile == waterTile)
                //{
                //    highlightMap.gameObject.layer = 10;
                //}
                else if (standingOnTile == waterTile || standingOnTile == movingWater)
                {
                    waterMap.gameObject.layer = 10;
                }

                if (leftTile == frozenWaterTile || leftTile == frozenWaterEndTile || leftTile == frozenMovingWater)
                {
                    waterMap.gameObject.layer = default;
                }
                else if (leftTile == waterTile || leftTile == waterEndTile || leftTile == movingWater)
                {
                    waterMap.gameObject.layer = 10;
                }

                if (rightTile == frozenWaterTile || rightTile == frozenWaterEndTile || rightTile == frozenMovingWater)
                {
                    waterMap.gameObject.layer = default;
                }
                else if (rightTile == waterTile || rightTile == waterEndTile || rightTile == movingWater)
                {
                    waterMap.gameObject.layer = 10;
                }

                if (groundLeftTile == ladderBottom && !growingLadder)
                {
                    StartCoroutine(GrowLadder(groundLeftCell, 0));
                    growingLadder = true;
                }

                if (groundRightTile == ladderBottom && !growingLadder)
                {
                    StartCoroutine(GrowLadder(groundRightCell, 0));
                    growingLadder = true;
                }

                if (groundAboveTile == frozenLadderTop || groundAboveTile == frozenLadderMiddle)
                {
                    StartCoroutine(UnfreezeLadder(groundAboveCell));
                }

                if (groundBelowTile == frozenLadderTop)
                {
                    StartCoroutine(UnfreezeLadder(groundBelowCell));
                }

                if (groundLeftTile == frozenLadderMiddle)
                {
                    StartCoroutine(UnfreezeLadder(groundLeftCell));
                }

                if (groundRightTile == frozenLadderMiddle)
                {
                    StartCoroutine(UnfreezeLadder(groundRightCell));
                }

                if (groundLeftTile == frozenLadderBottom)
                {
                    StartCoroutine(UnfreezeLadder(groundLeftCell));
                }

                if (groundRightTile == frozenLadderBottom)
                {
                    StartCoroutine(UnfreezeLadder(groundRightCell));
                }

                if (groundAboveTile == ladderMiddle || groundAboveTile == ladderTop)
                {
                    GetComponent<PlayerMovementTest>().isOnLadder = true;
                }
                else
                {
                    GetComponent<PlayerMovementTest>().isOnLadder = false;
                }
            }

            // erase previous
            //highlightMap.SetTile(previous, null);

            // save the new position for next frame
            previous = currentCell;
        }
    }

    private IEnumerator FreezeWater(Vector3Int tilePos, TileBase currentTile, TileBase currentTileFrozen)
    {
        tilePos.y -= 1;
        yield return new WaitForSeconds(waterFreezeSpeed);
        if (waterMap.GetTile(tilePos) == currentTile)
        {
            waterMap.SetTile(tilePos, null);
            waterMap.SetTile(tilePos, currentTileFrozen);
            StartCoroutine(FreezeWater(tilePos, currentTile, currentTileFrozen));
        }

        if (waterMap.GetTile(tilePos) == waterEndTile)
        {
            waterMap.SetTile(tilePos, null);
            waterMap.SetTile(tilePos, frozenWaterEndTile);
        }
    }

    private IEnumerator UnfreezeWater(Vector3Int tilePos, TileBase currentTileFrozen, TileBase currentTile)
    {
        tilePos.y -= 1;
        yield return new WaitForSeconds(waterFreezeSpeed);
        if (waterMap.GetTile(tilePos) == currentTileFrozen)
        {
            waterMap.SetTile(tilePos, null);
            waterMap.SetTile(tilePos, currentTile);
            StartCoroutine(UnfreezeWater(tilePos, currentTileFrozen, currentTile));
        }

        if (waterMap.GetTile(tilePos) == frozenWaterEndTile)
        {
            waterMap.SetTile(tilePos, null);
            waterMap.SetTile(tilePos, waterEndTile);
        }
    }

    private IEnumerator GrowLadder(Vector3Int tilePos, int growAmount)
    {
        growAmount++;
        tilePos.y += 1;
        yield return new WaitForSeconds(0.1f);
        if (groundMap.GetTile(tilePos) != ladderMiddle && growAmount < ladderGrowAmount)
        {
            groundMap.SetTile(tilePos, null);
            groundMap.SetTile(tilePos, ladderMiddle);
            StartCoroutine(GrowLadder(tilePos, growAmount));
        }
        else
        {
            groundMap.SetTile(tilePos, null);
            groundMap.SetTile(tilePos, ladderTop);
        }
    }

    private IEnumerator UnfreezeLadder(Vector3Int tilePos)
    {        
        yield return new WaitForSeconds(0.5f);
        if (groundMap.GetTile(tilePos) == frozenLadderBottom)
        {
            groundMap.SetTile(tilePos, null);
            groundMap.SetTile(tilePos, ladderBottom);
            tilePos.y += 1;
            StartCoroutine(UnfreezeLadder(tilePos));
        }
        else if (groundMap.GetTile(tilePos) == frozenLadderMiddle)
        {
            groundMap.SetTile(tilePos, null);
            groundMap.SetTile(tilePos, ladderMiddle);
            tilePos.y += 1;
            StartCoroutine(UnfreezeLadder(tilePos));
            tilePos.y -= 2;
            StartCoroutine(UnfreezeLadder(tilePos));
        }
        else if (groundMap.GetTile(tilePos) == frozenLadderTop)
        {
            groundMap.SetTile(tilePos, null);
            groundMap.SetTile(tilePos, ladderTop);
            tilePos.y -= 1;
            StartCoroutine(UnfreezeLadder(tilePos));
        }
        //else
        //{
        //    groundMap.SetTile(tilePos, null);
        //    groundMap.SetTile(tilePos, ladderTop);
        //}
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
