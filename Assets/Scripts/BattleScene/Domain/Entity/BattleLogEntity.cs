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
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }
        public BodyPartCode DestroyedPart { get; }
        public int DestroyCount { get; }
        public BuffCode BuffCode { get; } = BuffCode.NoBuff;
        public ImmutableList<AttackValueObject> AttackList { get; }
        public int TechnicalPoint { get; }

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
            // TODO: 並べ替える
            AttackList = damage.AttackList;
            ActorId = damage.ActorId;
            SkillCode = damage.SkillCode;
            TargetIdList = damage.TargetIdList;
        }

        public BattleLogEntity(
            BattleLogId battleLogId,
            int sequence,
            int turn,
            DestroyedPartValueObject destroyedPart)
        {
            Id = battleLogId;
            Sequence = sequence;
            Turn = turn;
            ActionCode = ActionCode.Skill;
            ActorId = destroyedPart.ActorId;
            TargetIdList = destroyedPart.TargetIdList;
            SkillCode = destroyedPart.SkillCode;
            DestroyedPart = destroyedPart.BodyPartCode;
            DestroyCount = destroyedPart.DestroyCount;
        }

        public BattleLogEntity(
            BattleLogId battleLogId,
            int sequence,
            int turn,
            RestoreValueObject restore)
        {
            Id = battleLogId;
            Sequence = sequence;
            Turn = turn;
            ActionCode = ActionCode.Skill;
            ActorId = restore.ActorId;
            TargetIdList = restore.TargetIdList;
            SkillCode = restore.SkillCode;
            TechnicalPoint = restore.TechnicalPoint;
        }

        public int CompareTo(BattleLogEntity other)
        {
            return Sequence - other.Sequence;
        }
    }
}