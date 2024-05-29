using BattleScene.UseCase.View.AttackCountView.OutputData;

namespace BattleScene.UseCase.View.AttackCountView.OutputBoundary
{
    public interface IAttackCountViewPresenter
    {
        public void Start(AttackCountOutputData attackCountOutputData);
    }
}