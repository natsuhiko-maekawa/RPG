#nullable enable
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;

namespace BattleScene.DataAccesses.Repository
{
    public partial class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : notnull
    {
        private readonly Dictionary<TId, TEntity> _entityDictionary = new();

        public bool TryGet(TId? id, [NotNullWhen(true)] out TEntity? entity)
        {
            entity = null;
            return id is not null && _entityDictionary.TryGetValue(id, out entity);
        }

        [return: NotNullIfNotNull("id")] public TEntity? Get(TId? id)
        {
            if (id is null) return null;
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

        public IReadOnlyList<TEntity> Get(IReadOnlyList<TId> idList)
        {
            // QUESTION: アロケーションを起こさずに辞書型から条件に一致するキーの値の配列を生成したいが方法がわからない。
            var entityArray = new TEntity[idList.Count];
            for (var i = 0; i < idList.Count; ++i)
            {
                var key = idList[i];
                var value = _entityDictionary[key];
                entityArray[i] = value;
            }

            return entityArray;
        }

        public IReadOnlyList<TEntity> Get() => _entityDictionary.Values.ToList();

        public void Add(TEntity entity)
        {
            Observe(entity);
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