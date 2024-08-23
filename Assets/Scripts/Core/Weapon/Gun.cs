using Assets.Scripts.Core.Bullets;
using Photon.Pun;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Core.Weapon
{
    public class Gun : Firearm
    {
        [SerializeField] private Recoil _recoil;

        public override Recoil RecoilConfig => _recoil;

        private void Start()
        {
            _aimPosition = new(0f, 0.5f, 0);
            _gripPosition = new(0f, -0.15f, 0.6f);
        }

        protected override string BulletPrefabPath => Path.Combine("Prefabs/Bullets", "GunBullet");

        public override Vector3 AimPosition => _aimPosition;
        public override Vector3 GripPosition => _gripPosition;

        protected override void SpawnBullet()
        {
            GameObject gameObject = PhotonNetwork.Instantiate(BulletPrefabPath, _bulletPosition.position, Quaternion.identity);
            Bullet bullet = gameObject.GetComponent<Bullet>();
            bullet.Fly(_bulletPosition.forward);
            _currentAmountBullets--;
            _amountBulletsText.text = _currentAmountBullets.ToString();
        }
    }
}
