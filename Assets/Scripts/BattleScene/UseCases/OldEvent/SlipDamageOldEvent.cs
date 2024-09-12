using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.DigitView.OutputBoundary;
using BattleScene.UseCases.View.DigitView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class SlipDamageOldEvent : IOldEvent, IWait
    {
        private readonly DamageDigitOutputDataFactory _damageDigitOutputDataFactory;
        private readonly IDigitViewPresenter _digitView;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;

        public SlipDamageOldEvent(
            DamageDigitOutputDataFactory damageDigitOutputDataFactory,
            IDigitViewPresenter digitView,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView)
        {
            _damageDigitOutputDataFactory = damageDigitOutputDataFactory;
            _digitView = digitView;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
        }

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
            var digitOutputDataList = _damageDigitOutputDataFactory.Create();
            _digitView.Start(digitOutputDataList);
            // HPバーを表示する
            // _hitPointBarView.Start(_hitPointBarOutputDataFactory.Create());
            var messageOutputData = _messageOutputDataFactory.Create(SlipDamageMessage);
            _messageView.Start(messageOutputData);
            // プレイヤーの画像を振動させる
            // _characterVibesView.Start(_characterVibesOutputDataFactory.Create());
        }
    }
}