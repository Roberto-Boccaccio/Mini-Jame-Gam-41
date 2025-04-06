using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public EnemyScriptableObject enemyData;

    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;


    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {

        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        if(PlayerStats.playerDied)
        {
            return;
        }
        Debug.Log("EnemyKilled");
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        es.OnEnemyKilled();

        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        player.currentPoints++;
    }
}
