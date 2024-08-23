using Assets.Scripts.Core.Bullets;
using UnityEngine;

namespace Assets.Scripts.Core.Spawn
{
    public class BulletFactory : IFactory
    {
        private string _path = "Bullets/SimpleBullet";

        public Bullet Get()
        {
            Bullet bullet = GameObject.Instantiate(Resources.Load(_path), Vector3.zero, Quaternion.identity) as Bullet;
            bullet.Initialize();
            return bullet;
        }
    }
}
