using System.Collections;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Core.Weapon.Throwables
{
    public class Grenade : Throwable
    {
        [SerializeField] private float _delay = 3f;

        protected override string PrefabPath => Path.Combine("Prefabs/Weapon", "Grenade");

        public override void Throw()
        {
            _rigidbody.AddForce(Vector3.forward * _throwVelocity);
            StartCoroutine(nameof(Explode));
        }

        private IEnumerator Explode()
        {
            yield return new WaitForSeconds(_delay);

            Debug.Log("BOOM");
            Destroy(gameObject);
        }
    }
}
