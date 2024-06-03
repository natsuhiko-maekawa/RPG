using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.AilmentView.OutputDataFactory;
using BattleScene.UseCase.View.AttackCountView.OutputBoundary;
using BattleScene.UseCase.View.AttackCountView.OutputDataFactory;
using BattleScene.UseCase.View.EnemyView.OutputBoundary;
using BattleScene.UseCase.View.EnemyView.OutputDataFactory;
using BattleScene.UseCase.View.HitPointBarView.OutputBoundary;
using BattleScene.UseCase.View.HitPointBarView.OutputDataFactory;
using BattleScene.UseCase.View.TechnicalPointBarView.OutputBoundary;
using BattleScene.UseCase.View.TechnicalPointBarView.OutputDaraFactory;
using static BattleScene.Domain.Code.CharacterTypeId;

namespace BattleScene.UseCase.Event
{
    internal class BattleStartEvent : IEvent
    {
        private readonly CharacterCreatorService _characterCreator;
        private readonly ICharacterRepository _characterRepository;

        public BattleStartEvent(
            CharacterCreatorService characterCreator,
            ICharacterRepository characterRepository)
        {
            _characterCreator = characterCreator;
            _characterRepository = characterRepository;
        }

        public EventCode Run()
        {
            var enemyTypeIdList = new List<CharacterTypeId> { Bee, Dragon, Mantis, Shuten, Slime };
            var enemyList = _characterCreator.CreateEnemyList(enemyTypeIdList);
            _characterRepository.Update(enemyList);
            
            return EventCode.OrderDecisionEvent;
        }
    }
}