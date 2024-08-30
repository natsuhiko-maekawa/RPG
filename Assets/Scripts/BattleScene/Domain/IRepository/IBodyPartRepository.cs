using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.IRepository
{
    [Obsolete]
    public interface IBodyPartRepository
    {
        public ImmutableList<BodyPartEntity> Select(CharacterId characterId);
        public ImmutableList<BodyPartEntity> Select(IList<CharacterId> characterIdList);
        public BodyPartEntity Select(CharacterId characterId, BodyPartCode bodyPartCode);
        public void Update(BodyPartEntity bodyPartEntity);
        public void Update(IList<BodyPartEntity> bodyPartList);
        public void Delete(BodyPartEntity bodyPartEntity);
    }
}