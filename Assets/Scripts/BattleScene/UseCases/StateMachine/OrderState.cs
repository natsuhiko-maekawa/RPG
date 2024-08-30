using System;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Output;
using BattleScene.UseCases.UseCase;
using Utility.Interface;
using VContainer;


namespace BattleScene.UseCases.StateMachine
{
    internal class OrderState : AbstractState
    {
        private readonly IObjectResolver _container;
        private readonly AilmentDomainService _ailment;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRandomEx _randomEx;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly OrderDecision _orderDecision;
        private readonly OrderView _orderView;

        public OrderState(
            IObjectResolver container,
            AilmentDomainService ailment,
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IRandomEx randomEx,
            OrderedItemsDomainService orderedItems,
            OrderDecision orderDecision,
            OrderView orderView)
        {
            _container = container;
            _ailment = ailment;
            _characterRepository = characterRepository;
            _randomEx = randomEx;
            _orderedItems = orderedItems;
            _orderDecision = orderDecision;
            _orderView = orderView;
        }

        public override void Start()
        {
            _orderDecision.Execute();
            _orderView.Out();
            var nextState = GetNextState();
            Context.TransitionTo(nextState);
        }

        private AbstractState GetNextState()
        {
            if (IsResetAilment()) throw new NotImplementedException();
            if (IsSlipDamage()) throw new NotImplementedException();
            if (PlayerCantAction()) throw new NotImplementedException();
            if (EnemyCantAction()) throw new NotImplementedException();
            if (IsPlayer()) return _container.Resolve<PlayerSelectSkillState>();
            return _container.Resolve<EnemySelectSkillState>();
        }
        
        private bool IsResetAilment()
        {
            return _orderedItems.First().TryGetAilmentCode(out _);
        }

        private bool IsSlipDamage()
        {
            return _orderedItems.First().TryGetSlipDamageCode(out _);
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
            if (!_orderedItems.First().TryGetCharacterId(out var characterId)) return false;
            var ailmentCode = _ailment.GetHighPriority(characterId)?.AilmentCode;
            if (!ailmentCode.HasValue) return false;
            if (ailmentCode.Value is 
                not AilmentCode.Sleep 
                and not AilmentCode.Stun 
                and not AilmentCode.Petrifaction
                and not AilmentCode.Paralysis
                and not AilmentCode.EnemyParalysis) 
                return false;
            return ailmentCode.Value is not (AilmentCode.Paralysis or AilmentCode.EnemyParalysis) || !_randomEx.Probability(0.5f);
        }

        private bool IsPlayer()
        {
            if (!_orderedItems.First().TryGetCharacterId(out var characterId)) return false;
            return _characterRepository.Select(characterId).IsPlayer();
        }
    }
}