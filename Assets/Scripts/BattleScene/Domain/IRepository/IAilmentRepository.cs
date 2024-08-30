using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    [Obsolete]
    public interface IAilmentRepository
    {
        public ImmutableList<AilmentEntity> Select(CharacterId characterId);
        public AilmentEntity Select(CharacterId characterId, AilmentCode ailmentCode);
        public void Update(AilmentEntity ailment);
        public void Delete(CharacterId characterId);
        public void Delete(CharacterId characterId, AilmentCode ailmentCode);
        public void Delete(IList<CharacterId> characterIdList);
        public void Delete(IList<AilmentEntity> ailmentEntityList);
    }
}