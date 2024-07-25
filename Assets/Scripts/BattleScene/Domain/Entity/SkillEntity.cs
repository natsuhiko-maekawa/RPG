using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class SkillEntity : BaseEntity<SkillEntity, CharacterId>
    {
        // private ImmutableQueue<ISkillElement> _skillServiceQueue;

        // public SkillEntity(
        //     CharacterId id,
        //     SkillCode skillCode,
        //     ISkill abstractSkill)
        // {
        //     Id = id;
        //     SkillCode = skillCode;
        //     AbstractSkill = abstractSkill;
        //     _skillServiceQueue = ImmutableQueue.Create(abstractSkill.GetSkillService()
        //         .OrderBy(x => x)
        //         .ToArray());
        // }

        public SkillEntity(
            CharacterId id,
            SkillValueObject skill)
        {
            Id = id;
            Skill = skill;
        }

        public override CharacterId Id { get; }
        [Obsolete]
        public SkillCode SkillCode { get; }
        public SkillValueObject Skill { get; }

        // [Obsolete]
        // public ISkillElement FirstSkillService()
        // {
        //     // return _skillServiceQueue.FirstOrDefault();
        //     throw new NotImplementedException();
        // }

        // [Obsolete]
        // public ISkillElement DequeSkillElement()
        // {
        //     // if (_skillServiceQueue.IsEmpty) return null;
        //     // var skillService = _skillServiceQueue.Peek();
        //     // var newSkillServiceQueue = _skillServiceQueue.Dequeue();
        //     // _skillServiceQueue = newSkillServiceQueue;
        //     // return skillService;
        //     throw new NotImplementedException();
        // }
    }
}