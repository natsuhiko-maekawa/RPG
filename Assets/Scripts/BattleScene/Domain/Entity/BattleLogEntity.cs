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
        public ActionCode ActionCode { get; }
        public ImmutableList<AttackValueObject> AttackList { get; }
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }
        public BuffCode BuffCode { get; } = BuffCode.NoBuff;

        public BattleLogEntity(
            BattleLogId battleLogId,
            int sequence,
            int turn,
            BuffValueObject buff)
        {
            Id = battleLogId;
            Sequence = sequence;
            Turn = turn;
            ActionCode = ActionCode.Skill;
            ActorId = buff.ActorId;
            TargetIdList = buff.TargetIdList;
            SkillCode = buff.SkillCode;
            BuffCode = buff.BuffCode;
        }
        
        public BattleLogEntity(
            BattleLogId battleLogId,
            int sequence,
            int turn,
            DamageValueObject damage)
        {
            Id = battleLogId;
            Sequence = sequence;
            Turn = turn;
            ActionCode = ActionCode.Skill;
            AttackList = damage.AttackList;
            ActorId = damage.ActorId;
            SkillCode = damage.SkillCode;
            TargetIdList = damage.TargetIdList;
        }

        public int CompareTo(BattleLogEntity other)
        {
            return Sequence - other.Sequence;
        }
    }
}