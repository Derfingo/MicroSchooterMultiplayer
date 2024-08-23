using Assets.Scripts.Core.Bullets;

namespace Assets.Scripts.Core.Spawn
{
    public interface IPool
    {
        Bullet Get();
        void Return(Bullet bullet);
    }
}
