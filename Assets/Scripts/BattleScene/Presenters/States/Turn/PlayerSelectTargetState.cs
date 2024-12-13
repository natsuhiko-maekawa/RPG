using System;
using System.Collections.Generic;
using BattleScene.Domain.Entities;
using BattleScene.Presenters.PresenterFacades;

namespace BattleScene.Presenters.States.Turn
{
    public class PlayerSelectTargetState : BaseState, ICancelable
    {
        private readonly SkillState _skillState;
        private readonly PlayerSelectTargetPresenterFacade _facade;

        public PlayerSelectTargetState(
            SkillState skillState,
            PlayerSelectTargetPresenterFacade facade)
        {
            _skillState = skillState;
            _facade = facade;
        }

        public override void Start()
        {
            if (Context.Skill == null) throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);
            _facade.Output(Context.Actor!, Context.Skill);
        }

        public override void Select(IReadOnlyList<CharacterEntity> targetList)
        {
            _facade.Stop();
            Context.TargetList = targetList;
            Context.TransitionTo(_skillState);
        }

        public void OnCancel()
        {
            _facade.Stop();
        }
    }
}