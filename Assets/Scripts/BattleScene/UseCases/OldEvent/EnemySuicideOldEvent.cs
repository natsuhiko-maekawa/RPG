using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class EnemySuicideOldEvent : IOldEvent, IWait
    {
        public EventCode Run()
        {
            // _orderedItems.First().TryGetCharacterId(out var characterId);
            // 自滅した敵の状態異常を削除
            // _ailmentRepository.Delete(characterId);
            // 自滅した敵の行動時間を削除
            // _actionTimeRepository.Delete(characterId);

            // 状態異常を表示
            // var ailmentOutputData = _ailmentOutputDataFactory.Create(characterId);
            // _ailmentView.Start(ailmentOutputData);
            // 敵の画像の表示を更新
            // var enemyOutputData = _enemyOutputDataFactory.Create();
            // _enemyView.Start(enemyOutputData);
            // メッセージを表示
            // var messageOutputData = _messageOutputDataFactory.Create(EnemySuicideMessage);
            // _messageView.Start(messageOutputData);

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return LoopEndEvent;
        }
    }
}