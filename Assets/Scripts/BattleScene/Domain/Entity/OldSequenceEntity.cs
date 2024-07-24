using System;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.Entity
{
    [Obsolete]
    public class OldSequenceEntity
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