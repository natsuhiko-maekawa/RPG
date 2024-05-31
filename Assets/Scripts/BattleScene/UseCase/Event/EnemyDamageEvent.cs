// using BattleScene.UseCase.Damage;
// using BattleScene.UseCase.Event.Interface;
// using BattleScene.UseCase.EventRunner;
// using BattleScene.UseCase.IPresenter;
// using BattleScene.UseCase.Message;
// using BattleScene.UseCase.Order;
// using BattleScene.UseCase.Skill;
// using static BattleScene.UseCase.EventRunner.EventCode;
// using static BattleScene.UseCase.Enum_.PlayerImageCode;
// using static BattleScene.UseCase.Message.MessageCode;
//
// namespace BattleScene.UseCase.Event
// {
//     internal class EnemyDamageEvent : IEvent, IWait
//     {
//         private readonly IDamageEntity _damageEntity;
//         private readonly IMessageGenerator _messageGenerator;
//         private readonly IOrder _order;
//         private readonly IDigitViewPresenter _digitViewPresenter;
//         private readonly IHitPointBarViewPresenter _hitPointBarViewPresenter;
//         private readonly IMessageViewPresenter _messageViewPresenter;
//         private readonly IPlayerImageViewPresenter _playerImageViewPresenter;
//         private readonly ICharacterVibesViewPresenter _characterVibesViewPresenter;
//         private bool _isAvoid;
//         
//         public EnemyDamageEvent(
//             IDamageEntity damageEntity,
//             IMessageGenerator messageGenerator,
//             IOrder order,
//             IDigitViewPresenter digitViewPresenter,
//             IHitPointBarViewPresenter hitPointBarViewPresenter,
//             IMessageViewPresenter messageViewPresenter,
//             IPlayerImageViewPresenter playerImageViewPresenter,
//             ICharacterVibesViewPresenter characterVibesViewPresenter)
//         {
//             _damageEntity = damageEntity;
//             _messageGenerator = messageGenerator;
//             _order = order;
//             _digitViewPresenter = digitViewPresenter;
//             _hitPointBarViewPresenter = hitPointBarViewPresenter;
//             _messageViewPresenter = messageViewPresenter;
//             _playerImageViewPresenter = playerImageViewPresenter;
//             _characterVibesViewPresenter = characterVibesViewPresenter;
//         }
//         
//         public EventCode Run()
//         {
//             _digitViewPresenter.StartDmgView(_damageEntity.Get());
//             _hitPointBarViewPresenter.StartHpBarView(_damageEntity.GetTargetList());
//             var isAvoid = _damageEntity.IsAvoided();
//             var message = _messageGenerator
//                 .SetActor(_order.FirstCharacter())
//                 .SetDmg(_damageEntity)
//                 .SetTarget(_damageEntity.GetTargetList())
//                 .Generate(isAvoid ? AvoidMessage : DamageMessage);
//             _messageViewPresenter.StartMessageView(message);
//             _playerImageViewPresenter.Start(isAvoid
//                 ? Avoidance
//                 : _order.FirstCharacter().Skill.GetImageName());
//             if (!isAvoid)
//                 _characterVibesViewPresenter.StartCharacterVibesView(_damageEntity.GetTargetList());
//             _isAvoid = isAvoid;
//             
//             return WaitEvent;
//         }
//
//         public EventCode NextEvent()
//         {
//             return _isAvoid ? GetIndexWhenAvoid() : GetIndex();
//         }
//
//         private EventCode GetIndex()
//         {
//             return _order.FirstCharacter().Skill switch
//             {
//                 IAilmentsSkill => EventCode.AilmentsEvent,
//                 IDestroyedPartSkill => EventCode.DestroyedPartEvent,
//                 IDebuffSkill => EventCode.DebuffEvent,
//                 ICureSkill => EventCode.CureEvent,
//                 IResetSkill => EventCode.ResetEvent,
//                 IBuffSkill => EventCode.BuffEvent,
//                 IEnhanceSkill => EventCode.EnhanceEvent,
//                 _ => EventCode.LoopEndEvent
//             };
//         }
//
//         private EventCode GetIndexWhenAvoid()
//         {
//             return _order.FirstCharacter().Skill switch
//             {
//                 ICureSkill => EventCode.CureEvent,
//                 IResetSkill => EventCode.ResetEvent,
//                 IBuffSkill => EventCode.BuffEvent,
//                 IEnhanceSkill => EventCode.EnhanceEvent,
//                 _ => EventCode.LoopEndEvent
//             };
//         }
//     }
// }

