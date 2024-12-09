using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject
{
    public class DamageEventValueObject : ISkillEventValueObject // 24byte
    {
        public SkillEventCode SkillEventCode { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<CharacterId> ActualTargetIdList => TargetIdList;
        public IReadOnlyList<AttackValueObject> AttackList { get; }

        public DamageEventValueObject(
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<AttackValueObject> attackList)
        {
            SkillEventCode = SkillEventCode.Damage;
            TargetIdList = targetIdList;
            AttackList = attackList;
        }
    }
}