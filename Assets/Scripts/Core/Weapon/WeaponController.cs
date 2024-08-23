using Assets.Scripts.Core.Input;
using Assets.Scripts.Core.Player;
using Assets.Scripts.Core.Weapon.Throwables;
using Photon.Pun;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Assets.Scripts.Core.Weapon
{
    public class WeaponController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Grenade _grenadePrefab;
        [SerializeField] private WeaponRecoil _recoil;
        [SerializeField] private PlayerLook _playerLook;
        [SerializeField] private Firearm[] _weaponPrefabs;
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private Transform _gripHolder;
        [SerializeField] private float _aimVelocity = 0.5f;

        public float AimVelocity => _aimVelocity;

        private List<Firearm> _weapons;

        private InputDevices _input;
        private Firearm _currentWeapon;

        private Vector3 _aimPosition = new(0f, 0.5f, 0);
        private Vector3 _shoulderPosition = new(0.3f, 0.4f, 0);

        private bool _isAim;
        public bool IsAim => _isAim;

        private float _currentLerpTime;
        private float _lerpTime = 1f;


        private string PrefabPath => Path.Combine("Prefabs/Weapon", "Grenade");

        private void Start()
        {
            _input = GetComponent<InputDevices>();
            SetDefaultWeapons();

            if (photonView.IsMine)
            {
                _input.OnChooseFirstWeapon += SetWeapon;
                _input.OnChooseSecondWeapon += SetWeapon;
                _input.OnThirdWeapon += SetWeapon;
                _input.OnRechargeWeapon += RechargeWeapon;
                _input.OnShoot += Shoot;
                _input.OnAim += (isAim) => _isAim = isAim;
            }
        }

        private void SetDefaultWeapons()
        {
            _weapons = new List<Firearm>();

            for (int i = 0; i < _weaponPrefabs.Length; i++)
            {
                var weapon = Instantiate(_weaponPrefabs[i], _gripHolder.position, Quaternion.identity, _gripHolder);
                weapon.transform.forward = transform.forward;
                _weapons.Add(weapon);
                weapon.Hide();
            }

            _currentWeapon = _weapons[0];
            _recoil.SetRecoilConfig(_currentWeapon.RecoilConfig);
            _currentWeapon.Show();
        }

        private void SetWeapon(int indexWeapon)
        {
            _currentWeapon.Hide();
            _currentWeapon = _weapons[indexWeapon];
            _recoil.SetRecoilConfig(_currentWeapon.RecoilConfig);
            _currentWeapon.Show();

            if (photonView.IsMine)
            {
                Hashtable hash = new()
                {
                    { "indexWeapon", indexWeapon }
                };
                PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            }
        }

        private void SetGrenade()
        {
            _currentWeapon.Hide();
            var grenade = Instantiate(_grenadePrefab, _gripHolder.position, Quaternion.identity, _gripHolder);
            grenade.transform.forward = transform.forward;
        }

        private void Throw(double startTime)
        {
            var grende = _gripHolder.GetComponent<Grenade>();

            if (grende == null)
            {
                SetGrenade();
            }

            grende.Throw();
            _currentWeapon.Shoot();
        }

        private void Shoot(double startTime)
        {
            _currentWeapon.Shoot();
            _recoil.RecoilShoot();
        }

        private void RechargeWeapon()
        {
            _currentWeapon.Recharge();
        }

        public void UpdatePass()
        {
            RotateWeapon();
        }

        public void FixedUpdatePass()
        {
            Aim(_isAim);
        }

        private void RotateWeapon()
        {
            Vector2 direction = _playerLook.LookDirection;
            _weaponHolder.rotation = Quaternion.Euler(-direction.x, direction.y, 0);
            //_currentWeapon.transform.localRotation = Quaternion.Euler(_recoil.RecoilRotation);
        }

        private void Aim(bool isAim)
        {
            Vector3 gripPosition = _gripHolder.localPosition;
            Vector3 weaponPosition = _weaponHolder.localPosition;

            _currentLerpTime += Time.fixedDeltaTime;
            if (_currentLerpTime > _lerpTime)
            {
                _currentLerpTime = _lerpTime;
            }

            float t = _currentLerpTime / _lerpTime;

            if (isAim)
            {
                _weaponHolder.localPosition = Vector3.LerpUnclamped(weaponPosition, _currentWeapon.AimPosition, t * _aimVelocity);
                _gripHolder.localPosition = Vector3.LerpUnclamped(gripPosition, _currentWeapon.GripPosition, t * _aimVelocity);
            }
            else
            {
                _weaponHolder.localPosition = Vector3.LerpUnclamped(weaponPosition, _shoulderPosition, t * _aimVelocity);
                _gripHolder.localPosition = Vector3.LerpUnclamped(gripPosition, new(0f, -0.15f, 0.6f), t * _aimVelocity);
            }
        }

        public void ResetWeapon()
        {
            RechargeWeapon();
        }

        public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
        {
            if (!photonView.IsMine && targetPlayer == photonView.Owner)
            {
                SetWeapon((int)changedProps["indexWeapon"]);
            }
        }
    }

    public enum WeaponType
    {
        Gun,
        Shotgun,
        Rifle
    }
}
