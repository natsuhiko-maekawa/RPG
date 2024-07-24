using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;
using BattleScene.Domain.IRepository;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class AutoIncrementIdRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : BaseEntity<TEntity, TId>
        where TId : AutoIncrementId<IId>
    {
        private readonly Repository<TEntity, TId> _repository = new();
        
        public AutoIncrementId<TId> GenerateId()
        {
            if (_repository.IsEmpty) return new AutoIncrementId<TId>(0);
            var number = Select()
                .Select(x => x.Id.Id)
                .OrderBy(x => x)
                .Last();
            return new AutoIncrementId<TId>(number + 1);
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