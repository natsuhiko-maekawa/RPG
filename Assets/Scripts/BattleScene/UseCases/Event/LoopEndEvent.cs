using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Event.Interface;
using BattleScene.UseCases.Event.Runner;
using BattleScene.UseCases.View.AilmentView.OutputBoundary;
using BattleScene.UseCases.View.AilmentView.OutputDataFactory;
using BattleScene.UseCases.View.BuffView.OutputBoundary;
using BattleScene.UseCases.View.BuffView.OutputDataFactory;
using BattleScene.UseCases.View.FrameView.OutputBoundary;

namespace BattleScene.UseCases.Event
{
    internal class LoopEndEvent : IEvent
    {
        private readonly AilmentDomainService _ailment;
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly IAilmentViewPresenter _ailmentView;
        private readonly BuffDomainService _buff;
        private readonly BuffOutputDataFactory _buffOutputDataFactory;
        private readonly IBuffViewPresenter _buffView;
        private readonly CharactersDomainService _characters;
        private readonly IFrameViewPresenter _frameView;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;
        private readonly OrderedItemsDomainService _orderedItems;

        public LoopEndEvent(
            AilmentDomainService ailment,
            AilmentOutputDataFactory ailmentOutputDataFactory,
            IAilmentViewPresenter ailmentView,
            BuffDomainService buff,
            BuffOutputDataFactory buffOutputDataFactory,
            IBuffViewPresenter buffView,
            CharactersDomainService characters,
            IFrameViewPresenter frameView,
            IRepository<HitPointAggregate, CharacterId> hitPointRepository,
            OrderedItemsDomainService orderedItems)
        {
            _ailment = ailment;
            _ailmentOutputDataFactory = ailmentOutputDataFactory;
            _ailmentView = ailmentView;
            _buff = buff;
            _buffOutputDataFactory = buffOutputDataFactory;
            _buffView = buffView;
            _characters = characters;
            _frameView = frameView;
            _hitPointRepository = hitPointRepository;
            _orderedItems = orderedItems;
        }

        public EventCode Run()
        {
            _frameView.Stop();

            // プレイヤーが死亡した場合、プレイヤーの敗北
            if (!_hitPointRepository.Select(_characters.GetPlayerId()).IsSurvive()) return EventCode.PlayerDeadEvent;

            // 先頭が状態異常だった場合、以下の処理は実行しないためreturnする
            if (_orderedItems.FirstItem() is not OrderedCharacterValueObject) return EventCode.OrderDecisionEvent;

            // 敵全体が死亡した場合、プレイヤーの勝利
            if (_characters.GetEnemies().IsEmpty) return EventCode.PlayerWinEvent;

            // 上記以外の場合戦闘を続行
            foreach (var characterId in _characters.GetIdList())
            {
                _ailment.AdvanceAllTurn(characterId);
                _buff.AdvanceAllTurn(characterId);
            }

            var ailmentOutputData = _ailmentOutputDataFactory.Create();
            _ailmentView.Start(ailmentOutputData);
            var buffOutputData = _buffOutputDataFactory.Create();
            _buffView.Start(buffOutputData);

            return EventCode.OrderDecisionEvent;
        }
    }
}