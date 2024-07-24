using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class AutoIncrementRepository<TEntity, TId> : IAutoIncrementRepository<TEntity, TId>
        where TEntity : BaseEntity<TEntity, TId>
        where TId : Number<TId>, new()
    {
        private readonly Repository<TEntity, TId> _repository = new();
        
        public TId GenerateId()
        {
            throw new NotImplementedException();
        }

        public TEntity Select(TId id)
        {
            return _repository.Select(id);
        }

        public ImmutableList<TEntity> Select()
        {
            return _repository.Select();
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        public void Update(IList<TEntity> entityList)
        {
            _repository.Update(entityList);
        }

        public void Delete(TId id)
        {
            _repository.Delete(id);
        }

        public void Delete(IList<TId> idList)
        {
            _repository.Delete(idList);
        }
    }
}