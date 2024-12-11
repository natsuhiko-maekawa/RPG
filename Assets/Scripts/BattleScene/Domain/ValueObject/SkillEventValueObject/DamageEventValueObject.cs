using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject
{
    public class DamageEventValueObject : ISkillEventValueObject // 24byte
    {
        public SkillEventCode SkillEventCode { get; }
        public IReadOnlyList<CharacterEntity> TargetList { get; }
        public IReadOnlyList<AttackValueObject> AttackList { get; }

        public DamageEventValueObject(
            IReadOnlyList<CharacterEntity> targetList,
            IReadOnlyList<AttackValueObject> attackList)
        {
            SkillEventCode = SkillEventCode.Damage;
            TargetList = targetList;
            AttackList = attackList;
        }
    }
}