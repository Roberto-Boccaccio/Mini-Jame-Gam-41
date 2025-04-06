using UnityEngine;

public class ExperienceAcorn : Pickup, ICollectible
{
    public int experienceGranted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Collect()
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        //player.IncreaseExperience(experienceGranted);
        player.currentPoints++;
    }
}
