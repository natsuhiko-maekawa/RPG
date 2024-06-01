using System;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.View.AilmentView.OutputBoundary;
using BattleScene.UseCase.View.DestroyedPartView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputBoundary;

namespace BattleScene.UseCase.Event
{
    public class ResetSkillViewEvent : IEvent, IWait
    {
        private readonly IAilmentViewPresenter _ailmentViewPresenter;
        private readonly IDestroyedPartViewPresenter _destroyedPartViewPresenter;
        private readonly IMessageViewPresenter _messageViewPresenter;

        public ResetSkillViewEvent(
            IAilmentViewPresenter ailmentViewPresenter,
            IDestroyedPartViewPresenter destroyedPartViewPresenter,
            IMessageViewPresenter messageViewPresenter)
        {
            _ailmentViewPresenter = ailmentViewPresenter;
            _destroyedPartViewPresenter = destroyedPartViewPresenter;
            _messageViewPresenter = messageViewPresenter;
        }

        public EventCode Run()
        {
            throw new NotImplementedException();
        }

        public EventCode NextEvent()
        {
            return EventCode.SwitchSkillEvent;
        }
    }
}