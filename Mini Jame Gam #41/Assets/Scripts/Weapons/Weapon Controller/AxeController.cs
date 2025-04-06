using UnityEngine;

public class AxeController : WeaponController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedAxe = Instantiate(weaponData.Prefab);
        spawnedAxe.transform.position = transform.position;
        spawnedAxe.transform.parent = transform;
        spawnedAxe.GetComponent<AxeBehaviour>().DirectionChecker(pm.lastMovedVector);
    }
}
