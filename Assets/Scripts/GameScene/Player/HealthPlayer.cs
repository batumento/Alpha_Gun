using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    GameManager gameManager;

    public float health;
    public float maxHealth = 100;
    public bool playerDead = false;

    public GameObject healthBarUI;
    public Slider slider;
    public PostProcessingDeneme postProcessing;
    private CameraFollow camFollow;


    private void Awake()
    {
        gameManager = GameObject.Find("CanvasUI").GetComponent<GameManager>();
        camFollow = FindObjectOfType<CameraFollow>();
    }
    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateHealth();
        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if (health <= 0)
        {
            PlayerisDead();
        }

    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;
        camFollow.ShakeIt();
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    public bool PlayerisDead()
    {
        gameManager.GameOverPanel();
        playerDead = true;
        return playerDead;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            health -= Random.Range(3, 25);
        }
    }
}
