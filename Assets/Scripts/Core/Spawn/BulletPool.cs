using Assets.Scripts.Core.Bullets;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core.Spawn
{
    public class BulletPool : IPool
    {

        private readonly List<Bullet> _bullets;

        public BulletPool(int countBullets = 50)
        {
            _bullets = new List<Bullet>(countBullets);
        }

        public Bullet Get()
        {
            throw new NotImplementedException();
        }

        public void Return(Bullet bullet)
        {
            if (bullet == null)
            {
                throw new NullReferenceException();
            }

            _bullets.Add(bullet);
        }
    }
}
