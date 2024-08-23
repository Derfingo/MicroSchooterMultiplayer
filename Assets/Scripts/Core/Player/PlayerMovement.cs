using Assets.Scripts.Core.Input;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.Core.Player
{
    public class PlayerMovement : MonoBehaviourPun
    {
        [Header("Move")]
        [SerializeField] private float _moveVelocity = 4f;
        [SerializeField] private float _runVelocity = 6f;
        [Header("Jump")]
        [SerializeField] private float _jumpVelocity = 200;

        private InputDevices _input;
        private Rigidbody _rigidbody;
        private GravityDetection _gravityDetection;

        private Vector2 _direction;

        private void Awake()
        {
            _input = GetComponent<InputDevices>();
            _rigidbody = GetComponent<Rigidbody>();
            _gravityDetection = GetComponent<GravityDetection>();
        }

        private void Start()
        {
            if (photonView.IsMine)
            {
                _input.OnJump += Jump;

            }
        }

        public void UpdatePass()
        {
            ReadDirection();
        }

        public void FixedUpdatePass()
        {
            MoveRigidbody();
        }

        private void ReadDirection() => _direction = _input.MoveDirection.normalized;

        private void MoveRigidbody()
        {
            float targetVelocity = _input.IsRun ? _runVelocity : _moveVelocity;
            Vector3 inputDirection = new(_direction.x, 0f, _direction.y);

            if (_direction != Vector2.zero)
            {
                inputDirection = transform.right * _direction.x + transform.forward * _direction.y;
            }

            float velocityX = inputDirection.x * targetVelocity;
            float velocityY = _rigidbody.velocity.y;
            float velocityZ = inputDirection.z * targetVelocity;
            _rigidbody.MovePosition(_rigidbody.position + new Vector3(velocityX, velocityY, velocityZ) * Time.fixedDeltaTime);
        }

        private void Jump(bool isJump)
        {
            if (IsGround() && isJump)
            {
                _rigidbody.AddForce(Vector3.up * _jumpVelocity);
            }
        }

        private bool IsGround() => _gravityDetection.IsGround;
    }
}
