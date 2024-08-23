using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public interface IUIAnimation
    {
        public Task MoveVertically(Transform transform, float endPoint);
        public Task MoveHorizontally(Transform transform, float endPoint);
        public Task Move(Transform transform, Vector3 endPoint);
    }
}
