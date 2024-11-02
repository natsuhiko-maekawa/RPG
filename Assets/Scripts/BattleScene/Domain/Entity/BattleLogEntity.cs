using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class BattleLogEntity : BaseEntity<BattleLogId>, IComparable<BattleLogEntity>
    {
        public override BattleLogId Id { get; }
        public int Sequence { get; }
        public int Turn { get; }
        private readonly BattleEventValueObject _battleEvent;
        public BattleEventCode BattleEventCode => _battleEvent.BattleEventCode;
        public CharacterId? ActorId => _battleEvent.ActorId;
        public SkillCode SkillCode => _battleEvent.SkillCode;
        public IReadOnlyList<CharacterId> TargetIdList => _battleEvent.TargetIdList;
        public IReadOnlyList<CharacterId> ActualTargetIdList => _battleEvent.ActualTargetIdList;
        public bool IsFailure => _battleEvent.IsFailure;
        public AilmentCode AilmentCode => _battleEvent.AilmentCode;
        public BodyPartCode DestroyedPart => _battleEvent.BodyPartCode;
        public int DestroyCount => _battleEvent.DestroyCount;
        public BuffCode BuffCode => _battleEvent.BuffCode;
        public IReadOnlyList<AttackValueObject> AttackList => _battleEvent.AttackList;
        public IReadOnlyList<CuringValueObject> CuringList => _battleEvent.CuringList;
        public int TechnicalPoint => _battleEvent.TechnicalPoint;
        public SlipCode SlipCode => _battleEvent.SlipCode;

        public BattleLogEntity(
            BattleLogId battleLogId,
            int sequence,
            int turn,
            BattleEventValueObject battleEvent)
        {
            Id = battleLogId;
            Sequence = sequence;
            Turn = turn;
            _battleEvent = battleEvent;
        }

        public int CompareTo(BattleLogEntity other)
        {
            return Sequence - other.Sequence;
        }
    }
}