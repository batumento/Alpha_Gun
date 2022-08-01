using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Animations.Rigging;

public class EnemyFire : MonoBehaviour
{
    public FOVEnemy fieldOfViewEnemy;
    
    //[SerializeField] private TwoBoneIKConstraint rightIK;
    //[SerializeField] private TwoBoneIKConstraint leftIK;

    [SerializeField] HealthPlayer currentPlayerHealth;

    [SerializeField] float distanceToCloset;
    [SerializeField] Transform currentPlayer;

    public bool allowFire;
    public GameObject player;

    [Header("Ammo Spawn")]
    public Transform spawnPoint;
    public GameObject bullet;
    public float speed = 5f;
    private void Awake()
    {
        fieldOfViewEnemy = GetComponent<FOVEnemy>();
        player = GameObject.Find("Player");
        currentPlayerHealth = GameObject.Find("Player").GetComponent<HealthPlayer>();
        currentPlayer = GameObject.Find("Player/currentPlayer").transform;
    }
    void Start()
    {
        //rightIK = this.gameObject.transform.Find("mixamorig:Hips/RigEnemy/ERight").GetComponent<TwoBoneIKConstraint>();
        //leftIK = this.gameObject.transform.Find("mixamorig:Hips/RigEnemy/ELeft").GetComponent<TwoBoneIKConstraint>();
        //StartCoroutine(EnemyEquip());
    }

    // Update is called once per frame
    void Update()
    {
        GetViewPlayer();
        AllowFire();
    }
    public void AllowFire()
    {
        if (allowFire && !GetComponent<HealthEnemy>().isDead)
        {
            Fire();
        }
    }
    void Fire()
    {
        if (currentPlayerHealth.playerDead) return;
        if (distanceToCloset > fieldOfViewEnemy.viewRadius) return;
        Vector3 playerPosition = new Vector3(currentPlayer.position.x, transform.position.y, currentPlayer.position.z);
        this.gameObject.transform.LookAt(playerPosition);
        StartCoroutine(EnemyAllowFire());
        ShootBullet();
    }

    void GetViewPlayer()
    {
        HealthPlayer tMin = null;
        float minDist = fieldOfViewEnemy.viewRadius;
        Vector3 currentPos = transform.position;
            float dist = Vector3.Distance(player.transform.position, currentPos);
            distanceToCloset = dist;
            if (dist < minDist)
            {
                if (!currentPlayerHealth.playerDead)
                {
                    tMin = currentPlayerHealth;
                    minDist = dist;
                    currentPlayer = tMin.transform;
                }
            }
    }

    public void ShootBullet()
    {
        GameObject cB = Instantiate(bullet, spawnPoint.transform.position, Quaternion.identity);
        Vector3 t_pos = new Vector3(currentPlayer.position.x, transform.position.y + 1, currentPlayer.position.z);
        cB.transform.LookAt(t_pos);
        cB.GetComponent<BulletScript>().isEnemy = true;
    }

    IEnumerator EnemyAllowFire()
    {
        allowFire = false;
        yield return new WaitForSeconds(0.5f);
        allowFire = true;
    }

    IEnumerator EnemyEquip()
    {
        //rightIK.data.target = this.gameObject.transform.GetChild(4).Find("GripR").transform;
        //leftIK.data.target = this.gameObject.transform.GetChild(4).Find("GripL").transform;
        yield return new WaitForEndOfFrame();
    }
}
