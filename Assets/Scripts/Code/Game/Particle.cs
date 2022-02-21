using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : PoolableMono
{
    ParticleSystem particle;
    ParticleSystem.MinMaxGradient startColor;
    ParticleSystem.MainModule main;
    public override void OnDespawn()
    {
        main.startColor = startColor;
        
    }

    public override void OnInitialize()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        main = particle.main;
        startColor = main.startColor;
        
    }
    public void SetColor(Color color)
    {
        main.startColor = new ParticleSystem.MinMaxGradient(color);
    }
    public override void OnSpawn()
    {
        Wait.Second(Despawn,main.duration);        
    }
    
}
