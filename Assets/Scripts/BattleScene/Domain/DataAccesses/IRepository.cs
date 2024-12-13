using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BattleScene.Domain.Entities;

namespace BattleScene.Domain.DataAccesses
{
    public interface IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : notnull 
    {
        public bool TryGet(TId? id, [NotNullWhen(true)] out TEntity? entity);
        [return: NotNullIfNotNull("id")] public TEntity? Get(TId? id);
        public bool TryGet([NotNullWhen(true)] out IReadOnlyList<TEntity>? entityList);
        public IReadOnlyList<TEntity> Get(IReadOnlyList<TId> idList);
        public IReadOnlyList<TEntity> Get();
        public void Add(TEntity entity);
        public void Add(IReadOnlyList<TEntity> entityList);
        public void Remove();
        public void Remove(TId id);
        public void Remove(IReadOnlyList<TId> idList);
    }
}