using Assets.Scripts.UI.Presenters;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIAnimation
    {
        private IMenuPresenter _presenter;
        private readonly float _duration = 1f;

        public Task MoveVertically(Transform transform, float endPoint)
        {
            return DOTween.Sequence()
                .Append(transform.DOMoveY(endPoint, _duration))
                .SetEase(Ease.InOutQuart)
                .AsyncWaitForCompletion();
        }

        public Task MoveHorizontally(Transform transform, float endPoint)
        {
            return DOTween.Sequence()
                .Append(transform.DOMoveX(endPoint, _duration))
                .SetEase(Ease.InOutQuart)
                .AsyncWaitForCompletion();
        }

        public Task Move(Transform transform, Vector3 endPoint)
        {
            return DOTween.Sequence()
                .Append(transform.DOMove(endPoint, _duration))
                .SetEase(Ease.InOutQuart)
                .AsyncWaitForCompletion();
        }
    }
}
