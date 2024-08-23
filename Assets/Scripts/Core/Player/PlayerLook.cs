using Assets.Scripts.Core.Input;
using Assets.Scripts.Core.Weapon;
using Assets.Scripts.Settings;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.Core.Player
{
    public class PlayerLook : MonoBehaviourPun
    {
        [SerializeField] private WeaponController _weaponController;
        [SerializeField] private WeaponRecoil _recoil;
        [SerializeField] private Transform _cameraHolder;
        [Header("Sensitivity of mouse")]
        [SerializeField, Range(0.1f, 1f)] private float _sensitivityX = 0.5f;
        [SerializeField, Range(0.1f, 1f)] private float _sensitivityY = 0.5f;
        [SerializeField, Range(0.1f, 1f)] private float _aimSensitivity = 0.5f;
        [SerializeField, Range(3f, 6f)] private float _sensitivityOffset = 4f;
        [Header("Look angles by vertical")]
        [SerializeField] private float _minLookAngle = -90f;
        [SerializeField] private float _maxLookAngle = 90f;

        public Transform CameraHolder => _cameraHolder;

        private InputDevices _lookInput;
        private Transform _lookRotation;

        private Vector2 _lookDirection = Vector2.zero;
        public Vector2 LookDirection => _lookDirection;

        private float _mouseX;
        private float _mouseY;

        public float VerticalRotation => _mouseY;

        private void Awake()
        {
            _lookInput = GetComponent<InputDevices>();
        }

        public void UpdatePass()
        {
            ReadDirection();
        }

        public void LateUpdatePass()
        {
            Look();
        }

        public void SetSensitivity(ControlData config)
        {
            _sensitivityX = config.HorizontalSensitivity;
            _sensitivityY = config.VerticalSensitivity;
            _aimSensitivity = config.AimSensitivity;
        }

        private void ReadDirection()
        {
            float targetSensitivityX = _weaponController.IsAim ? _sensitivityX * _aimSensitivity : _sensitivityX;
            float targetSensitivityY = _weaponController.IsAim ? _sensitivityY * _aimSensitivity : _sensitivityY;
            _mouseX += _lookInput.LookDirection.y / _sensitivityOffset * targetSensitivityY;
            _mouseY += _lookInput.LookDirection.x / _sensitivityOffset * targetSensitivityX;
            _mouseX = Mathf.Clamp(_mouseX, _minLookAngle, _maxLookAngle);
            _lookDirection = new Vector2(_mouseX, _mouseY);
        }

        private void Look()
        {
            transform.rotation = Quaternion.Euler(0, _mouseY, 0);
            _lookRotation.rotation = Quaternion.Euler(-_mouseX, _mouseY, 0);
            //_camera.transform.localRotation *= Quaternion.Euler(_recoil.RecoilRotation);
        }

        public void SetLookTransform(Transform lookRoataion)
        {
            _lookRotation = lookRoataion;
        }
    }
}
