using System.Collections.Generic;
using System.Linq;
using BattleScene.DataAccess;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;

namespace Tests.BattleScene.DataAccess.Repository
{
    public class MockRepository<TEntity, TId> : IRepository<TEntity, TId>, ISerializable
        where TEntity : BaseEntity<TId>
    {
        private readonly Dictionary<TId, TEntity> _entityDictionary = new();
        
        public TEntity Select(TId id)
        {
            _entityDictionary.TryGetValue(id, out var entity);
            return entity;
        }

        public IReadOnlyList<TEntity> Select() => _entityDictionary.Values.ToList();

        public void Update(TEntity entity)
        {
            if (entity == null) return;
            _entityDictionary.Add(entity.Id, entity);
        }

        public void Update(IReadOnlyList<TEntity> entityList)
        {
            foreach (var entity in entityList) Update(entity);
        }

        public void Delete()
        {
            _entityDictionary.Clear();
        }
        
        public void Delete(TId id)
        {
            _entityDictionary.Remove(id);
        }

        public void Delete(IReadOnlyList<TId> idList)
        {
            foreach (var id in idList) Delete(id);
        }

        public override string ToString()
        {
            var stringList = _entityDictionary.Values
                .Select(x => x.ToString())
                .ToList();
            var str = string.Join("\n", stringList);
            return str;
        }
    }
}