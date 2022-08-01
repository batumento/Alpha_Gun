using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class HealthEnemy : MonoBehaviour
{
    [Header("Loot")]
    public GameObject[] items = new GameObject[2];
    byte lootSpawn;
    [Header("Money")]
    public GameObject money;
    private bool moneySpawn;
    public Vector3 spawnPosition;
    [Header("HealthValues")]
    public float health;
    public float maxHealth = 100;
    public bool isDead = false;
    public Animator _Eanimator;
    [Header("HealthCanvas")]
    public GameObject healthBarUI;
    public Slider slider;
    [Header("Particles")]
    [SerializeField] private GameObject expParticles;
    [SerializeField] private GameObject bloodParticles;
    

    public void Awake()
    {
        _Eanimator = GetComponent<Animator>();
        expParticles = transform.Find("XP_Particle").gameObject;
        bloodParticles = transform.Find("bloodEffect").gameObject;
    }
    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        spawnPosition = transform.position;
        slider.value = CalculateHealth();
        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if (health <= 0)
        {
            var denemeLoot1 = Resources.Load("AmmoUp");
            var denemeLoot2 = Resources.Load("HealthUp");
            items[0] = denemeLoot1 as GameObject;
            items[1] = denemeLoot2 as GameObject;
            int spawnLoot = Random.Range(0, items.Length);
            if (lootSpawn == 0)
            {
                Instantiate(items[spawnLoot], this.transform);
                lootSpawn++;
            }
            items[spawnLoot].transform.parent = null;
            EnemyisDead();
            
        }

    }
    
    float CalculateHealth()
    {
        return health / maxHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammo")
        {
            PlayParticleEffect(bloodParticles);
        }
    }
    private void PlayParticleEffect(GameObject particleObject)
    {
        if (particleObject == null) return;

        ParticleSystem orbParticles = particleObject.GetComponent<ParticleSystem>();
        if (orbParticles != null)
        {
            orbParticles.Play();
        }
    }

    public bool EnemyisDead()
    {
        expParticles.active = true;
        Destroy(GetComponent<FOVEnemy>());
        isDead = true;
        _Eanimator.SetBool("playerDeath", true);
        if (!moneySpawn)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(money, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            moneySpawn = true;
        }
        GetComponent<NavMeshAgent>().ResetPath();
        return isDead;
    }
}
