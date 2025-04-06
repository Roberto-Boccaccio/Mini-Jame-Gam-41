using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;

    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;
    [HideInInspector]
    public int currentPoints;
    [HideInInspector]
    public float currentHighScore;

    public List<GameObject> spawnedWeapons;
    public static bool playerDied = false;

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public int maxFollowers;
    int followerCount;
    public GameObject follower;
    public List<GameObject> followerSpawnPoints;

    public List<LevelRange> levelRanges;

    void Awake()
    {
        characterData = CharacterSelector.GetData();

        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;
        characterData.Points = 0;
        currentPoints = characterData.Points;
        currentHighScore = characterData.HighScore;

        SpawnWeapon(characterData.StartingWeapon);
        playerDied = false;
    }

    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if(isInvincible)
        {
            isInvincible = false;
        }

        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if(experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }

            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float dmg)
    {
        if(!isInvincible)
        {
            currentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            
            if(currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        playerDied = true;
        if(currentHighScore < currentPoints)
        {
            characterData.HighScore = currentPoints;
        }
        characterData.Points = currentPoints;
        SceneController sc = FindAnyObjectByType<SceneController>();
        sc.SceneChange("Game Over");
    }

    public void SpawnFollower()
    {
        if(followerCount < maxFollowers)
        {
            GameObject fol = Instantiate(follower, followerSpawnPoints[followerCount].transform.position, Quaternion.identity);
            fol.transform.parent = followerSpawnPoints[followerCount].transform;
            followerCount++;
        }
    }

    void Recover()
    {
        if(currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;

            if(currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        spawnedWeapons.Add(spawnedWeapon);
    }
}
