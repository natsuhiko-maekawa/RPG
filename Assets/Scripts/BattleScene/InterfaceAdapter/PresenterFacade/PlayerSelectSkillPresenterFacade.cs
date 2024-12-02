using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.PresenterFacade
{
    public class PlayerSelectSkillPresenterFacade
    {
        private readonly InfoViewPresenter _infoView;
        private readonly SkillViewPresenter _skillView;

        public PlayerSelectSkillPresenterFacade(
            InfoViewPresenter infoView,
            SkillViewPresenter skillView)
        {
            _infoView = infoView;
            _skillView = skillView;
        }

        public void Output()
        {
            _infoView.StartAnimation(MessageCode.VerticalSelectAndCancel);
            _skillView.StartAnimation();
        }

        public void Stop()
        {
            _infoView.StopAnimation();
            _skillView.StopAnimation();
        }
    }
}