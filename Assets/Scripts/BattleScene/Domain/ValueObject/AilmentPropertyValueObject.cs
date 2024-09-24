namespace BattleScene.Domain.ValueObject
{
    public class AilmentPropertyValueObject
    {
        public int Turn { get; }
        public bool IsSelfRecovery { get; }
        public int? Priority { get; }

        public AilmentPropertyValueObject(
            int turn,
            bool isSelfRecovery,
            int? priority)
        {
            Turn = turn;
            IsSelfRecovery = isSelfRecovery;
            Priority = priority;
        }
    }
}