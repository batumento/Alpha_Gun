using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydrant : MonoBehaviour
{
    ParticleSystem ps;
    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.trigger.AddCollider(GameObject.Find("Player").GetComponent<Collider>());
        InvokeRepeating(nameof(PlayParticleEffect), 1, 5);
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
