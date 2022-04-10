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

    private static FXManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public static FXManager Instance => _instance;

    public ParticleSystem GetParticle(ParticleType type)
    {
        return particles.Find(x => x.name == type).particle;
    }
}
