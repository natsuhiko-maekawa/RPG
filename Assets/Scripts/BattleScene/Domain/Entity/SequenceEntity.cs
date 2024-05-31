using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public class SequenceEntity
    {
        private int _sequence;

        public void Increment()
        {
            ++_sequence;
        }

        public SequenceNumber Get()
        {
            return new SequenceNumber(_sequence);
        }
    }
}