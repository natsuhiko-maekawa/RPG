﻿#nullable enable
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;

namespace BattleScene.DataAccess.Collection
{
    public partial class Collection<TEntity, TId> : ICollection<TEntity, TId>, ISerializable
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