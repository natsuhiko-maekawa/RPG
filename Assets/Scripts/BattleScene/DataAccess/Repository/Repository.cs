using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using Utility;

namespace BattleScene.DataAccess.Repository
{
    public partial class Repository<TEntity, TId> : IRepository<TEntity, TId>, ISerializable
        where TEntity : BaseEntity<TId>
    {
        private readonly HashSet<TEntity> _entitySet = new();

        public TEntity Select(TId id)
        {
            return _entitySet
                .FirstOrDefault(x => Equals(x.Id, id));
        }

        public ImmutableList<TEntity> Select()
        {
            return _entitySet.ToImmutableList();
        }

        public void Update(TEntity entity)
        {
            if (entity == null) return;
            Observe(entity);
            _entitySet.Update(entity);
        }

        partial void Observe(TEntity entity);

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