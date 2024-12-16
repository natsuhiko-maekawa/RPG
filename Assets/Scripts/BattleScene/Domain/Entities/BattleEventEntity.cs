using System;
using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;
using Utility;

namespace BattleScene.Domain.Entities
{
    public class BattleEventEntity : BaseEntity<BattleEventId>, IComparable<BattleEventEntity>
    {
        public override BattleEventId Id { get; }
        public int Sequence { get; }
        public int Turn { get; }

        public BattleEventCode BattleEventCode { get; private set; } = BattleEventCode.NoEvent;

        // TODO: 以下のプロパティをActorInTurnに置換すること。
        public CharacterEntity? Actor { get; }
        public AilmentCode AilmentCode { get; private set; }
        public SlipCode SlipCode { get; private set; }

        // TODO: 以下のプロパティをISkillEventValueObject型のプロパティに統一すること。
        // レビュー会までに間に合わないと判断したため、後回しにしている。
        /*[Obsolete] */public SkillCode SkillCode { get; private set; } = SkillCode.NoSkill;
        /*[Obsolete] */public IReadOnlyList<CharacterEntity> TargetList { get; private set; } = Array.Empty<CharacterEntity>();
        /*[Obsolete] */public IReadOnlyList<CharacterEntity> ActualTargetList { get; private set; } = Array.Empty<CharacterEntity>();
        /*[Obsolete] */public bool IsFailure => ActualTargetList.Count == 0;
        /*[Obsolete] */public BodyPartCode DestroyedPart { get; private set; } = BodyPartCode.NoBodyPart;
        /*[Obsolete] */public int DestroyCount { get; private set; }
        /*[Obsolete] */public BuffCode BuffCode { get; private set; } = BuffCode.NoBuff;
        /*[Obsolete] */public IReadOnlyList<AttackValueObject> AttackList { get; private set; } = Array.Empty<AttackValueObject>();
        /*[Obsolete] */public IReadOnlyList<CuringValueObject> CuringList { get; private set; } = Array.Empty<CuringValueObject>();
        /*[Obsolete] */public EnhanceCode EnhanceCode { get; private set; }
        /*[Obsolete] */public IReadOnlyList<AilmentCode> ResetAilmentCodeList { get; private set; } = Array.Empty<AilmentCode>();
        /*[Obsolete] */public IReadOnlyList<BodyPartCode> ResetBodyPartCodeList { get; private set; } = Array.Empty<BodyPartCode>();
        /*[Obsolete] */public IReadOnlyList<SlipCode> ResetSlipCodeList { get; private set; } = Array.Empty<SlipCode>();
        /*[Obsolete] */public int TechnicalPoint { get; private set; }
        /*[Obsolete] */public float Rate { get; private set; } = 1.0f;
        /*[Obsolete] */public int EffectTurn { get; private set; }
        /*[Obsolete] */public LifetimeCode LifetimeCode { get; private set; } = LifetimeCode.NoLifetime;

        public BattleEventEntity(
            BattleEventId battleEventId,
            int sequence,
            int turn,
            CharacterEntity? actor = null,
            AilmentCode ailmentCode = AilmentCode.NoAilment,
            SlipCode slipCode = SlipCode.NoSlip)
        {
            Id = battleEventId;
            Sequence = sequence;
            Turn = turn;
            Actor = actor;
            AilmentCode = ailmentCode;
            SlipCode = slipCode;
        }

        public void UpdateSkill(SkillCode skillCode, BattleEventCode battleEventCode)
        {
            SkillCode = skillCode;
            BattleEventCode = battleEventCode;
        }

        public void UpdateAilment(
            AilmentCode ailmentCode,
            IReadOnlyList<CharacterEntity> targetList,
            IReadOnlyList<CharacterEntity> actualTargetList)
        {
            AilmentCode = ailmentCode;
            TargetList = targetList;
            ActualTargetList = actualTargetList;
        }

        public void UpdateBuff(
            BuffCode buffCode,
            int effectTurn,
            float rate,
            LifetimeCode lifetimeCode,
            IReadOnlyList<CharacterEntity> targetList)
        {
            BuffCode = buffCode;
            EffectTurn = effectTurn;
            Rate = rate;
            LifetimeCode = lifetimeCode;
            TargetList = targetList;
            ActualTargetList = targetList;
        }

        public void UpdateCure(
            IReadOnlyList<CuringValueObject> curingList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            CuringList = curingList;
            TargetList = targetList;
            ActualTargetList = targetList;
        }

        public void UpdateDamage(
            IReadOnlyList<AttackValueObject> attackList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            AttackList = attackList;
            TargetList = targetList;
            ActualTargetList = targetList;
        }

        public void UpdateDestroy(
            BodyPartCode destroyedPart,
            int destroyCount,
            IReadOnlyList<CharacterEntity> targetList)
        {
            DestroyedPart = destroyedPart;
            DestroyCount = destroyCount;
            TargetList = targetList;
        }

        public void UpdateEnhance(
            EnhanceCode enhanceCode,
            int effectTurn,
            LifetimeCode lifetimeCode,
            IReadOnlyList<CharacterEntity> targetList)
        {
            EnhanceCode = enhanceCode;
            EffectTurn = effectTurn;
            LifetimeCode = lifetimeCode;
            TargetList = targetList;
            ActualTargetList = targetList;
        }

        public void UpdateReset(
            IReadOnlyList<AilmentCode> resetAilmentCodeList,
            IReadOnlyList<BodyPartCode> resetBodyPartCodeList,
            IReadOnlyList<SlipCode> resetSlipCodeList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            ResetAilmentCodeList = resetAilmentCodeList;
            ResetBodyPartCodeList = resetBodyPartCodeList;
            ResetSlipCodeList = resetSlipCodeList;
            TargetList = targetList;
            ActualTargetList = targetList;
        }

        public void UpdateRestore(
            int technicalPoint,
            IReadOnlyList<CharacterEntity> targetList)
        {
            TechnicalPoint = technicalPoint;
            TargetList = targetList;
            ActualTargetList = targetList;
        }

        public void UpdateSlip(
            SlipCode slipCode,
            IReadOnlyList<CharacterEntity> targetList,
            IReadOnlyList<CharacterEntity> actualTargetList)
        {
            SlipCode = slipCode;
            TargetList = targetList;
            ActualTargetList = actualTargetList;
        }

        public void UpdateSlipDamage(
            IReadOnlyList<AttackValueObject> attackList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            MyDebug.Assert(SlipCode != SlipCode.NoSlip);
            AttackList = attackList;
            TargetList = targetList;
            ActualTargetList = targetList;
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
            if (Actor is not null) return $"Character ({Actor})";
            if (AilmentCode != AilmentCode.NoAilment) return AilmentCode.ToString();
            if (SlipCode != SlipCode.NoSlip) return SlipCode.ToString();
            return string.Empty;
        }
    }
}