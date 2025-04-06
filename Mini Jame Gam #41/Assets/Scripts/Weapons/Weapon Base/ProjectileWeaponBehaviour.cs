using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;


    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public virtual void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if(dirx < 0 && diry == 0)   //left
        {
            rotation.z = 180f;
        }
        else if(dirx == 0 && diry > 0)  //up
        {
            rotation.z = 90f;
        }
        else if(dirx == 0 && diry < 0)  //down
        {
            rotation.z = -90f;
        }
        else if(dirx > 0 && diry > 0)  //right up
        {
            rotation.z = 45f;
        }
        else if(dirx > 0 && diry < 0)  //right down
        {
            rotation.z = -45f;
        }
        else if(dirx < 0 && diry > 0)  //left up
        {
            rotation.z = 135f;
        }
        else if(dirx < 0 && diry < 0)  //left down
        {
            rotation.z = -135f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            ReducePierce();
        }
        else if(other.CompareTag("Prop"))
        {
            if(other.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
                ReducePierce();
            }
        }
    }

    void ReducePierce()
    {
        currentPierce--;
        if(currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
