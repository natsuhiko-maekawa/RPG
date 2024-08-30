using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class BattleLogEntity : BaseEntity<BattleLogEntity, BattleLogId>, IComparable<BattleLogEntity>
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
            DamageValueObject damage)
        {
            Id = battleLogId;
            Sequence = sequence;
            Turn = turn;
            AttackList = damage.AttackList.ToImmutableList();
            ActorId = damage.ActorId;
            SkillCode = damage.SkillCode;
            TargetIdList = damage.TargetIdList.ToImmutableList();
        }

        public int CompareTo(BattleLogEntity other)
        {
            return Sequence - other.Sequence;
        }
    }
}