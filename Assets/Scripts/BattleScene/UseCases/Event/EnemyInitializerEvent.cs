using BattleScene.UseCases.StateMachine;
using BattleScene.UseCases.UseCase;
using BattleScene.UseCases.View.EnemyView;

namespace BattleScene.UseCases.Event
{
    public class EnemyInitializerEvent : BaseEvent
    {
        private readonly BattleStart _battleStart;
        private readonly EnemyViewOutput _enemyView;
        
        public override void UseCase()
        {
            _battleStart.Execute();
        }

        public override void Output()
        {
            _enemyView.Out();
        }

        public override StateCode GetStateCode()
        {
            throw new System.NotImplementedException();
        }
    }
}