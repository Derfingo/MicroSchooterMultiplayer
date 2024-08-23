using System;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.Buttons
{
    public class SimpleButton : UIElement, IPointerUpHandler
    {
        public event Action OnClick;

        public void OnPointerUp(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
    }
}
