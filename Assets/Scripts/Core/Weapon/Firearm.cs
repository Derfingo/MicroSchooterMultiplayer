using Assets.Scripts.Core.Bullets;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Core.Weapon
{
    public abstract class Firearm : Weapon
    {
        [Header("Bullet")]
        [SerializeField] protected int _maxBullets;
        [SerializeField] protected Bullet _bulletPrefab;
        [SerializeField] protected Transform _bulletPosition;
        [SerializeField] protected TMP_Text _amountBulletsText;
        [Header("Position")]
        [SerializeField] protected Vector3 _aimPosition;
        [SerializeField] protected Vector3 _gripPosition;

        public abstract Vector3 AimPosition { get; }
        public abstract Vector3 GripPosition { get; }

        public abstract Recoil RecoilConfig { get; }

        protected abstract string BulletPrefabPath { get; }

        protected int _currentAmountBullets;

        private void Awake()
        {
            Recharge();
        }

        public bool ClipIsEmply()
        {
            if (_currentAmountBullets < 1)
            {
                return true;
            }

            return false;
        }

        public void Recharge()
        {
            _currentAmountBullets = _maxBullets;
            _amountBulletsText.text = _maxBullets.ToString();
        }

        public void Shoot()
        {
            if (ClipIsEmply())
            {
                _amountBulletsText.text = "R";
                return;
            }

            SpawnBullet();
        }

        protected abstract void SpawnBullet();
    }

    [Serializable]
    public struct Recoil
    {
        public float RecoilX;
        public float RecoilY;
        public float RecoilZ;
        public float Snappines;
        public float ReturnVelocity;
    }
}
