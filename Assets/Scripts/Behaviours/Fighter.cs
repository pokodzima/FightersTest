using ServiceLocator.Services;
using UnityEngine;

namespace Behaviours
{
    public class Fighter : MonoBehaviour
    {
        public float AttackRadius;
        public float MaxHealth;
        public float AttackPower;
        public float AttackRate;

        public Fighter FighterToAttack { get; set; }
        public Fighter AttackingFighter { get; private set; }
        private bool _isAttacking;
        private float _attackTimer;
        private float _currentHealth;
        private bool _isDestroying;
        private FighterLocator _fighterLocator;


        private void Start()
        {
            _fighterLocator = ServiceLocator.ServiceLocator.Current.Get<FighterLocator>();
            _fighterLocator.Register(this);
            _currentHealth = MaxHealth;
        }

        private void OnDisable()
        {
            _fighterLocator.Unregister(this);
        }

        public void StartAttack()
        {
            _isAttacking = true;
        }

        public void StopAttack()
        {
            _isAttacking = false;
        }

        private void Damage(float damageAmount, Fighter attacker)
        {
            _currentHealth -= damageAmount;
            AttackingFighter = attacker;
            if (_currentHealth <= 0f)
            {
                _isDestroying = true;
            }
        }

        private void Update()
        {
            if (_isDestroying)
            {
                Destroy(gameObject);
            }

            if (_isAttacking)
            {
                _attackTimer -= Time.deltaTime;
                if (_attackTimer <= 0f)
                {
                    if (FighterToAttack == null)
                    {
                        return;
                    }

                    FighterToAttack.Damage(AttackPower, this);
                    _attackTimer = 1f / AttackRate;
                }
            }
        }
    }
}