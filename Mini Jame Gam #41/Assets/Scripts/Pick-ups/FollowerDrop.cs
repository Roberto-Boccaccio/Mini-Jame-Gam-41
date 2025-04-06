using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowerDrop : Pickup, ICollectible
{
    public void Collect()
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        player.SpawnFollower();
    }
}
