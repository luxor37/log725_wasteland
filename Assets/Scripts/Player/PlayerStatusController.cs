using Status;
using UnityEngine;

namespace Player
{
    public class PlayerStatusController : StatusController
    {
        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
            _particlesController = GameObject.FindGameObjectWithTag("Player").GetComponent<ParticlesController>();

            base.onDeath += PlayerDeath;
        }

        public void AttackMultiplier(int multiplier, int flat)
        {

            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().meleeDamage *= multiplier;
            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().rangedDamage *= multiplier;
            ShowStat("x" + multiplier, new Color(0, 1, 1, 1));

            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().meleeDamage += flat;
            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().rangedDamage += flat;
            ShowStat("+" + flat, new Color(0, 1, 1, 1), 0.5f);

        }

        public void AttackMultiplierRevert(int multiplier, int flat)
        {
            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().meleeDamage -= flat;
            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().rangedDamage -= flat;
            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().meleeDamage /= multiplier;
            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().rangedDamage /= multiplier;
        }

        public void AddCoin()
        {
            PersistenceManager.coins += 1;
        }

        void PlayerDeath()
        {
            SceneTransitionManager.sceneTransitionManager.GameOver();
        }
    }
}
