using System.Collections.Immutable;
using BattleScene.Domain.Id;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class SequenceEntity : BaseEntity<SequenceEntity, AutoIncrementId<ISequence>>
    {
        public override AutoIncrementId<ISequence> Id { get; }
        public int Turn { get; }
        public CharacterId ActorId { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }
        public SkillValueObject Skill { get; }
        public AilmentValueObject Ailment { get; }

        public SequenceEntity(
            AutoIncrementId<ISequence> id)
        {
            Id = id;
        }
    }
}