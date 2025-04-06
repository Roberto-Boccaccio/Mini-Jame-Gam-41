using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy") && !markedEnemies.Contains(other.gameObject))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);

            markedEnemies.Add(other.gameObject);
        }
        else if(other.CompareTag("Prop") && !markedEnemies.Contains(other.gameObject))
        {
            if(other.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
                markedEnemies.Add(other.gameObject);
            }
        }
    }
}
