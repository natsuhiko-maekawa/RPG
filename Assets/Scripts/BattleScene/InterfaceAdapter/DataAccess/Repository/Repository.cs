﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using Utility;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>, IReadOnlyRepository<TEntity, TId>, ISerializable
        where TEntity : BaseEntity<TEntity, TId>
    {
        private readonly HashSet<TEntity> _entitySet = new();
        
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

        public void Update(TEntity entity)
        {
            _entitySet.Update(entity);
        }

        public void Update(IList<TEntity> entityList)
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

        public void Delete(IList<TId> idList)
        {
            foreach (var id in idList)
            {
                Delete(id);
            }
        }
    }
}