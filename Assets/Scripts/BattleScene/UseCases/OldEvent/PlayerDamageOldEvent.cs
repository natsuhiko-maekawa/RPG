using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.AttackCountView.OutputBoundary;
using BattleScene.UseCases.View.AttackCountView.OutputDataFactory;
using BattleScene.UseCases.View.DigitView.OutputBoundary;
using BattleScene.UseCases.View.DigitView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class PlayerDamageOldEvent
    {
        private readonly AttackCountOutputDataFactory _attackCountOutputDataFactory;
        private readonly IAttackCountViewPresenter _attackCountViewPresenter;
        private readonly DamageDigitOutputDataFactory _damageDigitOutputDataFactory;
        private readonly DamageMessageOutputDataFactory _damageMessageOutputDataFactory;
        private readonly IDigitViewPresenter _digitView;
        private readonly IMessageViewPresenter _messageViewPresenter;

        public PlayerDamageOldEvent(
            AttackCountOutputDataFactory attackCountOutputDataFactory,
            IAttackCountViewPresenter attackCountViewPresenter,
            DamageDigitOutputDataFactory damageDigitOutputDataFactory,
            DamageMessageOutputDataFactory damageMessageOutputDataFactory,
            IDigitViewPresenter digitView,
            IMessageViewPresenter messageViewPresenter)
        {
            _attackCountOutputDataFactory = attackCountOutputDataFactory;
            _attackCountViewPresenter = attackCountViewPresenter;
            _damageDigitOutputDataFactory = damageDigitOutputDataFactory;
            _damageMessageOutputDataFactory = damageMessageOutputDataFactory;
            _digitView = digitView;
            _messageViewPresenter = messageViewPresenter;
        }

        public EventCode Run()
        {
            // var characterId = _orderedItems.FirstCharacterId();

            // プレイヤーが回避した場合、回避イラストを表示
            // プレイヤーが回避していない場合、被ダメージイラストを表示し、振動させる
            // var isAvoid = !_result.Last<DamageValueObject>().Success();
            // var isAvoid = true;
            // if (!_characterRepository.Select(characterId).IsPlayer())
            //     _playerImageView.Start(new PlayerImageOutputData(
            //         isAvoid
            //             ? PlayerImageCode.Avoidance
            //             : _skillViewInfoFactory.Create(_skillRepository.Select(characterId).SkillCode)
            //                 .PlayerImageCode));
            //
            // if (isAvoid)
            // {
            //     var characterVibesOutputData = _characterVibesOutputDataFactory.Create();
            //     _characterVibesView.Start(characterVibesOutputData);
            // }

            var digitOutputData = _damageDigitOutputDataFactory.Create();
            _digitView.Start(digitOutputData);
            // HPバーを表示する
            // var hitPointBarOutputData = _hitPointBarOutputDataFactory.Create();
            // _hitPointBarView.Start(hitPointBarOutputData);
            var messageOutputData = _damageMessageOutputDataFactory.Create();
            _messageViewPresenter.Start(messageOutputData);
            var attackCountOutputData = _attackCountOutputDataFactory.Create();
            _attackCountViewPresenter.Start(attackCountOutputData);

            return WaitEvent;
        }

        // public EventCode NextEvent()
        // {
        //     return _result.Last<DamageValueObject>().Success()
        //         ? GetIndex()
        //         : GetIndexWhenAvoid();
        // }

        private EventCode GetIndex()
        {
            // 敵を倒した場合、別ルートに遷移
            // if (_hitPoint.AnyIsDead()) return EventCode.PlayerBeatEnemyEvent;
            return EventCode.SwitchSkillEvent;
        }

        private EventCode GetIndexWhenAvoid()
        {
            // 敵を倒した場合、別ルートに遷移
            // if (_hitPoint.AnyIsDead()) return EventCode.PlayerBeatEnemyEvent;
            return EventCode.SwitchSkillEvent;
        }
    }
}