using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.PresenterFacade
{
    public class PlayerSelectTargetPresenterFacade
    {
        private readonly InfoViewPresenter _infoView;
        private readonly TargetViewPresenter _targetView;

        public PlayerSelectTargetPresenterFacade(
            InfoViewPresenter infoView,
            TargetViewPresenter targetView)
        {
            _infoView = infoView;
            _targetView = targetView;
        }

        public void Output(CharacterId actorId, SkillValueObject skill)
        {
            _infoView.StartAnimation(MessageCode.HorizontalSelectAndCancel);
            _targetView.StartAnimation(actorId, skill);
        }

        public void Stop()
        {
            _infoView.StopAnimation();
            _targetView.StopAnimation();
        }
    }
}