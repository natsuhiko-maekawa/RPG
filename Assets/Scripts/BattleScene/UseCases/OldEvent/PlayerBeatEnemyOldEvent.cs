using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class PlayerBeatEnemyOldEvent : IOldEvent, IWait
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageViewPresenter;

        public PlayerBeatEnemyOldEvent(
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageViewPresenter)
        {
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageViewPresenter = messageViewPresenter;
        }

        public EventCode Run()
        {
            // このターンに死亡した敵をリストにする
            // var deadEnemyList = _hitPointRepository.Select()
            //     .Where(x => !x.IsSurvive())
            //     .Select(x => x.Id)
            //     .ToImmutableList();

            // 死亡した敵の状態異常をリセットする
            // _ailmentRepository.Delete(deadEnemyList);

            // 状態異常の表示を更新する
            // var ailmentOutputData = _ailmentOutputDataFactory.Create(deadEnemyList);
            // _ailmentViewPresenter.Start(ailmentOutputData);
            // 敵の画像の表示を更新
            // var enemyOutputData = _enemyOutputDataFactory.Create();
            // _enemyViewPresenter.Start(enemyOutputData);
            var messageOutputData = _messageOutputDataFactory.Create(BeatEnemyMessage);
            _messageViewPresenter.Start(messageOutputData);

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            // 攻撃対象がすべて死亡した場合はターンを終了する
            // var deadEnemyList = _hitPointRepository.Select()
            //     .Where(x => !x.IsSurvive())
            //     .Select(x => x.Id)
            //     .ToImmutableHashSet();
            // var targetList = _result.LastDamage().AttackList
            //     .Select(x => x.TargetId)
            //     .Distinct()
            //     .ToImmutableHashSet();
            // if (deadEnemyList == targetList)
            //     return EventCode.LoopEndEvent;

            return SwitchSkillEvent;
        }
    }
}