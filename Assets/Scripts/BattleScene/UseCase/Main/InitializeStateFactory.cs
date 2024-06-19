using System;
using System.Collections.Generic;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.BusinessLogic;
using BattleScene.UseCase.BusinessLogic.Interface;
using BattleScene.UseCase.Output.Interface;
using BattleScene.UseCase.View.AilmentView;
using BattleScene.UseCase.View.OrderView;
using Utility.Interface;
using VContainer;
using static BattleScene.Domain.Code.AilmentCode;

namespace BattleScene.UseCase.Main
{
    internal class InitializeStateFactory
    {
        private readonly IObjectResolver _container;
        private readonly AilmentDomainService _ailment;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IRandomEx _randomEx;

        public InitializeStateFactory(
            IObjectResolver container,
            AilmentDomainService ailment,
            OrderedItemsDomainService orderedItems,
            IRandomEx randomEx)
        {
            _container = container;
            _ailment = ailment;
            _orderedItems = orderedItems;
            _randomEx = randomEx;
        }

        public State Create()
        {
            var useCaseList = new List<IUseCase>()
            {
                _container.Resolve<InitializationLogic>(),
                _container.Resolve<BattleStartLogic>(),
                _container.Resolve<OrderDecisionLogic>()
            };
            
            var outputList = new List<IOutput>()
            {
                _container.Resolve<AilmentViewOutput>(),
                _container.Resolve<OrderViewOutput>()
            };
            
            var startEvent = new State.Event(useCaseList, outputList);

            var triggerDict = new Dictionary<Func<bool>, StateCode>()
            {
                {CantAction, StateCode.CantAction}
            };

            return new State(
                triggerDict: triggerDict,
                startEvent: startEvent);
        }

        private bool CantAction()
        {
            if (_orderedItems.FirstItem() is not OrderedCharacterValueObject) return false;
            var characterId = _orderedItems.FirstCharacterId();
            var ailmentCode = _ailment.GetHighPriority(characterId)?.AilmentCode;
            if (!ailmentCode.HasValue) return false;
            if (ailmentCode.Value is not Sleep and not Stun and not Petrifaction and not Paralysis and not EnemyParalysis) return false;
            if (ailmentCode.Value is Paralysis or EnemyParalysis && _randomEx.Probability(0.5f)) return false;
            return true;
        }
    }
}