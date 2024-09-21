using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using R3;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class TechnicalPointBarViewPresenter : IObserver<CharacterEntity>
    {
        private readonly IFactory<PlayerPropertyValueObject, CharacterTypeCode> _playerPropertyFactory;
        private readonly PlayerView _playerView;

        public TechnicalPointBarViewPresenter(
            IFactory<PlayerPropertyValueObject, CharacterTypeCode> playerPropertyFactory,
            PlayerView playerView)
        {
            _playerPropertyFactory = playerPropertyFactory;
            _playerView = playerView;
        }

        public void Observe(CharacterEntity character)
        {
            if (!character.IsPlayer) return;
            character.ReactiveCurrentTechnicalPoint.Subscribe(StartTechnicalPointBarView);
        }

        private void StartTechnicalPointBarView(int currentTechnicalPoint)
        {
            var maxTechnicalPoint = _playerPropertyFactory.Create(CharacterTypeCode.Player).TechnicalPoint;
            var dto = new TechnicalPointBarViewDto(maxTechnicalPoint, currentTechnicalPoint);
            _playerView.StartTechnicalPointBarView(dto);
        }
    }
}