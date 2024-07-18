using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Output;
using BattleScene.UseCases.StateMachine;
using BattleScene.UseCases.UseCase;
using BattleScene.UseCases.View.OrderView;
using Utility.Interface;
using static BattleScene.Domain.Code.AilmentCode;


namespace BattleScene.UseCases.Event
{
    internal class OrderEvent : BaseEvent
    {
        private readonly AilmentDomainService _ailment;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRandomEx _randomEx;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly OrderDecision _orderDecision;
        private readonly OrderView _orderView;

        public OrderEvent(
            AilmentDomainService ailment, 
            IRepository<CharacterAggregate, CharacterId> characterRepository, 
            IRandomEx randomEx, OrderedItemsDomainService orderedItems,
            OrderDecision orderDecision, 
            OrderView orderView)
        {
            _ailment = ailment;
            _characterRepository = characterRepository;
            _randomEx = randomEx;
            _orderedItems = orderedItems;
            _orderDecision = orderDecision;
            _orderView = orderView;
        }

        public override void UseCase()
        {
            _orderDecision.Execute();
        }

        public override void Output()
        {
            _orderView.Out();
        }

        public override StateCode GetStateCode()
        {
            if (IsResetAilment()) return StateCode.ResetAilment;
            if (IsSlipDamage()) return StateCode.SlipDamage;
            if (PlayerCantAction()) return StateCode.PlayerCantAction;
            if (EnemyCantAction()) return StateCode.EnemyCantAction;
            if (IsPlayer()) return StateCode.SelectAction;
            return StateCode.EnemySkill;
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
                not Sleep 
                and not Stun 
                and not Petrifaction
                and not Paralysis
                and not EnemyParalysis) 
                return false;
            return ailmentCode.Value is not (Paralysis or EnemyParalysis) || !_randomEx.Probability(0.5f);
        }

        private bool IsPlayer()
        {
            if (!_orderedItems.First().TryGetCharacterId(out var characterId)) return false;
            return _characterRepository.Select(characterId).IsPlayer();
        }
    }
}