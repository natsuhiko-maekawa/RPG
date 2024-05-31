// using System;
// using BattleScene.UseCase.Event.Interface;
// using BattleScene.UseCase.EventRunner;
// using BattleScene.UseCase.IPresenter;
// using static BattleScene.UseCase.EventRunner.EventCode;
//
// namespace BattleScene.UseCase.Event
// {
//     internal class EnhanceEvent : IEvent, IWait
//     {
//         private readonly IMessageGenerator _messageGenerator;
//         private readonly IOrder _order;
//         private readonly IPosition _position;
//         private readonly IMessageViewPresenter _messageViewPresenter;
//
//         public EnhanceEvent(
//             IMessageGenerator messageGenerator,
//             IOrder order,
//             IPosition position,
//             IMessageViewPresenter messageViewPresenter)
//         {
//             _messageGenerator = messageGenerator;
//             _order = order;
//             _position = position;
//             _messageViewPresenter = messageViewPresenter;
//         }
//         
//         public EventCode Run()
//         {
//             if (_order.FirstCharacter().Skill is not IEnhanceSkill enhanceSkill)
//                 throw new InvalidCastException();
//             
//             foreach (var target in _position.GetTargetList(_order.FirstCharacter()))
//                 target.EnhanceContainer.Set(enhanceSkill.Enhance, _order);
//             
//             var message = _messageGenerator
//                 .SetActor(_order.FirstCharacter())
//                 .Generate(enhanceSkill.GetMsg());
//             _messageViewPresenter.StartMessageView(message);
//
//             return WaitEvent;
//         }
//
//         public EventCode NextEvent()
//         {
//             return EventCode.LoopEndEvent;
//         }
//     }
// }

