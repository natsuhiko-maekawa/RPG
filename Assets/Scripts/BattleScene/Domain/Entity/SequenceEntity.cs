using BattleScene.Domain.OldId;

namespace BattleScene.Domain.Entity
{
    public class SequenceEntity : BaseEntity<SequenceEntity, SequenceNumber>
    {
        public override SequenceNumber Id { get; }

        public SequenceEntity()
        {
            
        }
    }
}