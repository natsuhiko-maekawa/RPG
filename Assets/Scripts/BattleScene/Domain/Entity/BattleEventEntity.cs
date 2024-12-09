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
        // TODO: 以下のプロパティをActorInTurn (OrderedItem)に置換すること。
        public CharacterId? ActorId { get; }
        public AilmentCode AilmentCode { get; private set; }
        public SlipCode SlipCode { get; private set; }

        public SkillCode SkillCode { get; private set; } = SkillCode.NoSkill;

        [Obsolete] public IReadOnlyList<CharacterId> TargetIdList { get; private set; } = Array.Empty<CharacterId>();
        [Obsolete] public IReadOnlyList<CharacterId> ActualTargetIdList { get; private set; } = Array.Empty<CharacterId>();
        [Obsolete] public bool IsFailure => ActualTargetIdList.Count == 0;
        [Obsolete] public BodyPartCode DestroyedPart { get; private set; } = BodyPartCode.NoBodyPart;
        [Obsolete] public int DestroyCount { get; private set; }
        [Obsolete] public BuffCode BuffCode { get; private set; } = BuffCode.NoBuff;
        [Obsolete] public IReadOnlyList<AttackValueObject> AttackList { get; private set; } = Array.Empty<AttackValueObject>();
        [Obsolete] public IReadOnlyList<CuringValueObject> CuringList { get; private set; } = Array.Empty<CuringValueObject>();
        [Obsolete] public EnhanceCode EnhanceCode { get; private set; }
        [Obsolete] public IReadOnlyList<AilmentCode> ResetAilmentCodeList { get; private set; } = Array.Empty<AilmentCode>();
        [Obsolete] public IReadOnlyList<BodyPartCode> ResetBodyPartCodeList { get; private set; } = Array.Empty<BodyPartCode>();
        [Obsolete] public IReadOnlyList<SlipCode> ResetSlipCodeList { get; private set; } = Array.Empty<SlipCode>();
        [Obsolete] public int TechnicalPoint { get; private set; }
        [Obsolete] public float Rate { get; private set; } = 1.0f;
        [Obsolete] public int EffectTurn { get; private set; }
        [Obsolete] public LifetimeCode LifetimeCode { get; private set; } = LifetimeCode.NoLifetime;

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
            BattleEventCode = BattleEventCode.Skill;
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
            int effectTurn,
            float rate,
            LifetimeCode lifetimeCode,
            IReadOnlyList<CharacterId> targetIdList)
        {
            BuffCode = buffCode;
            EffectTurn = effectTurn;
            Rate = rate;
            LifetimeCode = lifetimeCode;
            TargetIdList = targetIdList;
            ActualTargetIdList = targetIdList;
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

        public void UpdateEnhance(
            EnhanceCode enhanceCode,
            int effectTurn,
            LifetimeCode lifetimeCode,
            IReadOnlyList<CharacterId> targetIdList)
        {
            EnhanceCode = enhanceCode;
            EffectTurn = effectTurn;
            LifetimeCode = lifetimeCode;
            TargetIdList = targetIdList;
            ActualTargetIdList = targetIdList;
        }

        public void UpdateReset(
            IReadOnlyList<AilmentCode> resetAilmentCodeList, 
            IReadOnlyList<BodyPartCode> resetBodyPartCodeList, 
            IReadOnlyList<SlipCode> resetSlipCodeList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            ResetAilmentCodeList = resetAilmentCodeList;
            ResetBodyPartCodeList = resetBodyPartCodeList;
            ResetSlipCodeList = resetSlipCodeList;
            TargetIdList = targetIdList;
            ActualTargetIdList = targetIdList;
        }

        public void UpdateRestore(
            int technicalPoint,
            IReadOnlyList<CharacterId> targetIdList)
        {
            TechnicalPoint = technicalPoint;
            TargetIdList = targetIdList;
            ActualTargetIdList = targetIdList;
        }

        public void UpdateSlip(
            SlipCode slipCode,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList)
        {
            SlipCode = slipCode;
            TargetIdList = targetIdList;
            ActualTargetIdList = actualTargetIdList;
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

        public override string ToString()
        {
            var value = $@"BattleEventEntity
  Sequence: {Sequence}
  Turn: {Turn}
  Type: {GetEntityType()}";
            return value;
        }

        public int CompareTo(BattleEventEntity other)
        {
            return Sequence - other.Sequence;
        }

        private string GetEntityType()
        {
            if (ActorId is not null) return $"Character ({ActorId})";
            if (AilmentCode != AilmentCode.NoAilment) return AilmentCode.ToString();
            if (SlipCode != SlipCode.NoSlip) return SlipCode.ToString();
            return string.Empty;
        }
    }
}