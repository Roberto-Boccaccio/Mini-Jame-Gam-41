using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStats : MonoBehaviour
{
    CharacterScriptableObject characterData;

    public List<GameObject> spawnedWeapons;
    public List<Sprite> sprites;
    SpriteRenderer sr;

    void Awake()
    {
        characterData = CharacterSelector.GetData();

        SpawnWeapon(characterData.MinionStartingWeapon);
        sr = GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        sr.sprite = sprites[UnityEngine.Random.Range(0, sprites.Count)];
    }

    void Update()
    {

    }


    public void SpawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        spawnedWeapons.Add(spawnedWeapon);
    }
}
