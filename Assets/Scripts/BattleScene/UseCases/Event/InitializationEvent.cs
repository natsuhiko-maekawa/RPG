using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.StateMachine;
using BattleScene.UseCases.UseCase;
using BattleScene.UseCases.View.AilmentView;
using BattleScene.UseCases.View.EnemyView;
using BattleScene.UseCases.View.OrderView;
using Utility.Interface;
using static BattleScene.Domain.Code.AilmentCode;


namespace BattleScene.UseCases.Event
{
    internal class InitializationEvent : BaseEvent
    {
        private readonly AilmentDomainService _ailment;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IRandomEx _randomEx;
        private readonly Initialization _initialization;
        private readonly BattleStart _battleStart;
        private readonly OrderDecision _orderDecision;
        private readonly EnemyViewOutput _enemyView;
        private readonly AilmentViewOutput _ailmentView;
        private readonly OrderViewOutput _orderView;

        public override StateCode GetStateCode()
        {
            if (IsResetAilment()) return StateCode.ResetAilment;
            if (IsSlipDamage()) return StateCode.SlipDamage;
            if (PlayerCantAction()) return StateCode.PlayerCantAction;
            if (EnemyCantAction()) return StateCode.EnemyCantAction;
            if (IsPlayer()) return StateCode.SelectAction;
            return StateCode.EnemySkill;
        }

        public override void UseCase()
        {
            _initialization.Execute();
            _battleStart.Execute();
            _orderDecision.Execute();
        }

        public override void Output()
        {
            _enemyView.Out();
            _ailmentView.Out();
            _orderView.Out();
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