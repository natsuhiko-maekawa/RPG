using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public class TargetEntity
    {
        public CharacterId CharacterId { get; }
        public ImmutableList<CharacterId> TargetIdList { get; private set; }

        public TargetEntity(
            CharacterId characterId)
        {
            CharacterId = characterId;
        }

        public void Set(CharacterId targetId)
        {
            TargetIdList = ImmutableList.Create(new[] { targetId });
        }

        public void Set(IList<CharacterId> targetId)
        {
            TargetIdList = targetId.ToImmutableList();
        }
    }
}