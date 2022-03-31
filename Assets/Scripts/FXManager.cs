using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public enum ParticleType
    {
        Fire,
        HealthRegen,
        AttackBoost,
        Shield,
        Wind,
        FireTornado
    }

    public List<Particle> particles;

    [System.Serializable]
    public struct Particle
    {
        public ParticleType name;
        public ParticleSystem particle;
    }

    private static FXManager instance = null;

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

    public static FXManager Instance
    {
        get
        {
            return instance;
        }

    }

    public ParticleSystem GetParticle(ParticleType name)
    {
        return particles.Find(x => x.name == name).particle;
    }
}
