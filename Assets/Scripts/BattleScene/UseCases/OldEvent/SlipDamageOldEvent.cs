using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class SlipDamageOldEvent : IOldEvent, IWait
    {
        public EventCode Run()
        {
            // var result = _slipDamage.Damage();
            // _resultRepository.Update(result);

            // プレイヤーのHPを減らす
            // var playerId = _characters.GetPlayerId();
            // var hitPoint = _hitPointRepository.Select(playerId);
            // hitPoint.Reduce(_result.LastDamage().GetTotal());
            // _hitPointRepository.Update(hitPoint);

            // var target = _targetRepository.Select(playerId);
            // target.Set(playerId);
            // _targetRepository.Update(target);

            StartView();

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return LoopEndEvent;
        }

        private void StartView()
        {
            // ダメージを表示
            // var digitOutputDataList = _damageDigitOutputDataFactory.Create();
            // _digitView.Start(digitOutputDataList);
            // HPバーを表示
            // _hitPointBarView.Start(_hitPointBarOutputDataFactory.Create());
            // メッセージを表示
            // var messageOutputData = _messageOutputDataFactory.Create(SlipDamageMessage);
            // _messageView.Start(messageOutputData);
            // プレイヤーの画像を振動させる
            // _characterVibesView.Start(_characterVibesOutputDataFactory.Create());
        }
    }
}