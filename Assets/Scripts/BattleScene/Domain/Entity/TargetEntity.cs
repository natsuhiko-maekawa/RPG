using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.Entity
{
    public class TargetEntity
    {
        public CharacterId CharacterId { get; }
        public ImmutableList<CharacterId> TargetIdList { get; private set; }

        public TargetEntity(
            CharacterId characterId,
            CharacterId targetId)
        {
            CharacterId = characterId;
            TargetIdList = ImmutableList.Create(targetId);
        }

        public TargetEntity(
            CharacterId characterId, 
            IList<CharacterId> targetIdList)
        {
            CharacterId = characterId;
            TargetIdList = targetIdList.ToImmutableList();
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