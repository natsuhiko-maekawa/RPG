using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public partial class BodyPartEntity : BaseEntity<(CharacterId, BodyPartCode)>
    {
        public BodyPartEntity(
            CharacterId characterId,
            BodyPartCode bodyPartCode,
            int count)
        {
            CharacterId = characterId;
            BodyPartCode = bodyPartCode;
            _count = count;
        }

        public override (CharacterId, BodyPartCode) Id => (CharacterId, BodyPartCode);
        public CharacterId CharacterId { get; }
        public BodyPartCode BodyPartCode { get; }
        private readonly int _count;
        private int _destroyedCount;

        public int DestroyedCount
        {
            get => _destroyedCount;
            private set
            {
                _destroyedCount = Math.Min(value, _count);
                DestroyedCountOnChange(_destroyedCount);
            }
        }

        partial void DestroyedCountOnChange(int value);

        public bool IsAvailable => DestroyedCount < _count;

        public void Destroyed()
        {
            ++DestroyedCount;
        }

        public void Recovered()
        {
            DestroyedCount = 0;
        }
    }
}