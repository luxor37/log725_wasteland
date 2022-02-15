using System;
using System.Runtime.Serialization;
using Player;
using UnityEditor;
using UnityEngine;

namespace Enemy
{
    public class EnemyStatusController : MonoBehaviour
    {
        private EnemyCharacter _enemyCharacter;
        private Animator _animator;

        private void Start()
        {
            _enemyCharacter = this.gameObject.GetComponent<EnemyCharacter>();
            _animator = GetComponent<Animator>();
        }
        
    }
}