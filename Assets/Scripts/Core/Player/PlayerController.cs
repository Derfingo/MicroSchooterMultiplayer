using Assets.Scripts.Core.Weapon;
using Assets.Scripts.Save;
using Assets.Scripts.Settings;
using Assets.Scripts.UI.Presenters;
using Photon.Pun;
using System;
using UnityEngine;

namespace Assets.Scripts.Core.Player
{
    public class PlayerController : MonoBehaviourPun
    {
        private WeaponController _weaponController;
        private SettingsDataProvider _provider;
        private PlayerMovement _playerMovement;
        [SerializeField] private WeaponRecoil _weaponRecoil;
        private PlayerHealth _playerHealth;
        private GamePresenter _presenter;
        private PlayerLook _playerLook;

        public Transform CameraHolder => _playerLook.CameraHolder;

        public void SetData(SettingsData data)
        {
            if (photonView.IsMine)
            {
                _playerLook.SetSensitivity(data.ControlData);
            }
        }

        public void Initialize(Transform lookRotation, GamePresenter presenter)
        {
            _playerLook.SetLookTransform(lookRotation);
            _presenter = presenter;
        }

        private void Awake()
        {
            _provider = FindObjectOfType<SettingsDataProvider>();
            _weaponController = GetComponent<WeaponController>();
            _playerMovement = GetComponent<PlayerMovement>();
            //_weaponRecoil = GetComponent<WeaponRecoil>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerLook = GetComponent<PlayerLook>();
        }

        private void Start()
        {
            if (photonView.IsMine)
            {
                Cursor.visible = false;
                _playerHealth.OnHealthChanged += UpdateHealth;
                _playerHealth.OnDie += ResetHealth;
                _provider.OnSavedData += SetData;
            }
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                _playerMovement.UpdatePass();
                _playerLook.UpdatePass();
            }
        }

        private void FixedUpdate()
        {
            if (photonView.IsMine)
            {
                _playerMovement.FixedUpdatePass();
                _weaponController.FixedUpdatePass();
                _weaponRecoil.FixedUpdatePass();
            }
        }

        private void LateUpdate()
        {
            if (photonView.IsMine)
            {
                _playerLook.LateUpdatePass();
                _weaponController.UpdatePass();
            }
        }

        private void UpdateHealth(float healthValue)
        {
            _presenter.OnHealthbarUpdate(healthValue);
        }

        private void ResetHealth(PlayerHealth playerHealth)
        {
            _presenter.OnHealthbarUpdate(playerHealth.MaxHealth);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void ResetValues()
        {
            _playerHealth.ResetHelth();
            _weaponController.ResetWeapon();
        }
    }

    [Serializable]
    public struct PlayerData : ISaveStructure
    {
    }
}
