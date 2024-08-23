using UnityEngine;

namespace Assets.Scripts.Core.Player
{
    public class GravityDetection : MonoBehaviour
    {
        [SerializeField] private BoxCollider _groundCollider;

        public bool IsGround { get; private set; }

        private void OnTriggerStay(Collider other)
        {
            IsGround = true;
        }

        private void OnTriggerExit(Collider other)
        {
            IsGround = false;
        }

        private void OnCollisionStay(Collision collision)
        {
            IsGround = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            IsGround = false;
        }
    }
}
