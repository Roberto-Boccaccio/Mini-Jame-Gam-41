using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapController mc;
    public GameObject targetMap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mc = FindAnyObjectByType<MapController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            mc.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(mc.currentChunk == targetMap)
            {
                mc.currentChunk = null;
            }
        }
    }
}
