using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.StateMachine;

namespace BattleScene.UseCases.Event
{
    internal class SkillIterator
    {
        private readonly AilmentSkillService _ailmentSkill;
        private SkillValueObject _skill;

        private Func<StateCode> _skillDelegate;
        private List<Func<StateCode>> _skillDelegateList;


        public SkillIterator(
            AilmentSkillService ailmentSkill)
        {
            _ailmentSkill = ailmentSkill;
        }

        public void SetSkill(SkillValueObject skill)
        {
            _skill = skill;
        }
        
        public bool TryExecuteNext(out StateCode stateCode)
        {
            if (SkillEnumerator().MoveNext())
            {
                stateCode = SkillEnumerator().Current;
                return true;
            }
            
            stateCode = StateCode.NoState;
            return false;
        }
        
        private IEnumerator<StateCode> SkillEnumerator()
        {
            yield return StateCode.Damage;

            if (_skill == null) yield break;
            
            foreach (var ailment in _skill.AilmentList)
            {
                // _ailmentSkill.Execute(_skill, ailment);
                yield return StateCode.Ailment;
            }

            foreach (var buff in _skill.BuffList)
            {
                // Execute
                yield return StateCode.Buff;
            }

            foreach (var damage in _skill.DamageList)
            {
                // Execute
                yield return StateCode.Damage;
            }
        }
    }
}