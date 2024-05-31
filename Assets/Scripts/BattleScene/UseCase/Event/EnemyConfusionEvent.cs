// using BattleScene.UseCase.Damage;
// using BattleScene.UseCase.Event.Interface;
// using BattleScene.UseCase.EventRunner;
// using BattleScene.UseCase.IPresenter;
// using BattleScene.UseCase.Message;
// using BattleScene.UseCase.Order;
// using BattleScene.UseCase.Position;
// using static BattleScene.UseCase.Message.MessageCode;
//
// namespace BattleScene.UseCase.Event
// {
//     internal class EnemyConfusionEvent : IEvent, IWait
//     {
//         private readonly IDamageEntity _damageEntity;
//         private readonly IMessageGenerator _messageGenerator;
//         private readonly IOrder _order;
//         private readonly IPosition _position;
//         private readonly IDigitViewPresenter _digitViewPresenter;
//         private readonly IFrameViewPresenter _frameViewPresenter;
//         private readonly IMessageViewPresenter _messageViewPresenter;
//         private readonly IPlayerImageViewPresenter _playerImageViewPresenter;
//         private readonly ICharacterVibesViewPresenter _characterVibesViewPresenter;
//
//         public EnemyConfusionEvent(
//             IDamageEntity damageEntity,
//             IMessageGenerator messageGenerator,
//             IOrder order,
//             IPosition position,
//             IDigitViewPresenter digitViewPresenter,
//             IFrameViewPresenter frameViewPresenter,
//             IMessageViewPresenter messageViewPresenter,
//             IPlayerImageViewPresenter playerImageViewPresenter,
//             ICharacterVibesViewPresenter characterVibesViewPresenter)
//         {
//             _damageEntity = damageEntity;
//             _messageGenerator = messageGenerator;
//             _order = order;
//             _position = position;
//             _digitViewPresenter = digitViewPresenter;
//             _messageViewPresenter = messageViewPresenter;
//             _frameViewPresenter = frameViewPresenter;
//             _playerImageViewPresenter = playerImageViewPresenter;
//             _characterVibesViewPresenter = characterVibesViewPresenter;
//         }
//
//         public EventCode Run()
//         {
//             _damageEntity.Set(_order.FirstCharacter());
//
//             _digitViewPresenter.StartDmgView(_damageEntity.Get());
//             _frameViewPresenter.StartActorView(_order.FirstCharacter());
//             _playerImageViewPresenter.StartPlayerView(_position.GetPlayer().Skill.GetImageName());
//             var enemyConfusionActMessage = _messageGenerator
//                 .SetActor(_order.FirstCharacter())
//                 .SetDmg(_damageEntity)
//                 .Generate(EnemyConfusionActMessage);
//             _messageViewPresenter.StartMessageView(enemyConfusionActMessage);
//             _characterVibesViewPresenter.StartCharacterVibesView(_damageEntity.GetTargetList());
//
//             return EventCode.WaitEvent;
//         }
//
//         public EventCode NextEvent()
//         {
//             return _order.FirstCharacter().CurrentHp > 0
//                 ? EventCode.LoopEndEvent
//                 : EventCode.EnemySuicideEvent;
//         }
//     }
// }

