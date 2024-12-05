using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class BattleEventEntity : BaseEntity<BattleEventId>, IComparable<BattleEventEntity>
    {
        public override BattleEventId Id { get; }
        public int Sequence { get; }
        public int Turn { get; }
        private BattleEventValueObject? _battleEvent;
        public BattleEventCode BattleEventCode => _battleEvent?.BattleEventCode ?? BattleEventCode.NoAction;
        public CharacterId? ActorId { get; }
        public SkillCode SkillCode { get; private set; }
        public IReadOnlyList<CharacterId> TargetIdList => _battleEvent?.TargetIdList ?? Array.Empty<CharacterId>();
        public IReadOnlyList<CharacterId> ActualTargetIdList
            => _battleEvent?.ActualTargetIdList ?? Array.Empty<CharacterId>();
        public bool IsFailure => _battleEvent?.IsFailure ?? false;
        public AilmentCode AilmentCode { get; private set; }
        public BodyPartCode DestroyedPart => _battleEvent?.BodyPartCode ?? BodyPartCode.NoBodyPart;
        public int DestroyCount => _battleEvent?.DestroyCount ?? 0;
        public BuffCode BuffCode => _battleEvent?.BuffCode ?? BuffCode.NoBuff;
        public IReadOnlyList<AttackValueObject> AttackList
            => _battleEvent?.AttackList ?? Array.Empty<AttackValueObject>();
        public IReadOnlyList<CuringValueObject> CuringList
            => _battleEvent?.CuringList ?? Array.Empty<CuringValueObject>();
        public int TechnicalPoint => _battleEvent?.TechnicalPoint ?? 0;
        public SlipCode SlipCode { get; private set; }

        public BattleEventEntity(
            BattleEventId battleEventId,
            int sequence,
            int turn,
            CharacterId? actorId,
            AilmentCode ailmentCode,
            SlipCode slipCode)
        {
            Id = battleEventId;
            Sequence = sequence;
            Turn = turn;
            ActorId = actorId;
            AilmentCode = ailmentCode;
            SlipCode = slipCode;
        }

        public void Update(SkillCode skillCode)
        {
            SkillCode = skillCode;
        }

        // TODO: 以下のメソッドの内容はBattleEventValueObjectを削除した際に同時に削除すること
        public void Update(BattleEventValueObject battleEvent)
        {
            _battleEvent = battleEvent;
            AilmentCode = _battleEvent.AilmentCode != AilmentCode.NoAilment
                ? _battleEvent.AilmentCode
                : AilmentCode;
            SlipCode = _battleEvent.SlipCode != SlipCode.NoSlip
                ? _battleEvent.SlipCode
                : SlipCode;
            SkillCode = _battleEvent.SkillCode != SkillCode.NoSkill
                ? _battleEvent.SkillCode
                : SkillCode;
        }

        public int CompareTo(BattleEventEntity other)
        {
            return Sequence - other.Sequence;
        }
    }
}