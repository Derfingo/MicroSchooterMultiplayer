using Assets.Scripts.Core.Bullets;
using Photon.Pun;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Core.Weapon
{
    public class Shotgun : Firearm
    {
        [SerializeField] private int _bulletFraction = 4;
        [SerializeField] private float _horizontalSpreadAngle = 45f;
        [SerializeField] private float _verticalSpreadAngle = 45f;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Recoil _recoil;

        public override Recoil RecoilConfig => _recoil;

        public override Vector3 AimPosition => _aimPosition;
        public override Vector3 GripPosition => _gripPosition;

        protected override string BulletPrefabPath => Path.Combine("Prefabs/Bullets", "ShotgunBullet");

        private void Start()
        {
            _aimPosition = new(0f, 0.5f, 0);
            _gripPosition = new(0f, -0.15f, 0.6f);
        }

        protected override void SpawnBullet()
        {
            for (int i = 0; i <= _bulletFraction; i++)
            {
                float x = Random.Range(-_verticalSpreadAngle, _verticalSpreadAngle);
                float y = Random.Range(-_horizontalSpreadAngle, _horizontalSpreadAngle);
                Quaternion randomRotation = Quaternion.Euler(x, y, 0);

                GameObject gameObject = PhotonNetwork.Instantiate(BulletPrefabPath, _spawnPoints[i].position, Quaternion.identity);
                Bullet bullet = gameObject.GetComponent<Bullet>();
                bullet.transform.forward = _spawnPoints[i].forward;
                bullet.transform.rotation *= randomRotation;
                bullet.Fly(bullet.transform.forward);
            }

            _currentAmountBullets--;
            _amountBulletsText.text = _currentAmountBullets.ToString();
        }
    }
}
