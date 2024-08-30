using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.UseCase.Interface;
using static BattleScene.Domain.Code.CharacterTypeCode;

namespace BattleScene.UseCases.UseCase
{
    internal class BattleStart : IUseCase
    {
        private readonly EnemiesDomainService _enemies;

        public BattleStart(EnemiesDomainService enemies)
        {
            _enemies = enemies;
        }

        public void Execute()
        {
            var enemyTypeIdList = new List<CharacterTypeCode> { Bee, Dragon, Mantis, Shuten, Slime };
            _enemies.Add(enemyTypeIdList);
        }
    }
}