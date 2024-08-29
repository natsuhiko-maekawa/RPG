using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class BattleLogEntity : BaseEntity<BattleLogEntity, BattleLogId>
    {
        public override BattleLogId Id { get; }
        public int Sequence { get; }
        public int Turn { get; }
        public ImmutableList<AttackValueObject> AttackList { get; }
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }

        public BattleLogEntity(
            BattleLogId battleLogId,
            int sequence,
            int turn,
            IList<AttackValueObject> attackList,
            CharacterId actorId,
            SkillCode skillCode,
            IList<CharacterId> targetIdList)
        {
            Id = battleLogId;
            Sequence = sequence;
            Turn = turn;
            AttackList = attackList.ToImmutableList();
            ActorId = actorId;
            SkillCode = skillCode;
            TargetIdList = targetIdList.ToImmutableList();
        }
    }
}