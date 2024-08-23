using Photon.Pun;
using System;
using UnityEngine;

namespace Assets.Scripts.Core.Bullets
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public abstract class Bullet : MonoBehaviourPun
    {
        [SerializeField, Range(1, 200)] protected float _speed;
        [SerializeField, Range(-5f, 5f)] protected float _gravityForce;
        [SerializeField, Range(1, 100)] protected int _damageValue;

        [SerializeField] protected Rigidbody _rigidBody;
        [SerializeField] protected CapsuleCollider _collider;

        public void Initialize()
        {

        }

        public void Fly(Vector3 direction)
        {
            transform.forward = direction;
            Vector3 gravityVelocity = Vector3.down * _gravityForce;
            _rigidBody.AddForce(direction * _speed + gravityVelocity, ForceMode.Impulse);
        }
    }
}
