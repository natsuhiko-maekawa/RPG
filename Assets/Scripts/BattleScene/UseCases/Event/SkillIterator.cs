using System.Collections.Generic;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.StateMachine;

namespace BattleScene.UseCases.Event
{
    public class SkillIterator
    {
        private readonly AilmentSkillService _ailmentSkill;
        private SkillValueObject _skill;

        public void SetSkill(SkillValueObject skill)
        {
            _skill = skill;
        }
        
        public bool TryExecuteNext(out StateCode stateCode)
        {
            stateCode = SkillEnumerator().Current;
            return SkillEnumerator().MoveNext();
        }
        
        private IEnumerator<StateCode> SkillEnumerator()
        {
            if (_skill == null) yield break;
            
            foreach (var ailment in _skill.AilmentList)
            {
                _ailmentSkill.Execute(_skill, ailment);
                yield return StateCode.Ailment;
            }

            foreach (var buff in _skill.BuffList)
            {
                // Execute
                yield return StateCode.Buff;
            }
        }
    }
}