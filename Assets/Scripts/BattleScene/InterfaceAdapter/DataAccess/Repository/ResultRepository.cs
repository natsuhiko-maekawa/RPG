using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.Serialization;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using Utility;

namespace BattleScene.Infrastructure.Repository
{
    [TempSave]
    [DataContract]
    public class ResultRepository : IResultRepository
    {
        [DataMember] private readonly HashSet<ResultEntity> _resultSet;

        public ImmutableList<ResultEntity> Select()
        {
            return _resultSet.ToImmutableList();
        }

        public void Update(ResultEntity result)
        {
            _resultSet.Update(result);
        }
    }
}