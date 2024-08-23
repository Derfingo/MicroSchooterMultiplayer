using Assets.Scripts.Core.Input;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimationMovement : MonoBehaviour
    {
        private InputDevices _input;
        private Animator _animator;

        private int _walkingHash;
        private int _runningHash;
        private int _velocityHash;

        private Vector2 _currentDirection;
        private bool _isWalkingPressed;
        private bool _isRunningPressed;

        [Inject]
        private void Construct()
        {
            _animator = GetComponent<Animator>();
            _walkingHash = Animator.StringToHash("isWalking");
            _runningHash = Animator.StringToHash("isRunning");
            _velocityHash = Animator.StringToHash("velocity");
        }

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            _currentDirection = _input.MoveDirection;
            _isWalkingPressed = ValidateDirection(_currentDirection);
            _isRunningPressed = _input.IsRun;

            bool isWalking = _animator.GetBool(_walkingHash);
            bool isRunning = _animator.GetBool(_runningHash);

            //if (isWalking)
            //{
            //    _velocity += Time.deltaTime * _acceleration;
            //}

            if (_isWalkingPressed && !isWalking)
            {
                _animator.SetBool(_walkingHash, true);
            }

            if (!_isWalkingPressed && isWalking)
            {
                _animator.SetBool(_walkingHash, false);
            }

            if ((_isWalkingPressed && _isRunningPressed) && !isRunning)
            {
                _animator.SetBool(_runningHash, true);
            }

            if ((!_isWalkingPressed || !_isRunningPressed) && isRunning)
            {
                _animator.SetBool(_runningHash, false);
            }

            //_animator.SetFloat(_velocityHash, _velocity);
        }

        private void Rotate()
        {
            Vector3 currentPosition = transform.position;
            Vector3 offset = new(_currentDirection.x, 0f, _currentDirection.y);
            Vector3 positionToLookAt = currentPosition + offset;
            transform.LookAt(positionToLookAt);
        }

        private bool ValidateDirection(Vector2 direction)
        {
            return direction.x != 0f || direction.y != 0f;
        }
    }
}
