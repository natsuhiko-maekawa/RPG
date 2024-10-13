using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using R3;

namespace BattleScene.InterfaceAdapter.Presenter
{
    internal class BodyPartViewReactivePresenter : IReactive<BodyPartEntity>
    {
        private readonly ToIndexService _toIndex;
        private readonly PlayerStatusView _playerStatusView;

        public BodyPartViewReactivePresenter(
            ToIndexService toIndex,
            PlayerStatusView playerStatusView)
        {
            _toIndex = toIndex;
            _playerStatusView = playerStatusView;
        }

        public void Observe(BodyPartEntity bodyPart)
        {
            bodyPart.ReactiveDestroyedCount.Subscribe(x => StartPlayerBodyPartView(bodyPart.BodyPartCode, x));
        }

        private void StartPlayerBodyPartView(BodyPartCode bodyPartCode, int destroyedCount)
        {
            var index = _toIndex.FromBodyPart(bodyPartCode);
            var model = new BodyPartViewModel(index, destroyedCount);
            _playerStatusView.StartPlayerDestroyedPartView(model);
        }
    }
}