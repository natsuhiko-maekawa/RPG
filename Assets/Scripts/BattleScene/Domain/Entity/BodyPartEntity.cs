using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using UnityEngine;

namespace BattleScene.Domain.Entity
{
    public class BodyPartEntity : BaseEntity<(CharacterId, BodyPartCode)>
    {
        private readonly int _bodyPartCount;

        public BodyPartEntity(
            CharacterId characterId,
            BodyPartCode bodyPartCode,
            int bodyPartCount)
        {
            CharacterId = characterId;
            BodyPartCode = bodyPartCode;
            Id = (CharacterId, BodyPartCode);
            _bodyPartCount = bodyPartCount;
            Destroyed();
        }

        public override (CharacterId, BodyPartCode) Id { get; }
        public CharacterId CharacterId { get; }
        public BodyPartCode BodyPartCode { get; }
        public int DestroyedCount { get; private set; }

        public void Destroyed()
        {
            DestroyedCount = Mathf.Min(++DestroyedCount, _bodyPartCount);
        }

        public bool IsAvailable()
        {
            return DestroyedCount < _bodyPartCount;
        }

    }
}