using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using Utility;

namespace BattleScene.Domain.Entity
{
    public class BattleEventEntity : BaseEntity<BattleEventId>, IComparable<BattleEventEntity>
    {
        public override BattleEventId Id { get; }
        public int Sequence { get; }
        public int Turn { get; }
        public BattleEventCode BattleEventCode { get; private set; } = BattleEventCode.NoEvent;
        public CharacterId? ActorId { get; }
        public SkillCode SkillCode { get; private set; } = SkillCode.NoSkill;
        public IReadOnlyList<CharacterId> TargetIdList { get; private set; } = Array.Empty<CharacterId>();
        public IReadOnlyList<CharacterId> ActualTargetIdList { get; private set; } = Array.Empty<CharacterId>();
        public bool IsFailure { get; private set; }
        public AilmentCode AilmentCode { get; private set; }
        public BodyPartCode DestroyedPart { get; private set; } = BodyPartCode.NoBodyPart;
        public int DestroyCount { get; private set; }
        public BuffCode BuffCode { get; private set; } = BuffCode.NoBuff;
        public IReadOnlyList<AttackValueObject> AttackList { get; private set; } = Array.Empty<AttackValueObject>();
        public IReadOnlyList<CuringValueObject> CuringList { get; private set; } = Array.Empty<CuringValueObject>();
        public EnhanceCode EnhanceCode { get; private set; }
        public int TechnicalPoint { get; private set; }
        public SlipCode SlipCode { get; private set; }
        public float Rate { get; private set; } = 1.0f;
        public int BuffTurn { get; private set; }
        public LifetimeCode LifetimeCode { get; private set; } = LifetimeCode.NoLifetime;

        public BattleEventEntity(
            BattleEventId battleEventId,
            int sequence,
            int turn,
            CharacterId? actorId = null,
            AilmentCode ailmentCode = AilmentCode.NoAilment,
            SlipCode slipCode = SlipCode.NoSlip)
        {
            Id = battleEventId;
            Sequence = sequence;
            Turn = turn;
            ActorId = actorId;
            AilmentCode = ailmentCode;
            SlipCode = slipCode;
        }

        public void UpdateSkill(SkillCode skillCode)
        {
            SkillCode = skillCode;
        }

        public void UpdateAilment(
            AilmentCode ailmentCode,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList)
        {
            AilmentCode = ailmentCode;
            TargetIdList = targetIdList;
            ActualTargetIdList = actualTargetIdList;
        }

        public void UpdateBuff(
            BuffCode buffCode,
            int turn,
            float rate,
            LifetimeCode lifetimeCode,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList)
        {
            BuffCode = buffCode;
            BuffTurn = turn;
            Rate = rate;
            LifetimeCode = lifetimeCode;
            TargetIdList = targetIdList;
            ActualTargetIdList = actualTargetIdList;
        }

        public void UpdateCure(
            IReadOnlyList<CuringValueObject> curingList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            CuringList = curingList;
            TargetIdList = targetIdList;
            ActualTargetIdList = targetIdList;
        }

        public void UpdateDamage(
            IReadOnlyList<AttackValueObject> attackList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            AttackList = attackList;
            TargetIdList = targetIdList;
            ActualTargetIdList = targetIdList;
        }

        public void UpdateDestroy(
            BodyPartCode destroyedPart,
            int destroyCount,
            IReadOnlyList<CharacterId> targetIdList)
        {
            DestroyedPart = destroyedPart;
            DestroyCount = destroyCount;
            TargetIdList = targetIdList;
            ActualTargetIdList = targetIdList;
        }

        public void UpdateSlipDamage(
            IReadOnlyList<AttackValueObject> attackList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            MyDebug.Assert(SlipCode != SlipCode.NoSlip);
            AttackList = attackList;
            TargetIdList = targetIdList;
            ActualTargetIdList = targetIdList;
        }

        public int CompareTo(BattleEventEntity other)
        {
            return Sequence - other.Sequence;
        }
    }
}