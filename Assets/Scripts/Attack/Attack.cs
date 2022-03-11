using UnityEngine;
using Status;

public enum AttackForm
{
    Melee, Range
}

public enum AttackType
{
    Single , Aoe
}

public class Attack : MonoBehaviour
{
    private int _basicAttack;
    private AttackType _attackType;
    private AttackForm _attackForm;
    private Transform _attackStartPoint;
    private float _damageRadiusX;
    private float _damageRadiusY;
    private IStatus _debuff;
    
    private ParticleSystem _attackVFX;
    private ParticleSystem _HitVFX;
    private object _attackSound;
    
    private CharacterElement _attackElement;

    public Attack(int basicAttack,float _damageRadiusX,float _damageRadiusY,  AttackType attackType, AttackForm attackForm, Transform attackStartPoint,CharacterElement attackElement, IStatus debuff)
    {
        _basicAttack = basicAttack;
        _attackType = attackType;
        _attackForm = attackForm;
        _attackStartPoint = attackStartPoint;
        _attackElement = attackElement;
        _debuff = debuff;
        _damageRadiusX = _damageRadiusX;
        _damageRadiusY = _damageRadiusY;


    }

    public int BasicAttack
    {
        get => _basicAttack;
        set => _basicAttack = value;
    }

    public AttackType AttackType
    {
        get => _attackType;
        set => _attackType = value;
    }

    public AttackForm AttackForm
    {
        get => _attackForm;
        set => _attackForm = value;
    }

    public Transform AttackStartPoint
    {
        get => _attackStartPoint;
        set => _attackStartPoint = value;
    }

    public ParticleSystem AttackVFX
    {
        get => _attackVFX;
        set => _attackVFX = value;
    }

    public ParticleSystem HitVFX
    {
        get => _HitVFX;
        set => _HitVFX = value;
    }

    public object AttackSound
    {
        get => _attackSound;
        set => _attackSound = value;
    }

    public CharacterElement AttackElement
    {
        get => _attackElement;
        set => _attackElement = value;
    }

    public IStatus Debuff
    {
        get => _debuff;
        set => _debuff = value;
    }

    public float DamageRadiusX
    {
        get => _damageRadiusX;
        set => _damageRadiusX = value;
    }
    
    public float DamageRadiusY
    {
        get => _damageRadiusY;
        set => _damageRadiusY = value;
    }
}

