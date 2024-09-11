using System;
using System.Collections.Immutable;
using System.Linq;
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
        public AilmentCode AilmentCode { get; }
        public BodyPartCode DestroyedPart { get; }
        public int DestroyCount { get; }
        public BuffCode BuffCode { get; } = BuffCode.NoBuff;
        public ImmutableList<AttackValueObject> AttackList { get; }
        public int TechnicalPoint { get; }

        public BattleLogEntity(
            BattleLogId battleLogId,
            int sequence,
            int turn,
            AilmentValueObject ailment)
        {
            Id = battleLogId;
            Sequence = sequence;
            Turn = turn;
            ActionCode = ActionCode.Skill;
            ActorId = ailment.ActorId;
            TargetIdList = ailment.TargetIdList;
            SkillCode = ailment.SkillCode;
            AilmentCode = ailment.AilmentCode;
        }
        
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
            ActorId = damage.ActorId;
            TargetIdList = damage.TargetIdList;
            SkillCode = damage.SkillCode;
            AttackList = damage.AttackList;
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

        public int GetTotalDamageAmount()
        {
            return AttackList
                .Where(x => x.IsHit)
                .Select(x => x.Amount)
                .Sum();
        }

        public bool IsAvoid()
        {
            return AttackList.All(x => !x.IsHit);
        }
        
        public int CompareTo(BattleLogEntity other)
        {
            return Sequence - other.Sequence;
        }
    }
}