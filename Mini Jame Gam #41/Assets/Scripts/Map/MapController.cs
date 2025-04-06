using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerMovement pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist; //Must be greater than the length and width of the tilemap
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if(!currentChunk)
        {
            return;
        }

        if(pm.moveDir.x > 0 && pm.moveDir.y == 0)   //right
        {
            FindChunk("Right");
            FindChunk("Right Up");
            FindChunk("Right Down");
        }
        else if(pm.moveDir.x < 0 && pm.moveDir.y == 0)  //left
        {
            FindChunk("Left");
            FindChunk("Left Up");
            FindChunk("Left Down");
        }
        else if(pm.moveDir.x == 0 && pm.moveDir.y > 0)  //up
        {
            FindChunk("Up");
            FindChunk("Right Up");
            FindChunk("Left Up");
        }
        else if(pm.moveDir.x == 0 && pm.moveDir.y < 0)  //down
        {
            FindChunk("Down");
            FindChunk("Right Down");
            FindChunk("Left Down");
        }
        else if(pm.moveDir.x > 0 && pm.moveDir.y > 0)  //right up
        {
            FindChunk("Right Up");
            FindChunk("Right");
            FindChunk("Up");
        }
        else if(pm.moveDir.x > 0 && pm.moveDir.y < 0)  //right down
        {
            FindChunk("Right Down");
            FindChunk("Right");
            FindChunk("Down");
        }
        else if(pm.moveDir.x < 0 && pm.moveDir.y > 0)  //left up
        {
            FindChunk("Left Up");
            FindChunk("Left");
            FindChunk("Up");
        }
        else if(pm.moveDir.x < 0 && pm.moveDir.y < 0)  //left down
        {
            FindChunk("Left Down");
            FindChunk("Left");
            FindChunk("Down");
        }
    }
    void FindChunk(string dir)
    {
        if(!Physics2D.OverlapCircle(currentChunk.transform.Find(dir).position, checkerRadius, terrainMask))
        {
            noTerrainPosition = currentChunk.transform.Find(dir).position;
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if(optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }
        
        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
