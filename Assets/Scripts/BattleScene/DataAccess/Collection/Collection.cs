using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;

namespace BattleScene.DataAccess.Collection
{
    public partial class Collection<TEntity, TId> : ICollection<TEntity, TId>, ISerializable
        where TEntity : BaseEntity<TId>
    {
        private readonly Dictionary<TId, TEntity> _entityDictionary = new();
        
        public TEntity Get(TId id)
        {
            _entityDictionary.TryGetValue(id, out var entity);
            return entity;
        }

        public IReadOnlyList<TEntity> Get() => _entityDictionary.Values.ToList();

        public void Add(TEntity entity)
        {
            if (entity == null) return;
            Observe(entity);
            
            // TODO: TryAddをAddに修正すること
            if (_entityDictionary.TryAdd(entity.Id, entity)) return;
            _entityDictionary.Remove(entity.Id);
            _entityDictionary.Add(entity.Id, entity);
        }

        partial void Observe(TEntity entity);

        public void Add(IReadOnlyList<TEntity> entityList)
        {
            foreach (var entity in entityList) Add(entity);
        }

        public void Remove()
        {
            _entityDictionary.Clear();
        }
        
        public void Remove(TId id)
        {
            _entityDictionary.Remove(id);
        }

        public void Remove(IReadOnlyList<TId> idList)
        {
            foreach (var id in idList) Remove(id);
        }
    }
}