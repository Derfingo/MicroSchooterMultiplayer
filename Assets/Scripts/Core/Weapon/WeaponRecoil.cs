using UnityEngine;

namespace Assets.Scripts.Core.Weapon
{
    public class WeaponRecoil : MonoBehaviour
    {
        [SerializeField] private Transform _cameraHolder;
        [SerializeField] private Transform _weaponHolder;

        private float _recoilX;
        private float _recoilY;
        private float _recoilZ;

        private float _snappines;
        private float _returnVelocity;

        private float _recoilVelocity = 1.5f;

        private Vector3 _currentRotation;
        private Vector3 _targetRotation;

        //public Vector3 RecoilRotation => _currentRotation;

        public void FixedUpdatePass()
        {
            _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnVelocity * Time.fixedDeltaTime * _recoilVelocity);
            _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappines * Time.fixedDeltaTime * _recoilVelocity);
            _weaponHolder.localRotation *= Quaternion.Euler(_currentRotation);
            _cameraHolder.localRotation *= Quaternion.Euler(_currentRotation);
        }

        public void RecoilShoot()
        {
            _targetRotation += new Vector3(_recoilX, Random.Range(-_recoilY, _recoilY), Random.Range(-_recoilZ, _recoilZ));
        }

        public void SetRecoilConfig(Recoil config)
        {
            _recoilX = config.RecoilX;
            _recoilY = config.RecoilY;
            _recoilZ = config.RecoilZ;
            _snappines = config.Snappines;
            _returnVelocity = config.ReturnVelocity;
        }
    }
}
