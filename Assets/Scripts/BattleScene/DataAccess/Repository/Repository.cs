using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using Utility;

namespace BattleScene.DataAccess.Repository
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>, IReadOnlyRepository<TEntity, TId>, ISerializable
        where TEntity : BaseEntity<TId>
    {
        private readonly IEnumerable<IObserver<TEntity>> _observers;
        private readonly HashSet<TEntity> _entitySet = new();
        
        public Repository(
            IEnumerable<IObserver<TEntity>> observers)
        {
            _observers = observers;
        }
        
        public bool IsEmpty => _entitySet.Count == 0;

        public TEntity Select(TId id)
        {
            return _entitySet
                .FirstOrDefault(x => Equals(x.Id, id));
        }

        public ImmutableList<TEntity> Select()
        {
            return _entitySet.ToImmutableList();
        }

        public virtual void Update(TEntity entity)
        {
            foreach (var observer in _observers)
            {
                observer.Observe(entity);
            }
            
            _entitySet.Update(entity);
        }

        public void Update(IReadOnlyList<TEntity> entityList)
        {
            foreach (var entity in entityList)
            {
                Update(entity);
            }
        }

        public void Delete()
        {
            _entitySet.Clear();
        }
        
        public void Delete(TId id)
        {
            _entitySet.RemoveWhere(x => Equals(x.Id, id));
        }

        public void Delete(IReadOnlyList<TId> idList)
        {
            foreach (var id in idList)
            {
                Delete(id);
            }
        }
    }
}