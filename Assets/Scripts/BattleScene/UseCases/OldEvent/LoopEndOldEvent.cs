using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.OldEvent
{
    internal class LoopEndOldEvent : IOldEvent
    {
        public EventCode Run()
        {
            // フレームを非表示
            // _frameView.Stop();

            // プレイヤーが死亡した場合、プレイヤーの敗北
            // if (!_hitPointRepository.Select(_characters.GetPlayerId()).IsSurvive()) return EventCode.PlayerDeadEvent;

            // 先頭が状態異常だった場合、以下の処理は実行しないためreturnする
            // if (!_orderedItems.First().TryGetCharacterId(out _)) return EventCode.OrderDecisionEvent;

            // 敵全体が死亡した場合、プレイヤーの勝利
            // if (_characters.GetEnemies().IsEmpty) return EventCode.PlayerWinEvent;

            // 上記以外の場合戦闘を続行
            // foreach (var characterId in _characters.GetIdList())
            // {
            // 全員の状態異常のターンを進める
            //     _ailment.AdvanceAllTurn(characterId);
            // 全員のバフのターンを進める
            //     _buff.AdvanceAllTurn(characterId);
            // }

            // 状態異常を表示
            // var ailmentOutputData = _ailmentOutputDataFactory.Create();
            // _ailmentView.Start(ailmentOutputData);
            // バフを表示
            // var buffOutputData = _buffOutputDataFactory.Create();
            // _buffView.Start(buffOutputData);

            return EventCode.OrderDecisionEvent;
        }
    }
}