using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class ResultEntity : BaseEntity<ResultEntity, SequenceNumber>, IComparable<ResultEntity>
    {
        [Obsolete]
        public ResultEntity(
            TurnNumber turn,
            SequenceNumber sequence,
            IResult result)
        {
            Turn = turn;
            Id = sequence;
            Result = result;
        }

        public ResultEntity(
            TurnNumber turn,
            SequenceNumber sequence,
            AbstractSkill skill)
        {
            Turn = turn;
            Id = sequence;
            Skill = skill;
        }

        public ResultEntity(TurnNumber turn,
            SequenceNumber sequence,
            AbstractSkill skill,
            ImmutableList<CharacterId> targetIdList,
            AilmentCode ailmentCode)
        {
            Turn = turn;
            Id = sequence;
            Skill = skill;
            TargetIdList = targetIdList;
            AilmentCode = ailmentCode;
        }

        public TurnNumber Turn { get; }
        public override SequenceNumber Id { get; }
        [Obsolete]
        public IResult Result { get; }
        public AbstractSkill Skill { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }
        public AilmentCode AilmentCode { get; }

        public int CompareTo(ResultEntity other)
        {
            if (!Equals(Turn, other.Turn)) return Turn.CompareTo(other.Turn);
            return Id.CompareTo(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var resultEntity = (ResultEntity)obj;
            return Turn == resultEntity.Turn
                   && Id == resultEntity.Id;
        }

        public override int GetHashCode()
        {
            return (Turn, Id).GetHashCode();
        }
    }
}