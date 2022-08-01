using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollector : MonoBehaviour
{
    ParticleSystem ps;
    [SerializeField] private LevelBar levelBar;
    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.trigger.AddCollider(GameObject.Find("Player").GetComponent<Collider>());
        levelBar = GameObject.Find("CanvasUI").transform.Find("LevelPanel").GetComponent<LevelBar>();
    }

    private void OnParticleTrigger()
    {
        int triggeredParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for (int i = 0; i < triggeredParticles; i++)
        {
            ParticleSystem.Particle p = particles[i];
            p.remainingLifetime = 0;
            levelBar.AddExperience(Random.Range(10,30));
            levelBar.levelBar.localScale = new Vector3(levelBar.GetExperienceNormalized(), 1, 1);
            particles[i] = p;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}
