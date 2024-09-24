using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class Context<TPrimeSkillParameter, TPrimeSkill> : IContext
    {
        private BaseState<TPrimeSkillParameter, TPrimeSkill> _state;
        
        public SkillCommonValueObject SkillCommon { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<TPrimeSkillParameter> PrimeSkillParameterList { get; }
        public Queue<TPrimeSkill> PrimeSkillQueue { get; set; }

        public Context(
            BaseState<TPrimeSkillParameter, TPrimeSkill> primeSkillState,
            SkillCommonValueObject skillCommon ,
            IReadOnlyList<CharacterId> targetIdList ,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList)
        {
            SkillCommon = skillCommon;
            TargetIdList = targetIdList;
            PrimeSkillParameterList = primeSkillParameterList;
            TransitionTo(primeSkillState);
        }

        public void TransitionTo(BaseState<TPrimeSkillParameter, TPrimeSkill> skillState)
        {
            Debug.Log(skillState.GetType().Name);
            _state = skillState;
            _state.SetContext(this);
            _state.Start();
        }

        public void Select() => _state.Select();

        public bool IsContinue => _state is not IPrimeSkillStopState and not IPrimeSkillBreakState;

        public bool IsBreak => _state is IPrimeSkillBreakState;
    }
}