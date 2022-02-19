using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;


public class ParticlesController : MonoBehaviour
{

    public ParticleManager particleManager;
    /**
    float timer = 0f;
    ParticleSystem curParticleSystem;
    float duration;
    string curParticleName;
    */
    Dictionary<string, ParticleSystem> particlesMap;
    Dictionary<string, float> durationsMap;
    Dictionary<string, float> maxDurationMap;
    private void Start()
    {
        particleManager = ParticleManager.Instance;    
        particlesMap = new Dictionary<string, ParticleSystem>();
        durationsMap = new Dictionary<string, float>();
        maxDurationMap = new Dictionary<string, float>();
    }

    public void ChangeParticles(string name, float duration)
    {
        Debug.Log("change particles");
        /**
        if (curParticleSystem && curParticleName == name)
            return;
       
        curParticleSystem = particleManager.GetParticle(name);
        if (curParticleSystem != null)
        {
            timer = 0f;
            this.duration = duration;
            curParticleSystem = Instantiate(curParticleSystem, gameObject.transform);
            curParticleName = name;
        }
        */
        if (particlesMap.ContainsKey(name))
            return;
        var newParticles = particleManager.GetParticle(name);
        if (newParticles != null)
        {
            durationsMap[name] = 0f;
            maxDurationMap[name] = duration;
            particlesMap[name] = Instantiate(newParticles, gameObject.transform);

        }
            
    }

    private void Update()
    {
        foreach(var particleName in particlesMap.Keys.ToList())
        {
            durationsMap[particleName] += Time.deltaTime;
            if (durationsMap[particleName] > maxDurationMap[particleName])
            {
                Destroy(particlesMap[particleName]);
                particlesMap.Remove(particleName);
                durationsMap.Remove(name);
                maxDurationMap.Remove(name);
            }
        }
        /**
        timer += Time.deltaTime;
        if (timer > duration && curParticleSystem)
        {
            Destroy(curParticleSystem);
            curParticleSystem = null;
        }
        */
            
    }
}
