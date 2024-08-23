using UnityEngine;

namespace Assets.Scripts.Core.Weapon.Throwables
{
    public abstract class Throwable : Weapon
    {
        [SerializeField] protected Rigidbody _rigidbody;
        [Space]
        [SerializeField] protected float _throwVelocity;
        [SerializeField] protected float _damage;

        protected abstract string PrefabPath { get; }

        public abstract void Throw();
    }
}
