using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.AttackCountView.OutputData;

namespace BattleScene.UseCase.View.AttackCountView.OutputDataFactory
{
    public class AttackCountOutputDataFactory
    {
        private readonly AttackCounterService _attackCounter;

        public AttackCountOutputDataFactory(AttackCounterService attackCounter)
        {
            _attackCounter = attackCounter;
        }

        public AttackCountOutputData Create()
        {
            return new AttackCountOutputData(_attackCounter.GetRate());
        }
    }
}