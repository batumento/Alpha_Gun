using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePipeController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] HealthPlayer playerHealth;
    [Header("Particle Settings")]
    [SerializeField] public float damage;
    ParticleSystem ps;
    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();
        ps = GetComponent<ParticleSystem>();
        InvokeRepeating(nameof(PlayParticleEffect), 1, 4.5f);
        ps.trigger.AddCollider(GameObject.Find("Player").GetComponent<Collider>());
    }

    private void OnParticleTrigger()
    {
        int triggeredParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for (int i = 0; i < triggeredParticles; i++)
        {
            ParticleSystem.Particle p = particles[i];
            p.remainingLifetime = 0;
            playerHealth.health -= damage;
            particles[i] = p;
        }
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
    private void PlayParticleEffect()
    {
        if (ps == null) return;

        ParticleSystem orbParticles = ps.GetComponent<ParticleSystem>();
        if (orbParticles != null)
        {
            orbParticles.Play();
        }
    }
}
