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
        private readonly PrimeSkillValueObject _primeSkill;
        public ActionCode ActionCode { get; } = ActionCode.Skill;
        public CharacterId? ActorId => _primeSkill.ActorId;
        public SkillCode SkillCode => _primeSkill.SkillCode;
        public IReadOnlyList<CharacterId> TargetIdList => _primeSkill.TargetIdList;
        public IReadOnlyList<CharacterId> ActualTargetIdList => _primeSkill.ActualTargetIdList;
        public AilmentCode AilmentCode => _primeSkill.AilmentCode;
        public BodyPartCode DestroyedPart => _primeSkill.BodyPartCode;
        public int DestroyCount => _primeSkill.DestroyCount;
        public BuffCode BuffCode => _primeSkill.BuffCode;
        public IReadOnlyList<AttackValueObject> AttackList => _primeSkill.AttackList;
        public int TechnicalPoint => _primeSkill.TechnicalPoint;
        public SlipCode SlipCode => _primeSkill.SlipCode;

        public BattleLogEntity(
            BattleLogId battleLogId,
            int sequence,
            int turn,
            PrimeSkillValueObject primeSkill)
        {
            Id = battleLogId;
            Sequence = sequence;
            Turn = turn;
            _primeSkill = primeSkill;
        }

        public int CompareTo(BattleLogEntity other)
        {
            return Sequence - other.Sequence;
        }
    }
}