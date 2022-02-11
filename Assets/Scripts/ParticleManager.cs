using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public List<ParticleSystem> particles = new List<ParticleSystem>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ParticleSystem GetParticle(string name)
    {
        foreach (ParticleSystem p in particles)
        {
            if (p.name == name)
                return p;
        }
        return null;
    }
}
