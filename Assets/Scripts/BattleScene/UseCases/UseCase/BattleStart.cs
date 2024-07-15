using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.UseCase.Interface;
using static BattleScene.Domain.Code.CharacterTypeId;

namespace BattleScene.UseCases.UseCase
{
    internal class BattleStart : IUseCase
    {
        private readonly EnemiesDomainService _enemies;

        public void Execute()
        {
            var enemyTypeIdList = new List<CharacterTypeId> { Bee, Dragon, Mantis, Shuten, Slime };
            _enemies.Add(enemyTypeIdList);
        }
    }
}