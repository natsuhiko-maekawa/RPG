using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.Event
{
    internal class SkillQueueFactory
    {
        private readonly AilmentSkillService _ailmentSkill;

        public SkillQueueFactory(
            AilmentSkillService ailmentSkill)
        {
            _ailmentSkill = ailmentSkill;
        }

        public SkillQueue Create(SkillValueObject skill)
            => new SkillQueue(
                skill,
                _ailmentSkill);
    }
}