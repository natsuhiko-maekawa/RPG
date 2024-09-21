using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using UnityEngine;

namespace BattleScene.Domain.Entity
{
    public partial class BodyPartEntity : BaseEntity<(CharacterId, BodyPartCode)>
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

        private int _destroyedCount;
        public int DestroyedCount
        {
            get => _destroyedCount;
            private set { _destroyedCount = Mathf.Min(value, _bodyPartCount); DestroyedCountOnChange(_destroyedCount);}
        }

        partial void DestroyedCountOnChange(int value);

        public void Destroyed()
        {
            ++DestroyedCount;
        }

        public bool IsAvailable()
        {
            return DestroyedCount < _bodyPartCount;
        }
    }
}