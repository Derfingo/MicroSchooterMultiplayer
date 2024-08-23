using Assets.Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI.Panels
{
    public class TeamVsTeamPanel : UIPanel
    {
        [SerializeField] private SimpleButton _enterFirstTeamButton;
        [SerializeField] private SimpleButton _enterSecondTeamButton;

        [SerializeField] private ScrollView _firstTeamScrollView;
        [SerializeField] private ScrollView _secondTeamScrollView;

        public SimpleButton EnterFirstTeamButton { get => _enterFirstTeamButton; set => _enterFirstTeamButton = value; }
        public SimpleButton EnterSecondTeamButton { get => _enterSecondTeamButton; set => _enterSecondTeamButton = value; }
        public ScrollView FirstTeamScrollView { get => _firstTeamScrollView; set => _firstTeamScrollView = value; }
        public ScrollView SecondTeamScrollView { get => _secondTeamScrollView; set => _secondTeamScrollView = value; }
    }
}
