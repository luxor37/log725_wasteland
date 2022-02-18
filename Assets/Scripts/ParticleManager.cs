using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public List<ParticleSystem> particles = new List<ParticleSystem>();


    private static ParticleManager instance = null;




    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public static ParticleManager Instance
    {
        get
        {
            return instance;
        }

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
