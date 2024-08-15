using System;
using System.Collections.Generic;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.StateMachine;

namespace BattleScene.UseCases.Event
{
    internal class SkillQueue : Queue<Func<StateCode>>
    {
        private readonly Queue<Func<StateCode>> _skillQueue = new();
        
        public SkillQueue(
            SkillValueObject skill,
            AilmentSkillService ailmentSkill)
        {
            foreach (var ailment in skill.AilmentList)
            {
                _skillQueue.Enqueue(() => { ailmentSkill.Execute(skill, ailment);
                    return StateCode.Ailment;
                });
                
            }
        }

        public StateCode Invoke()
        {
            return _skillQueue.Dequeue().Invoke();
        }
    }
}