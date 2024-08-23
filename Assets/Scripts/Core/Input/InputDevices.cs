using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Core.Input
{
    public class InputDevices : MonoBehaviour
    {
        private InputPlayerMap _inputMap;

        public Vector2 MoveDirection => _moveDirection;
        public Vector2 LookDirection => _lookDirection;
        public bool IsRun => _isRun;

        public event Action<bool> OnJump;
        public event Action<double> OnShoot;
        public event Action<bool> OnAim;

        public event Action<int> OnChooseFirstWeapon;
        public event Action<int> OnChooseSecondWeapon;
        public event Action<int> OnThirdWeapon;
        public event Action OnGrenade;
        public event Action OnRechargeWeapon;

        private Vector2 _moveDirection;
        private Vector2 _lookDirection;
        private bool _isRun;

        private void Awake()
        {
            _inputMap = new InputPlayerMap();
            _inputMap.Enable();
            AddListeners();
        }

        private void ReadMove(InputAction.CallbackContext contex)
        {
            _moveDirection = contex.ReadValue<Vector2>();
        }

        private void ReadLook(InputAction.CallbackContext contex)
        {
            _lookDirection = contex.ReadValue<Vector2>();
        }

        private void ReadJump(InputAction.CallbackContext contex)
        {
            OnJump?.Invoke(contex.action.IsPressed());
        }

        private void ReadShoot(InputAction.CallbackContext contex)
        {
            OnShoot?.Invoke(contex.startTime);
        }

        private void ReadRun(InputAction.CallbackContext contex)
        {
            _isRun = contex.action.IsPressed();
        }

        private void ReadAim(InputAction.CallbackContext contex)
        {
            OnAim?.Invoke(contex.action.IsPressed());
        }

        private void ReadFirstWeapon()
        {
            OnChooseFirstWeapon?.Invoke(0);
        }

        private void ReadSecondWeapon()
        {
            OnChooseSecondWeapon?.Invoke(1);
        }

        private void ReadThirdWeapon()
        {
            OnThirdWeapon?.Invoke(2);
        }

        private void ReadRecharge()
        {
            OnRechargeWeapon?.Invoke();
        }

        private void ReadGrenade(InputAction.CallbackContext contex)
        {
            OnGrenade?.Invoke();
        }

        private void AddListeners()
        {
            _inputMap.Player.Move.performed += ReadMove;
            _inputMap.Player.Look.performed += ReadLook;
            _inputMap.Player.Jump.performed += ReadJump;
            _inputMap.Player.Run.performed += ReadRun;
            _inputMap.Player.Shoot.performed += ReadShoot;
            _inputMap.Player.FirstWeapon.performed += context => ReadFirstWeapon();
            _inputMap.Player.SecondWeapon.performed += context => ReadSecondWeapon();
            _inputMap.Player.ThirdWeapon.performed += context => ReadThirdWeapon();
            _inputMap.Player.Recharge.performed += context => ReadRecharge();
            _inputMap.Player.Aim.performed += ReadAim;
            _inputMap.Player.Grenage.performed += ReadGrenade;
        }

        private void RevomeListeners()
        {
            _inputMap.Player.Move.performed -= ReadMove;
            _inputMap.Player.Look.performed -= ReadLook;
            _inputMap.Player.Jump.performed -= ReadJump;
            _inputMap.Player.Run.performed -= ReadRun;
            _inputMap.Player.Shoot.performed -= ReadShoot;
            _inputMap.Player.FirstWeapon.performed -= context => ReadFirstWeapon();
            _inputMap.Player.SecondWeapon.performed -= context => ReadSecondWeapon();
            _inputMap.Player.ThirdWeapon.performed -= context => ReadThirdWeapon();
            _inputMap.Player.Recharge.performed -= context => ReadRecharge();
            _inputMap.Player.Aim.performed -= ReadAim;
            _inputMap.Player.Grenage.performed -= ReadGrenade;
        }
    }
}
