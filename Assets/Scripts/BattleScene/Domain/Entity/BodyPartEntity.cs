﻿using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public class BodyPartEntity : BaseEntity<BodyPartEntity, (CharacterId, BodyPartCode)>
    {
        private readonly int _bodyPartNumber;
        private int _destroyedPartNum;

        public BodyPartEntity(
            CharacterId characterId,
            BodyPartCode bodyPartCode,
            int bodyPartNumber)
        {
            CharacterId = characterId;
            BodyPartCode = bodyPartCode;
            Id = (CharacterId, BodyPartCode);
            _bodyPartNumber = bodyPartNumber;
            Destroyed();
        }

        public override (CharacterId, BodyPartCode) Id { get; }
        public CharacterId CharacterId { get; }
        public BodyPartCode BodyPartCode { get; }
        public int DestroyedCount { get; }

        public void Destroyed()
        {
            // TODO: Min()を使って三項演算子を書き換える
            _destroyedPartNum = _bodyPartNumber <= _destroyedPartNum ? _bodyPartNumber : ++_destroyedPartNum;
        }

        public int DestroyedPartCount()
        {
            return _destroyedPartNum;
        }

        public bool IsAvailable()
        {
            return _destroyedPartNum < _bodyPartNumber;
        }

    }
}