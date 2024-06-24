using BattleScene.UseCases.View.AttackCountView.OutputData;

namespace BattleScene.UseCases.View.AttackCountView.OutputBoundary
{
    public interface IAttackCountViewPresenter
    {
        public void Start(AttackCountOutputData attackCountOutputData);
    }
}