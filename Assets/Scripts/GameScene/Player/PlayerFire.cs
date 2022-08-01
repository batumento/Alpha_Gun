using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{ 
    [Header("Script References")]
    public FireController fireController;
    public WeaponController weaponController;
    public MermiController _mermiController;
    public AmmoUp ammoUp;
    [SerializeField] PlayerController playerController;
    public HealthEnemy healthEnemy;
    public FOV fieldOfView;
    [Header("Variable")]
    public bool isEnemy;
    public bool isTrigged;
    public bool forWork;
    public bool isReload;
    public float rate;
    public int currentEnemyPoint;
    public Transform targetE;
    public List<Transform> visibleTargets = new List<Transform>();

    public int currentIndex;

    bool allowFire = true;
    [Header("FireSistem")]
    public Transform currentEnemy;
    [SerializeField] float distanceToCloset;
    [SerializeField] HealthEnemy currenEnemyHealth;

    bool inFight;
    private void Awake()
    {
        healthEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<HealthEnemy>();
        _mermiController = GetComponent<MermiController>();
        playerController = GetComponent<PlayerController>();
        fireController = GameObject.Find("CanvasUI").GetComponent<FireController>();
        weaponController = GetComponent <WeaponController>();
        fieldOfView = GetComponent<FOV>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AmmoUp")
        {
            Destroy(other.gameObject);
            isTrigged = true;
        }
    }
    void Start()
    {
        isReload = true;
        forWork = false;
        for (int i =0; i<weaponController._weapons.Length; i++)
        {
            weaponController._weapons[i].Intialize();
        }
    }

    void Update()
    {

        if (currentEnemy != null && fireController.currentWeapon != null)
        {
            float distancePlayer = Vector3.Distance(transform.position, currentEnemy.position);
            distanceToCloset = distancePlayer;
        }
        FindClosestEnemy();

        if (playerController.isShooting && (allowFire) && fireController.currentWeapon != null && currentEnemy != null && currenEnemyHealth!=null && !GetComponent<HealthPlayer>().playerDead)
        {
            Fire();
        }
        if (inFight) return;

            weaponController.Reload(fireController.currentIndex);
            fireController.UpdateWeaponButtons();
    }
    void Fire()
    {
        if (currenEnemyHealth.isDead) return;
        if (distanceToCloset > fireController.weapons[fireController.currentIndex].fireRate) return;
        Vector3 enemyPosition = new Vector3(currentEnemy.position.x, transform.position.y, currentEnemy.position.z);
        transform.LookAt(enemyPosition);
        StartCoroutine(AllowFire(fireController.currentIndex));
        weaponController.FireBullet(fireController.currentIndex);
        fireController.UpdateWeaponButtons();
        _mermiController.ShootBullet();
    }

    IEnumerator AllowFire(int ID)
    {
        float rateFire = weaponController._weapons[ID].reloadTime;
        rateFire -= RateUp();
        allowFire = false;
        yield return new WaitForSeconds(rateFire);
        allowFire = true;
    }
    public float RateUp()
    {
        if (this.isTrigged)
        {
            rate = 0.4f;
            StartCoroutine(nameof(RateSecond));
        }
        else
        {
            rate = 0;
        }
        return rate;
    }
    public IEnumerator RateSecond()
    {
        yield return new WaitForSeconds(3);
        isTrigged = false;
    }

    void FindClosestEnemy()
    {
        GetClosestEnemy(GameObject.FindObjectsOfType<HealthEnemy>());
    }

    void GetClosestEnemy(HealthEnemy[] enemies)
    {
        HealthEnemy closestEnemy = null;
        float minDistance = fireController.weapons[fireController.currentIndex].fireRate;
        foreach(HealthEnemy enemy in enemies)
        {
            if (enemy.isDead == false)
            {
                float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
                if (distanceToEnemy < minDistance)
                {
                    inFight = true;
                    closestEnemy = enemy;
                    distanceToCloset = distanceToEnemy;
                    currenEnemyHealth = closestEnemy;
                    currentEnemy = closestEnemy.transform;
                    minDistance = distanceToCloset;
                }
                else
                {
                    inFight = false;
                }
            }
        }
    }

   /* bool FindVisibleTargets()
    {
        forWork = false;
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, fieldOfView.viewRadius, fieldOfView.targetMask);
        if (targetsInViewRadius.Length != 0)
        {
            targetE = targetsInViewRadius[0].transform;
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                forWork = true;
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if ((Vector3.Angle(transform.forward, dirToTarget) < fieldOfView.viewAngle / 2) && healthEnemy.EnemyisDead())
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, fieldOfView.obstacleMask))
                    {
                        Debug.Log("Enemy Hedef Alındı");
                        visibleTargets.Add(target);
                        return isEnemy = true;
                    }
                }
            }
        }
        if (isEnemy && forWork)
        {
            return isEnemy;
        }
        else
        {
            return isEnemy = false;
        }
        
    }*/
}
