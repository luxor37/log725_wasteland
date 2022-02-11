using System.Collections;
using UnityEngine;


public class ParticlesController : MonoBehaviour
{

    public ParticleManager particleManager;

    float timer = 0f;
    ParticleSystem curParticleSystem;
    float duration;
    string curParticleName;

    public void ChangeParticles(string name, float duration)
    {
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
            
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration && curParticleSystem)
        {
            Destroy(curParticleSystem);
            curParticleSystem = null;
        }
            
    }
}
