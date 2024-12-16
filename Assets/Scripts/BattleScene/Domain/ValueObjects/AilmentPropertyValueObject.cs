using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public struct AilmentPropertyValueObject
    {
        public AilmentCode AilmentCode { get; }
        public int DefaultTurn { get; }
        public bool IsSelfRecovery { get; }
        public int? Priority { get; }

        public AilmentPropertyValueObject(
            AilmentCode ailmentCode,
            int defaultTurn,
            bool isSelfRecovery,
            int? priority)
        {
            AilmentCode = ailmentCode;
            DefaultTurn = defaultTurn;
            IsSelfRecovery = isSelfRecovery;
            Priority = priority;
        }
    }
}