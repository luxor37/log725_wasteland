using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using static FXManager;

public class ParticlesController : MonoBehaviour
{

    private FXManager fxManager;
    private Dictionary<ParticleType, ParticleSystem> particlesMap;
    private Dictionary<ParticleType, float> durationsMap;
    private Dictionary<ParticleType, float> maxDurationMap;
    private void Start()
    {
        fxManager = FXManager.Instance;    
        particlesMap = new Dictionary<ParticleType, ParticleSystem>();
        durationsMap = new Dictionary<ParticleType, float>();
        maxDurationMap = new Dictionary<ParticleType, float>();
    }

    public void ChangeParticles(ParticleType type, float duration, bool onBody=true)
    {

        if (particlesMap.ContainsKey(type))
            return;
        var newParticles = fxManager.GetParticle(type);
        if (newParticles == null)
            return;
       
        durationsMap[type] = 0f;
        maxDurationMap[type] = duration;
        if (onBody)
        {
            particlesMap[type] = Instantiate(newParticles, gameObject.transform);
        } else
            particlesMap[type] = Instantiate(newParticles, gameObject.transform.position, Quaternion.identity);

    }

    private void Update()
    {
        foreach(var particleName in particlesMap.Keys.ToList())
        {
            durationsMap[particleName] += Time.deltaTime;

            if (!(durationsMap[particleName] > maxDurationMap[particleName])) continue;

            Destroy(particlesMap[particleName].gameObject);
            particlesMap.Remove(particleName);
            durationsMap.Remove(particleName);
            maxDurationMap.Remove(particleName);
        }
            
    }

    public void RemoveParticle(ParticleType particleName)
    {
        if (!particlesMap.ContainsKey(particleName)) return;

        Destroy(particlesMap[particleName].gameObject);
        particlesMap.Remove(particleName);
        durationsMap.Remove(particleName);
        maxDurationMap.Remove(particleName);
    }
}
