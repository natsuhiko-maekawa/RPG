using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class BodyPartRepository : IBodyPartRepository
    {
        public ImmutableList<BodyPartEntity> Select(CharacterId characterId)
        {
            throw new System.NotImplementedException();
        }

        public ImmutableList<BodyPartEntity> Select(IList<CharacterId> characterIdList)
        {
            throw new System.NotImplementedException();
        }

        public BodyPartEntity Select(CharacterId characterId, BodyPartCode bodyPartCode)
        {
            throw new System.NotImplementedException();
        }

        public void Update(BodyPartEntity bodyPartEntity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(IList<BodyPartEntity> bodyPartList)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(BodyPartEntity bodyPartEntity)
        {
            throw new System.NotImplementedException();
        }
    }
}