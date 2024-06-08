using BattleScene.UseCase.Output.Interface;
using BattleScene.UseCase.View.AilmentView.OutputBoundary;
using BattleScene.UseCase.View.AilmentView.OutputDataFactory;

namespace BattleScene.UseCase.View.AilmentView
{
    internal class AilmentViewOutput : IOutput
    {
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly IAilmentViewPresenter _ailmentView;

        public AilmentViewOutput(
            AilmentOutputDataFactory ailmentOutputDataFactory,
            IAilmentViewPresenter ailmentView)
        {
            _ailmentOutputDataFactory = ailmentOutputDataFactory;
            _ailmentView = ailmentView;
        }

        public void Out()
        {
            _ailmentView.Start(_ailmentOutputDataFactory.Create());
        }
    }
}