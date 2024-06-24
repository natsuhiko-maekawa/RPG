using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.AttackCountView.OutputData;

namespace BattleScene.UseCases.View.AttackCountView.OutputDataFactory
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