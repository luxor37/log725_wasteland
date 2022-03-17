using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using static FXManager;

public class ParticlesController : MonoBehaviour
{

    private FXManager fxManager;
    Dictionary<ParticleType, ParticleSystem> particlesMap;
    Dictionary<ParticleType, float> durationsMap;
    Dictionary<ParticleType, float> maxDurationMap;
    private void Start()
    {
        fxManager = FXManager.Instance;    
        particlesMap = new Dictionary<ParticleType, ParticleSystem>();
        durationsMap = new Dictionary<ParticleType, float>();
        maxDurationMap = new Dictionary<ParticleType, float>();
    }

    public void ChangeParticles(ParticleType name, float duration)
    {

        if (particlesMap.ContainsKey(name))
            return;
        var newParticles = fxManager.GetParticle(name);
        if (newParticles != null)
        {
            durationsMap[name] = 0f;
            maxDurationMap[name] = duration;
            particlesMap[name] = Instantiate(newParticles, gameObject.transform);

        }
            
    }

    private void Update()
    {
        foreach(ParticleType particleName in particlesMap.Keys.ToList())
        {
            durationsMap[particleName] += Time.deltaTime;
            if (durationsMap[particleName] > maxDurationMap[particleName])
            {
                Destroy(particlesMap[particleName]);
                particlesMap.Remove(particleName);
                durationsMap.Remove(particleName);
                maxDurationMap.Remove(particleName);
            }
        }
            
    }
}
