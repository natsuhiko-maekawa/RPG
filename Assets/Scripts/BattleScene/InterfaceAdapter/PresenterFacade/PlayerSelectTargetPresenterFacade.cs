using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
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

        public void Output(CharacterEntity actor, SkillValueObject skill)
        {
            _infoView.StartAnimation(MessageCode.HorizontalSelectAndCancel);
            _targetView.StartAnimation(actor, skill);
        }

        public void Stop()
        {
            _infoView.StopAnimation();
            _targetView.StopAnimation();
        }
    }
}