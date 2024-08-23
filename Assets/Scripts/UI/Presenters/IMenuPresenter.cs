using Assets.Scripts.UI.Buttons;
using Assets.Scripts.UI.Panels;

namespace Assets.Scripts.UI.Presenters
{
    public interface IMenuPresenter
    {
        MainPanel MainPanel { get; }
        MenuButton BackMenuButton { get; }
    }
}
