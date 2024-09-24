using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.UseCases.Service;
using Utility.Interface;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    internal class OrderState : BaseState
    {
        private readonly IObjectResolver _container;
        private readonly ActionTimeService _actionTime;
        private readonly AilmentDomainService _ailment;
        private readonly OrderService _order;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRandomEx _randomEx;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly OrderViewPresenter _orderView;

        public OrderState(
            IObjectResolver container,
            ActionTimeService actionTime,
            AilmentDomainService ailment,
            OrderService order,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRandomEx randomEx,
            OrderedItemsDomainService orderedItems,
            OrderViewPresenter orderView)
        {
            _container = container;
            _actionTime = actionTime;
            _ailment = ailment;
            _order = order;
            _characterRepository = characterRepository;
            _randomEx = randomEx;
            _orderedItems = orderedItems;
            _orderView = orderView;
        }

        public override void Start()
        {
            _order.Update();
            _actionTime.Update();
            _orderView.StartAnimationAsync();
            var nextState = GetNextState();
            Context.TransitionTo(nextState);
        }

        private BaseState GetNextState()
        {
            if (IsResetAilment()) throw new NotImplementedException();
            if (IsSlipDamage()) throw new NotImplementedException();
            if (PlayerCantAction()) throw new NotImplementedException();
            if (EnemyCantAction()) throw new NotImplementedException();
            if (IsPlayer()) return _container.Resolve<PlayerSelectActionState>();
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
            var ailmentCode = _ailment.GetHighestPriority(characterId)?.AilmentCode;
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
            return _characterRepository.Select(characterId).IsPlayer;
        }
    }
}