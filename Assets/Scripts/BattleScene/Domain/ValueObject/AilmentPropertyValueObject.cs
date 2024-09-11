using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class AilmentPropertyValueObject
    {
        public int Turn { get; }
        public bool IsSelfRecovery { get; }
        public Priority Priority { get; }

        public AilmentPropertyValueObject(
            int turn,
            bool isSelfRecovery,
            Priority priority)
        {
            Turn = turn;
            IsSelfRecovery = isSelfRecovery;
            Priority = priority;
        }
    }
}