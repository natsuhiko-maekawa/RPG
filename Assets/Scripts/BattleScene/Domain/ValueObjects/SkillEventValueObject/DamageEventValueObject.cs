using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObjects.SkillEventValueObject
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