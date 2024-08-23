using UnityEngine;

namespace Assets.Scripts.Core.Bullets
{
    public class BulletNonRigidbody : MonoBehaviour
    {
        [SerializeField] private float _velocity;
        [SerializeField] private float _gravity;

        private Vector3 _startPosition;

        private float _startTime = -1;

        private void FixedUpdate()
        {
            if (_startTime < 0)
            {
                _startTime = Time.time;
            }

            RaycastHit hit;
            float currentTime = Time.time - _startTime;
            float nextTime = currentTime + Time.fixedDeltaTime;

            Vector3 currentPoint = FindPointOnParabola(currentTime);
            Vector3 nextPoint = FindPointOnParabola(nextTime);

            if (CastRayBetweenPoints(currentPoint, nextPoint, out hit))
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            float currentTime = Time.time - _startTime;
            Vector3 currentPoint = FindPointOnParabola(currentTime);
            transform.position = currentPoint;
        }

        private Vector3 FindPointOnParabola(float time)
        {
            Vector3 point = _startPosition + (_startPosition * _velocity * time);
            Vector3 gravityVelocity = Vector3.down * _gravity * time * time;
            return point + gravityVelocity;
        }

        private bool CastRayBetweenPoints(Vector3 startPoint, Vector3 endPoint, out RaycastHit hit)
        {
            return Physics.Raycast(startPoint, endPoint - startPoint, out hit, (endPoint - startPoint).magnitude);
        }
    }
}
