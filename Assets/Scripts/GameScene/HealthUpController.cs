using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpController : MonoBehaviour
{
    HealthPlayer healthPlayer;
    [SerializeField] float rotateSpeed;
    private void Start()
    {
        healthPlayer = GameObject.Find("Player").GetComponent<HealthPlayer>();
    }
    private void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (healthPlayer.health == 100)
            {
                healthPlayer.health = 100;
            }
            healthPlayer.health += 20;
            Destroy(gameObject);
        }
    }
}
