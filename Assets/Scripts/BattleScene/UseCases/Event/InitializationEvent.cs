using System;
using System.Collections.Generic;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Output.Interface;
using BattleScene.UseCases.StateMachine;
using BattleScene.UseCases.UseCase;
using BattleScene.UseCases.UseCase.Interface;
using BattleScene.UseCases.View.AilmentView;
using BattleScene.UseCases.View.EnemyView;
using BattleScene.UseCases.View.OrderView;
using Utility.Interface;
using VContainer;
using static BattleScene.Domain.Code.AilmentCode;


namespace BattleScene.UseCases.Event
{
    internal class InitializationEvent : BaseEvent
    {
        private readonly IObjectResolver _container;
        private readonly AilmentDomainService _ailment;
        private readonly ICharacterRepository _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IRandomEx _randomEx;

        public InitializationEvent(
            IObjectResolver container,
            AilmentDomainService ailment,
            ICharacterRepository characterRepository,
            OrderedItemsDomainService orderedItems,
            IRandomEx randomEx)
        {
            _container = container;
            _ailment = ailment;
            _characterRepository = characterRepository;
            _orderedItems = orderedItems;
            _randomEx = randomEx;
        }

        public StateCode Execute()
        {
            var useCaseList = new List<IUseCase>
            {
                _container.Resolve<Initialization>(),
                _container.Resolve<BattleStart>(),
                _container.Resolve<OrderDecision>()
            };

            var outputList = new List<IOutput>
            {
                _container.Resolve<EnemyViewOutput>(),
                _container.Resolve<AilmentViewOutput>(),
                _container.Resolve<OrderViewOutput>()
            };

            var startEvent = new State.Event(useCaseList, outputList);

            var triggerDict = new Dictionary<Func<bool>, StateCode>
            {
                { IsResetAilment, StateCode.ResetAilment },
                { IsSlipDamage, StateCode.SlipDamage },
                { PlayerCantAction, StateCode.PlayerCantAction },
                { EnemyCantAction, StateCode.EnemyCantAction },
                { IsPlayer, StateCode.SelectAction },
                { () => !IsPlayer(), StateCode.EnemySkill }
            };

            return StateCode.Ailment;
        }

        public override void UseCase()
        {
            throw new NotImplementedException();
        }

        public override void Output()
        {
            throw new NotImplementedException();
        }

        public override StateCode StateCode { get; set; }

        private bool IsResetAilment()
        {
            return _orderedItems.FirstItem() is OrderedAilmentValueObject;
        }

        private bool IsSlipDamage()
        {
            return _orderedItems.FirstItem() is OrderedSlipDamageValueObject;
        }

        private bool PlayerCantAction()
        {
            return IsPlayer() && CantAction();
        }

        private bool EnemyCantAction()
        {
            return !IsPlayer() && CantAction();
        }
        
        private bool CantAction()
        {
            if (_orderedItems.FirstItem() is not OrderedCharacterValueObject) return false;
            var characterId = _orderedItems.FirstCharacterId();
            var ailmentCode = _ailment.GetHighPriority(characterId)?.AilmentCode;
            if (!ailmentCode.HasValue) return false;
            if (ailmentCode.Value is not Sleep and not Stun and not Petrifaction and not Paralysis
                and not EnemyParalysis) return false;
            if (ailmentCode.Value is Paralysis or EnemyParalysis && _randomEx.Probability(0.5f)) return false;
            return true;
        }

        private bool IsPlayer()
        {
            if (_orderedItems.FirstItem() is not OrderedCharacterValueObject orderedCharacter) return false;
            return _characterRepository.Select(orderedCharacter.CharacterId).IsPlayer();
        }
    }
}