using Assets.Scripts.Core.Player;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.Core.Bullets
{
    public class GunBullet : Bullet
    {
        [SerializeField] private SpriteRenderer _bulletImpactPrefab;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out IDamage damage))
            {
                damage.TakeDamage(_damageValue);
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                _damageValue = 0;
            }

            photonView.RPC(nameof(HitRPC), RpcTarget.All, collision.contacts[0].point, collision.contacts[0].normal);
            Destroy(gameObject, 3);
        }

        [PunRPC]
        private void HitRPC(Vector3 hitPostion, Vector3 hitNoraml)
        {
            Collider[] colliders = Physics.OverlapSphere(hitPostion, 0.3f);
            if (colliders.Length != 0)
            {
                var bulletImpact = Instantiate(_bulletImpactPrefab, hitPostion + hitNoraml * 0.001f, Quaternion.LookRotation(hitNoraml, Vector3.up));
                Destroy(bulletImpact, 1f);
                bulletImpact.transform.SetParent(colliders[0].transform);
            }
        }
    }
}
