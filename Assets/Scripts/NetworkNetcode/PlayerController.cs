using Assets.Scripts.Core.Input;
using Assets.Scripts.Core.Player;
using System;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.NetworkNetcode
{
    public class PlayerController : NetworkBehaviour
    {
        [Header("Move")]
        [SerializeField] private float _moveVelocity = 4f;
        [SerializeField] private float _runVelocity = 6f;
        [Header("Jump")]
        [SerializeField] private float _jumpVelocity = 200f;
        [Header("Look")]
        [SerializeField] private Transform _cameraPoint;
        [SerializeField, Range(0.1f, 1f)] private float _sensitivityX = 0.5f;
        [SerializeField, Range(0.1f, 1f)] private float _sensitivityY = 0.5f;
        [SerializeField, Range(3f, 6f)] private float _sensitivityOffset = 4f;
        [SerializeField] private float _minLookAngle = -90f;
        [SerializeField] private float _maxLookAngle = 90f;

        private InputDevices _input;
        private Rigidbody _rigidbody;
        private GravityDetection _gravityDetection;

        private Camera _camera;
        private float _mouseX;
        private float _mouseY;

        // For serialization
        private Vector3 _syncStartPosition = Vector3.zero;
        private Vector3 _syncEndPosition = Vector3.zero;
        private Quaternion _syncStartRotation = Quaternion.identity;
        private Quaternion _syncEndRotation = Quaternion.identity;

        private void Awake()
        {
            GetComponents();
        }

        private void Start()
        {
            if (IsOwner)
            {
                _input.OnJump += Jump;
                SetCameraPosition();
            }
        }

        private void FixedUpdate()
        {
            if (IsOwner)
            {
                Move();
            }
        }

        private void LateUpdate()
        {
            if (IsOwner)
            {
                Look();
            }
        }

        private void GetComponents()
        {
            _input = GetComponent<InputDevices>();
            _rigidbody = GetComponent<Rigidbody>();
            _gravityDetection = GetComponent<GravityDetection>();
            _camera = FindObjectOfType<Camera>();
        }

        private void Move()
        {
            Vector2 direction = _input.MoveDirection;
            float targetVelocity = _input.IsRun ? _runVelocity : _moveVelocity;
            Vector3 inputDirection = new Vector3(direction.x, 0f, direction.y).normalized;

            if (direction != Vector2.zero)
            {
                inputDirection = transform.right * direction.x + transform.forward * direction.y;
            }

            float velocityX = inputDirection.x * targetVelocity;
            float velocityY = _rigidbody.velocity.y;
            float velocityZ = inputDirection.z * targetVelocity;
            _rigidbody.velocity = new Vector3(velocityX, velocityY, velocityZ);
        }

        private void Jump(bool isJump)
        {
            if (IsGround() && isJump)
            {
                _rigidbody.AddForce(Vector3.up * _jumpVelocity);
            }
        }

        private void Look()
        {
            _mouseX += _input.LookDirection.y / _sensitivityOffset * _sensitivityY;
            _mouseY += _input.LookDirection.x / _sensitivityOffset * _sensitivityX;

            _mouseX = Mathf.Clamp(_mouseX, _minLookAngle, _maxLookAngle);

            transform.rotation = Quaternion.Euler(0, _mouseY, 0);
            _camera.transform.rotation = Quaternion.Euler(-_mouseX, _mouseY, 0);
        }

        private bool IsGround() => _gravityDetection.IsGround;

        private void SetCameraPosition()
        {
            _camera.transform.SetParent(_cameraPoint);
            _camera.transform.position = _cameraPoint.position;
        }
    }
}
