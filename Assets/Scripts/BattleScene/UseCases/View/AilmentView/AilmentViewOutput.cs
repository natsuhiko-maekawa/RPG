using BattleScene.UseCases.Output.Interface;
using BattleScene.UseCases.View.AilmentView.OutputBoundary;
using BattleScene.UseCases.View.AilmentView.OutputDataFactory;

namespace BattleScene.UseCases.View.AilmentView
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