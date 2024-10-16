using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public partial class BodyPartEntity : BaseEntity<(CharacterId, BodyPartCode)>
    {
        private readonly int _count;

        public BodyPartEntity(
            CharacterId characterId,
            BodyPartCode bodyPartCode,
            int count)
        {
            CharacterId = characterId;
            BodyPartCode = bodyPartCode;
            Id = (CharacterId, BodyPartCode);
            _count = count;
            Destroyed();
        }

        public override (CharacterId, BodyPartCode) Id { get; }
        public CharacterId CharacterId { get; }
        public BodyPartCode BodyPartCode { get; }

        private int _destroyedCount;
        public int DestroyedCount
        {
            get => _destroyedCount;
            private set { _destroyedCount = Math.Min(value, _count); DestroyedCountOnChange(_destroyedCount);}
        }

        partial void DestroyedCountOnChange(int value);

        public void Destroyed()
        {
            ++DestroyedCount;
        }

        public bool IsAvailable()
        {
            return DestroyedCount < _count;
        }
    }
}