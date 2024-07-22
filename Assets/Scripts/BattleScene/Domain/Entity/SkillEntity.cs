using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Entity
{
    public class SkillEntity : BaseEntity<SkillEntity, CharacterId>
    {
        private ImmutableQueue<ISkillElement> _skillServiceQueue;

        public SkillEntity(
            CharacterId id,
            SkillCode skillCode,
            ISkill abstractSkill)
        {
            Id = id;
            SkillCode = skillCode;
            AbstractSkill = abstractSkill;
            _skillServiceQueue = ImmutableQueue.Create(abstractSkill.GetSkillService()
                .OrderBy(x => x)
                .ToArray());
        }

        public override CharacterId Id { get; }
        public SkillCode SkillCode { get; }
        public ISkill AbstractSkill { get; }

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