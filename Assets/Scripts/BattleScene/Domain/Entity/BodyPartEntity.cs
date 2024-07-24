using BattleScene.Domain.Code;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.Entity
{
    public class BodyPartEntity
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
            _bodyPartNumber = bodyPartNumber;
            Destroyed();
        }

        public CharacterId CharacterId { get; }
        public BodyPartCode BodyPartCode { get; }

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