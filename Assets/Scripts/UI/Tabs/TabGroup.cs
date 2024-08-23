using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Tabs
{
    public class TabGroup : UIElement
    {
        [SerializeField] private TabButton[] _tabButtons;
        [SerializeField] private UIPanel _graphicsTab;
        [SerializeField] private UIPanel _controlTab;
        [SerializeField] private UIPanel _soundTab;

        private UIPanel _currentTab;
        private Dictionary<TabType, UIPanel> _tabs;

        private void Start()
        {
            AddListeners();
            SetTabs();
            SetDefaultTab();
        }

        private void SetTabs()
        {
            _tabs = new Dictionary<TabType, UIPanel>()
            {
                { TabType.Graphics, _graphicsTab },
                { TabType.Control, _controlTab },
                { TabType.Sound, _soundTab }
            };
        }

        private void SetDefaultTab()
        {
            _currentTab = _tabs[TabType.Graphics];
            _tabs[TabType.Control].Hide();
            _tabs[TabType.Sound].Hide();
            _currentTab.Show();
        }

        private void OnTabButtonClick(TabType type)
        {
            if (_tabs.ContainsKey(type))
            {
                _currentTab.Hide();
                UIPanel tab = _tabs[type];
                _currentTab = tab;
                _currentTab.Show();
            }
        }

        private void AddListeners()
        {
            for (int i = 0; i < _tabButtons.Length; i++)
            {
                _tabButtons[i].OnTabClick += OnTabButtonClick;
            }
        }
    }
}
