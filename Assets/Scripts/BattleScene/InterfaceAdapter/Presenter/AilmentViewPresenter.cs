using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using R3;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class AilmentViewPresenter : IObserver<AilmentEntity>
    {
        private readonly PlayerStatusView _playerAilmentsView;

        public AilmentViewPresenter(
            PlayerStatusView playerAilmentsView)
        {
            _playerAilmentsView = playerAilmentsView;
        }

        public void Observe(AilmentEntity ailment)
        {
            ailment.ReactiveEffects.Subscribe(x => StartPlayerAilmentView(ailment.AilmentCode, x));
        }

        private void StartPlayerAilmentView(AilmentCode ailmentCode, bool effects)
        {
            var ailmentId = AilmentIdConverter.Ailment(ailmentCode);
            var dto = new AilmentViewModel(ailmentId, effects);
            _playerAilmentsView.StartPlayerAilmentsView(dto);
        }
    }
}