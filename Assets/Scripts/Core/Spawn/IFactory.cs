using Assets.Scripts.Core.Bullets;

namespace Assets.Scripts.Core.Spawn
{
    public interface IFactory
    {
        Bullet Get();
    }
}
