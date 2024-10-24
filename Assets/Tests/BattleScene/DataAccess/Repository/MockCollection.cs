using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BattleScene.DataAccess;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;

namespace Tests.BattleScene.DataAccess.Repository
{
    public class MockCollection<TEntity, TId> : ICollection<TEntity, TId>, ISerializable
        where TEntity : BaseEntity<TId>
    {
        private readonly Dictionary<TId, TEntity> _entityDictionary = new();

        public bool TryGet(TId? id, [NotNullWhen(true)] out TEntity? entity)
        {
            entity = null;
            return id != null && _entityDictionary.TryGetValue(id, out entity);
        }

        [return: NotNullIfNotNull("id")] public TEntity? Get(TId? id)
        {
            if (id == null) return null;
            _entityDictionary.TryGetValue(id, out var entity);
            return entity;
        }

        public bool TryGet([NotNullWhen(true)] out IReadOnlyList<TEntity>? entityList)
        {
            entityList = null;
            if (_entityDictionary.Count == 0) return false;
            entityList = _entityDictionary.Values.ToList();
            return true;
        }
        
        public IReadOnlyList<TEntity> Get() => _entityDictionary.Values.ToList();

        public void Add(TEntity entity)
        {
            _entityDictionary.Add(entity.Id, entity);
        }

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