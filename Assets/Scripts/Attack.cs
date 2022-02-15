using UnityEngine;

public enum AttackForm
{
    Melee, Range
}

public enum AttackType
{
    Single , Aoe
}

public class Attack
{
    private int _basicAttack;
    private AttackType _attackType;
    private AttackForm _attackForm;
    private Transform _attackPoint;

    //private status debuff
    private object _attackVFX;
    private object _attackSound;
    private CharacterElement _attackElement;

    public Attack(int basicAttack, AttackType attackType, AttackForm attackForm, Transform attackPoint, object attackVFX, object attackSound, CharacterElement attackElement)
    {
        _basicAttack = basicAttack;
        _attackType = attackType;
        _attackForm = attackForm;
        _attackPoint = attackPoint;
        _attackVFX = attackVFX;
        _attackSound = attackSound;
        _attackElement = attackElement;
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

    public Transform AttackPoint
    {
        get => _attackPoint;
        set => _attackPoint = value;
    }

    public object AttackVFX
    {
        get => _attackVFX;
        set => _attackVFX = value;
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
}
