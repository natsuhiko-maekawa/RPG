using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class AilmentRepository : IAilmentRepository
    {
        private List<AilmentEntity> _ailmentEntityList;

        public ImmutableList<AilmentEntity> Select(CharacterId characterId)
        {
            return _ailmentEntityList
                .Where(x => Equals(x.CharacterId, characterId))
                .ToImmutableList();
        }

        public AilmentEntity Select(CharacterId characterId, AilmentCode ailmentCode)
        {
            return _ailmentEntityList
                .First(x => Equals(x.CharacterId, characterId) && x.AilmentCode == ailmentCode);
        }

        public void Update(AilmentEntity ailmentEntity)
        {
            _ailmentEntityList.Remove(ailmentEntity);
            _ailmentEntityList.Add(ailmentEntity);
        }

        public void Delete(CharacterId characterId)
        {
            _ailmentEntityList = _ailmentEntityList
                .Where(x => !Equals(x.CharacterId, characterId))
                .ToList();
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
            return _ailmentEntityList
                .ToImmutableList();
        }

        public void Delete(AilmentEntity ailmentEntity)
        {
            _ailmentEntityList.Remove(ailmentEntity);
        }
    }
}