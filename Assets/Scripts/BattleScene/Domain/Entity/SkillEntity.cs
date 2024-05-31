using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Entity
{
    public class SkillEntity
    {
        private ImmutableQueue<ISkillElement> _skillServiceQueue;

        public SkillEntity(
            CharacterId characterId,
            SkillCode skillCode,
            AbstractSkill abstractSkill)
        {
            CharacterId = characterId;
            SkillCode = skillCode;
            AbstractSkill = abstractSkill;
            _skillServiceQueue = ImmutableQueue.Create(abstractSkill.GetSkillService()
                .OrderBy(x => x)
                .ToArray());
        }

        public CharacterId CharacterId { get; }
        public SkillCode SkillCode { get; }
        public AbstractSkill AbstractSkill { get; }

        public ISkillElement FirstSkillService()
        {
            return _skillServiceQueue.FirstOrDefault();
        }

        public ISkillElement DequeSkillElement()
        {
            if (_skillServiceQueue.IsEmpty) return null;
            var skillService = _skillServiceQueue.Peek();
            var newSkillServiceQueue = _skillServiceQueue.Dequeue();
            _skillServiceQueue = newSkillServiceQueue;
            return skillService;
        }
    }
}