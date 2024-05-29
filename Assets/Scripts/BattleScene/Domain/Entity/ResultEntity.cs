using System;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Entity
{
    public class ResultEntity : IComparable<ResultEntity>
    {
        public TurnNumber Turn { get; }
        public SequenceNumber Sequence { get; }
        public IResult Result { get; }

        public ResultEntity(
            TurnNumber turn,
            SequenceNumber sequence,
            IResult result)
        {
            Turn = turn;
            Sequence = sequence;
            Result = result;
        }

        public int CompareTo(ResultEntity other)
        {
            if (!Equals(Turn, other.Turn)) return Turn.CompareTo(other.Turn);
            return Sequence.CompareTo(other.Sequence);
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var resultEntity = (ResultEntity)obj;
            return Turn == resultEntity.Turn
                && Sequence == resultEntity.Sequence;
        }
        
        public override int GetHashCode()
        {
            return (Turn, Sequence).GetHashCode();
        }
    }
}