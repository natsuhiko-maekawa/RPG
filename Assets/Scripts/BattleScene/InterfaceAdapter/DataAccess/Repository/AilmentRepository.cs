using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using Utility;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class AilmentRepository : IAilmentRepository
    {
        private readonly HashSet<AilmentEntity> _ailmentEntitySet = new();

        public ImmutableList<AilmentEntity> Select(CharacterId characterId)
        {
            return _ailmentEntitySet
                .Where(x => Equals(x.CharacterId, characterId))
                .ToImmutableList();
        }

        public AilmentEntity Select(CharacterId characterId, AilmentCode ailmentCode)
        {
            return _ailmentEntitySet
                .First(x => Equals(x.CharacterId, characterId) && x.AilmentCode == ailmentCode);
        }

        public void Update(AilmentEntity ailmentEntity)
        {
            _ailmentEntitySet.Update(ailmentEntity);
        }

        public void Delete(CharacterId characterId)
        {
            _ailmentEntitySet
                .RemoveWhere(x => !Equals(x.CharacterId, characterId));
        }

        public void Delete(CharacterId characterId, AilmentCode ailmentCode)
        {
            throw new NotImplementedException();
        }

        public void Delete(IList<CharacterId> characterIdList)
        {
            throw new NotImplementedException();
        }

        public void Delete(IList<AilmentEntity> ailmentEntityList)
        {
            foreach (var ailmentEntity in ailmentEntityList) Delete(ailmentEntity);
        }

        public ImmutableList<AilmentEntity> Select()
        {
            return _ailmentEntitySet
                .ToImmutableList();
        }

        public void Delete(AilmentEntity ailmentEntity)
        {
            _ailmentEntitySet.Remove(ailmentEntity);
        }
    }
}