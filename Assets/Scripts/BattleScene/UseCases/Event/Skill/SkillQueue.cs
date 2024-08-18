using System;
using System.Collections.Generic;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.Event.Skill
{
    internal class SkillQueue
    {
        private readonly Queue<Func<SkillStateCode>> _skillQueue = new();
        
        public SkillQueue(
            SkillValueObject skill,
            AilmentSkillService ailmentSkill)
        {
            foreach (var ailment in skill.AilmentList)
            {
                _skillQueue.Enqueue(() => { 
                    ailmentSkill.Execute(skill, ailment);
                    return SkillStateCode.Ailment;
                });
            }
            
            _skillQueue.Enqueue(() => SkillStateCode.NoSkill);
        }

        public SkillStateCode Invoke()
        {
            return _skillQueue.Dequeue().Invoke();
        }
    }
}