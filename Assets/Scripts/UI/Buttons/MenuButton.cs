using Assets.Scripts.UI.Transition;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.Buttons
{
    public class MenuButton : UIElement, IPointerUpHandler
    {
        [SerializeField] private PanelType _panelType;

        public event Action<PanelType> OnClick;

        public void OnPointerUp(PointerEventData eventData)
        {
            OnClick?.Invoke(_panelType);
        }
    }
}
