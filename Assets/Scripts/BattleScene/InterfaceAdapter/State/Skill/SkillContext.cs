using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class SkillContext<TPrimeSkillParameter, TPrimeSkill> : ISkillContext
    {
        private AbstractSkillState<TPrimeSkillParameter, TPrimeSkill> _skillState;
        
        public SkillCommonValueObject SkillCommon { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<TPrimeSkillParameter> PrimeSkillParameterList { get; }
        public IReadOnlyList<TPrimeSkill> PrimeSkillList { get; set; }

        public SkillContext(
            AbstractSkillState<TPrimeSkillParameter, TPrimeSkill> primeSkillState,
            SkillCommonValueObject skillCommon ,
            IReadOnlyList<CharacterId> targetIdList ,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList)
        {
            SkillCommon = skillCommon;
            TargetIdList = targetIdList;
            PrimeSkillParameterList = primeSkillParameterList;
            TransitionTo(primeSkillState);
        }

        public void TransitionTo(AbstractSkillState<TPrimeSkillParameter, TPrimeSkill> skillState)
        {
            Debug.Log(skillState.GetType().Name);
            _skillState = skillState;
            _skillState.SetContext(this);
            _skillState.Start();
        }

        public void Select() => _skillState.Select();

        public bool HasEndState() => _skillState is IPrimeSkillStopState;
    }
}