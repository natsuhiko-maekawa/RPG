﻿using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public class ResultEntity : BaseEntity<ResultEntity, ResultId>, IComparable<ResultEntity>
    {
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