using Photon.Pun;
using System;

namespace Assets.Scripts.Core.Player
{
    public class PlayerHealth : MonoBehaviourPun, IDamage
    {
        public event Action<float> OnHealthChanged;
        public event Action<PlayerHealth> OnDie;

        private readonly float _maxHealth = 100f;
        private float _currentHealth;

        public float MaxHealth => _maxHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void ResetHelth()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            photonView.RPC(nameof(TakeDamageRPC), RpcTarget.All, damage);
        }

        [PunRPC]
        private void TakeDamageRPC(float damage)
        {
            if (photonView.IsMine)
            {
                _currentHealth -= damage;
                OnHealthChanged?.Invoke(_currentHealth);

                if (_currentHealth < 1)
                {
                    OnDie?.Invoke(this);
                    return;
                }
            }
        }
    }
}
