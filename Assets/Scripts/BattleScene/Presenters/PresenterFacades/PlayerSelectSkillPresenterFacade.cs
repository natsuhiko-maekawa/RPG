using BattleScene.Domain.Codes;
using BattleScene.Presenters.Presenters;

namespace BattleScene.Presenters.PresenterFacades
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

        public void Output(SkillCode[] skillCodeArray)
        {
            _infoView.StartAnimation(MessageCode.VerticalSelectAndCancel);
            _skillView.StartAnimation(skillCodeArray);
        }

        public void Stop()
        {
            _infoView.StopAnimation();
            _skillView.StopAnimation();
        }
    }
}