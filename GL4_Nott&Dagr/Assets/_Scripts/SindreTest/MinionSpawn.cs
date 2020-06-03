using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MinionSpawn : MonoBehaviour
{
    public GameObject minionPrefab;
    public Sprite nottMinionSprite, dagrMinionSprite;
    public float timeToCompleteRoute;
    public bool alwaysAliveAfterSpawned = false, canCollideWithPlayer = false;
    public int timeAlive;

    [Header("Path number, needs to be added in the ITweenPath script")]
    public int pathNumber;
    
    private iTweenPath[] paths;
    private iTweenPath firstPath;

    private GameObject minionToMove;

    private GameObject nott, dagr;
    private GameObject dagrMinionClone, nottMinionClone;
    private GameObject originalParent;

    private bool dagrMinionIsSpawned = false, nottMinionIsSpawned = false;
    private bool minionOnRoute = false;

    private static MinionSpawn instance;
    private bool disableSpawning;

    private Animator n_Anim, d_Anim;
    [SerializeField]
    private RuntimeAnimatorController n_AC, d_AC;
    private bool isIdle = true;

    private void Start()
    {
        dagr = GameObject.Find("Player1");
        nott = GameObject.Find("Player2");
        
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            disableSpawning = true;
        }

        gameObject.GetComponent<iTweenPath>().nodes[0] = transform.position; //set start node to the same position as the gameobject

        if (canCollideWithPlayer)
        {
            minionPrefab.layer = 0;
        }
        else
        {
            minionPrefab.layer = 20;
        }
    }


    private void Update()
    {
        if (!disableSpawning)
        {
            if (Input.GetButtonDown("MinionSpawnP" + dagr.GetComponent<PlayerController>().playerId) &&
                !dagrMinionIsSpawned) //Spawns Dagr's minion
            {
                dagrMinionIsSpawned = true;
                dagrMinionClone = Instantiate(minionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                dagrMinionClone.transform.SetParent(dagr.transform);
                dagrMinionClone.transform.localPosition = new Vector3(0, 2, 0);
                originalParent = dagr;
                dagrMinionClone.GetComponent<SpriteRenderer>().sprite = dagrMinionSprite;
                d_Anim = dagrMinionClone.GetComponent<Animator>();
                dagrMinionClone.GetComponent<Animator>().runtimeAnimatorController = d_AC;
                if (!alwaysAliveAfterSpawned)
                {
                    StartCoroutine(despawnDagrMinion());
                }
            }

            if (Input.GetButtonDown("MinionSpawnP" + nott.GetComponent<PlayerController>().playerId) &&
                !nottMinionIsSpawned) //Spawns Nott's minion
            {
                nottMinionIsSpawned = true;
                nottMinionClone = Instantiate(minionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                nottMinionClone.transform.SetParent(nott.transform);
                nottMinionClone.transform.localPosition = new Vector3(0, 2, 0);
                originalParent = nott;
                nottMinionClone.GetComponent<SpriteRenderer>().sprite = nottMinionSprite;
                n_Anim = nottMinionClone.GetComponent<Animator>();
                nottMinionClone.GetComponent<Animator>().runtimeAnimatorController = n_AC;
                if (!alwaysAliveAfterSpawned)
                {
                    StartCoroutine(despawnNottMinion());
                }
            }
        }

        if (d_Anim != null)
        {
            d_Anim.SetBool("DayIdle", isIdle);
        }
        if (n_Anim != null)
        {
            n_Anim.SetBool("NightIdle", isIdle);
        }

        if (minionToMove != null && !isIdle)
        {
            if (minionToMove.transform.position == iTween.PointOnPath(iTweenPath.GetPath("MinionPath" + pathNumber), 1))
            {
                isIdle = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Minion"))
        {
            minionOnRoute = true;
            minionToMove = other.gameObject;
            other.gameObject.transform.SetParent(null);
            minionToMove.GetComponent<MinionIdleMovement>().onPath = true;
            PathOne();
            isIdle = false;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            originalParent = other.gameObject;
        }
    }
    
    
    private void PathOne()
    {
        iTween.MoveTo(minionToMove, iTween.Hash("path", iTweenPath.GetPath("MinionPath" + pathNumber), "time", timeToCompleteRoute, "easetype", iTween.EaseType.linear, "oncomplete", "PathOne"));

        if (alwaysAliveAfterSpawned)
        {
            StartCoroutine(parentMinion());
        }
    }


    private IEnumerator parentMinion() //parents minion if they are always alive after they are spawned
    {
        yield return new WaitForSeconds(timeAlive);
        minionToMove.transform.SetParent(originalParent.transform);
        minionToMove.transform.localPosition = new Vector3(0,2,0);
        minionOnRoute = false;
        minionToMove.GetComponent<MinionIdleMovement>().onPath = false;
    }
    
    
    private IEnumerator despawnDagrMinion() //despawns dagrs minion
    {
        yield return new WaitForSeconds(timeAlive);
        dagrMinionClone.GetComponent<MinionIdleMovement>().onPath = false;
        Destroy(dagrMinionClone);
        dagrMinionIsSpawned = false;
        minionOnRoute = false;
    }
    private IEnumerator despawnNottMinion() //despawns notts minion
    {
        yield return new WaitForSeconds(timeAlive);
        nottMinionClone.GetComponent<MinionIdleMovement>().onPath = false;
        Destroy(nottMinionClone);
        nottMinionIsSpawned = false;
        minionOnRoute = false;
    }
}
