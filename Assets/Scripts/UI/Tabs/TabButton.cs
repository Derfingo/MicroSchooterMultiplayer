using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.Tabs
{
    public class TabButton : UIElement, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private TabType _tabType;

        public event Action OnTabEnter;
        public event Action OnTabExit;
        public event Action<TabType> OnTabClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnTabClick?.Invoke(_tabType);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnTabEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnTabExit?.Invoke();
        }
    }

    public enum TabType
    {
        Graphics,
        Control,
        Sound
    }
}
