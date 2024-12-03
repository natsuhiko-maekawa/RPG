using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public struct AilmentPropertyValueObject
    {
        public AilmentCode AilmentCode { get; }
        public int Turn { get; }
        public bool IsSelfRecovery { get; }
        public int? Priority { get; }

        public AilmentPropertyValueObject(
            AilmentCode ailmentCode,
            int turn,
            bool isSelfRecovery,
            int? priority)
        {
            AilmentCode = ailmentCode;
            Turn = turn;
            IsSelfRecovery = isSelfRecovery;
            Priority = priority;
        }
    }
}