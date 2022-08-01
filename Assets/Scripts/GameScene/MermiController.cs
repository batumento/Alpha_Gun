using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiController : MonoBehaviour
{
    public WeaponController weaponController;
    public FireController fireController;
    public PlayerFire playerFire;
    Transform spawnAmmo;
    public GameObject bullet;
    public float speed = 5f;

    private void Awake()
    {
        playerFire = GameObject.Find("Player").GetComponent<PlayerFire>();
        weaponController = GameObject.Find("Player").GetComponent<WeaponController>();
        fireController = GameObject.Find("CanvasUI").GetComponent<FireController>();
    }
    public void ShootBullet()
    {
        bullet = weaponController._weapons[fireController.currentIndex].mermiPrefab;
        GameObject cB = Instantiate(bullet, fireController.ammoSpawn.position, Quaternion.LookRotation(playerFire.currentEnemy.position));
    }
}
