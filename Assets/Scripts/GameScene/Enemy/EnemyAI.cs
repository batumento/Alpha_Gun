using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    FOVEnemy fieldOfViewEnemy;
    EnemyFire enemyFire;

    public NavMeshAgent agent;
    public Animator anim;
    public Transform player;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    private bool calculating;


    [Header("Patroling")]
    public Transform[] path;
    public Transform walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    private int pathIndex;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    [Header("States")]
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        enemyFire = GetComponent<EnemyFire>();
        fieldOfViewEnemy = GetComponent<FOVEnemy>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        walkPoint = transform.Find("AI_point").GetChild(0);
        path[0] = transform.Find("AI_point").GetChild(0);
        path[1] = transform.Find("AI_point").GetChild(1);
        path[0].parent = null;
        path[1].parent = null;
    }
    public void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, fieldOfViewEnemy.viewRadius, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, fieldOfViewEnemy.viewRadius, whatIsPlayer);
        if (transform.GetComponent<HealthEnemy>().isDead) return;

        WalkToThePoint();
        if (walkPoint == null) return;

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
    public void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(player.transform.position);
        if (this.name == "NearEnemy") return;
        enemyFire.AllowFire();
    }
    private IEnumerator CalculatePath()
    {
        calculating = true;
        if (pathIndex <= path.Length - 1)
        {
            pathIndex++;
            yield return new WaitForSecondsRealtime(0);
            walkPoint = path[pathIndex];
            agent.SetDestination(walkPoint.position);
            calculating = false;
            anim.SetBool("isRunning", true);
        }
    }
    private void WalkToThePoint()
    {

        if (pathIndex >= path.Length) pathIndex = 0;

        if (!playerInSightRange && walkPoint != null && !calculating)
        {
            if (Vector3.Distance(transform.position, agent.destination) < 2 && agent.destination != null)
            {
                anim.SetBool("isRunning", true);
                agent.SetDestination(walkPoint.position);
            }
            if (Vector3.Distance(transform.position, walkPoint.position) < 2)
            {
                anim.SetBool("isRunning", false);
                StartCoroutine(CalculatePath());
            }
        }

    }


}
