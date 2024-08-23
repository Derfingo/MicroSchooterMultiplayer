using Assets.Scripts.Core.Input;
using UnityEngine;

namespace Assets.Scripts.Core.Player
{
    public class CharacterControllerMovement : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField] private float _moveVelocity = 20f;
        [SerializeField] private float _runVelocity = 6f;
        [Header("Ground")]
        [SerializeField] private float _gravityMultiplier = 9.81f;
        [Header("Jump")]
        [SerializeField] private float _jumpVelocity = 4f;

        private InputDevices _input;
        private CharacterController _characterController;

        private Vector2 _direction;
        private float _gravityVelocity;

        private void Awake()
        {
            _input = GetComponent<InputDevices>();
            _characterController = GetComponent<CharacterController>();
            _input.OnJump += Jump;
        }

        private void Update()
        {
            ApplyGravity();
            _direction = _input.MoveDirection.normalized;
        }

        private void FixedUpdate()
        {
            MoveCharacterController();
        }

        private void MoveCharacterController()
        {
            float targetVelocity = _input.IsRun ? _runVelocity : _moveVelocity;
            Vector3 inputDirection = new Vector3(_direction.x, 0f, _direction.y).normalized;

            if (_direction != Vector2.zero)
            {
                inputDirection = transform.right * _direction.x + transform.forward * _direction.y;
            }

            _characterController.Move(inputDirection * targetVelocity * Time.deltaTime);
        }

        private void Jump(bool isJump)
        {
            if (IsGround() && isJump)
            {
                _gravityVelocity = _jumpVelocity;
                Vector3 jumpDirection = new(0, _gravityVelocity * Time.deltaTime, 0f);
                _characterController.Move(jumpDirection);
            }
        }

        private void ApplyGravity()
        {
            if (IsGround())
            {
                _gravityVelocity = 0f;
            }

            _gravityVelocity -= _gravityMultiplier * Time.deltaTime;
            _characterController.Move(new Vector3(0, _gravityVelocity, 0) * Time.deltaTime);
        }

        private bool IsGround() => _characterController.isGrounded;
    }
}
