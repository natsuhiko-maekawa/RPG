using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class ResultEntity : BaseEntity<ResultEntity, ResultId>, IComparable<ResultEntity>
    {
        [Obsolete]
        public ResultEntity(
            TurnNumber turn,
            SequenceNumber sequence,
            IResult result)
        {
            Result = result;
        }

        public ResultEntity(
            TurnNumber turn,
            SkillValueObject skill)
        {
            Skill = skill;
        }

        public ResultEntity(ResultId id,
            int turn,
            int sequence,
            ImmutableList<CharacterId> targetIdList,
            AilmentCode ailmentCode)
        {
            Id = id;
            Turn = turn;
            Sequence = sequence;
            TargetIdList = targetIdList;
            AilmentCode = ailmentCode;
        }
        
        public int Turn { get; }
        public int Sequence { get; }
        public override ResultId Id { get; }
        [Obsolete]
        public IResult Result { get; }
        public SkillValueObject Skill { get; }
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