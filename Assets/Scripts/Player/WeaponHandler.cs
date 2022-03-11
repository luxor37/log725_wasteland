using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEditor;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Transform meleeWeaponOnhand;
    public Transform meleeWeaponOnback;
    public Transform rangeWeaponOnhand;
    public Transform rangeWeaponOnback;
    private PlayerAttack _playerAttack;
    private PlayerMovementController _playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        _playerMovement = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerAttack.isRange &&  !_playerMovement.isClimbing)
        {
            rangeWeaponOnhand.gameObject.SetActive(true);
            rangeWeaponOnback.gameObject.SetActive(false);
        }
        else if (!_playerAttack.isRange || _playerMovement.isClimbing)
        {
            rangeWeaponOnhand.gameObject.SetActive(false);
            rangeWeaponOnback.gameObject.SetActive(true);
        }
    }
}
