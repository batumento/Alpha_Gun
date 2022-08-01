using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NearEnemyFire : MonoBehaviour
{
    public Animator animator;
    public FOVEnemy fieldOfEnemy;
    [SerializeField] HealthPlayer currentPlayerHealth;
    [SerializeField] HealthEnemy healthEnemy;

    [SerializeField] float distanceToCloset;
    [SerializeField] Transform currentPlayer;

    public GameObject player;
    public GameObject sword;

    [SerializeField] private SphereCollider attackCollider;
    private void Awake()
    {
        healthEnemy = GetComponent<HealthEnemy>();
        animator = GetComponent<Animator>();
        fieldOfEnemy = this.GetComponent<FOVEnemy>();
        player = GameObject.Find("Player");
        currentPlayerHealth = GameObject.Find("Player").GetComponent<HealthPlayer>();
    }

    private void Update()
    {
        GetViewPlayer();
        Attack();
    }
    void GetViewPlayer()
    {
        HealthPlayer tMin = null;
        float minDist = fieldOfEnemy.viewRadius;
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

    void Attack()
    {
        if (healthEnemy.health <= 0) return;
        if (currentPlayerHealth.playerDead) return;
        if (distanceToCloset > fieldOfEnemy.viewRadius) return;
        Vector3 playerPosition = new Vector3(currentPlayer.position.x, transform.position.y, currentPlayer.position.z);
        this.gameObject.transform.LookAt(playerPosition);
        if (Vector3.Distance(player.transform.position, transform.position) < 1.9f && !GetComponent<HealthEnemy>().isDead)
        {
            animator.SetBool("isAttack", true);
            sword.transform.rotation = Quaternion.Euler(sword.transform.rotation.x, sword.transform.rotation.y, sword.transform.rotation.z + 90);
        }
        else
        {
            animator.SetBool("isAttack",false);
        }
    }

    void Damage()
    {
        if (healthEnemy.health <= 0) return;
        attackCollider.enabled = true;
    }

    void Undo()
    {
        attackCollider.enabled = false;
    }

}
