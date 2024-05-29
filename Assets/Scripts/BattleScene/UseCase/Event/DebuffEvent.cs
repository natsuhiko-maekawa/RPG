// using System;
// using BattleScene.UseCase.Event.Interface;
// using BattleScene.UseCase.EventRunner;
// using BattleScene.UseCase.IPresenter;
// using BattleScene.UseCase.Skill;
// using static BattleScene.UseCase.EventRunner.EventCode;
// using static BattleScene.UseCase.Message.MessageCode;
//
// namespace BattleScene.UseCase.Event
// {
//     internal class DebuffEvent : IEvent, IWait
//     {
//         private readonly IMessageGenerator _messageGenerator;
//         private readonly IOrder _order;
//         private readonly IPosition _position;
//         private readonly IBuffViewPresenter _buffViewPresenter;
//         private readonly IMessageViewPresenter _messageViewPresenter;
//         
//         public EventCode Run()
//         {
//             if (_order.FirstCharacter().Skill is not IDebuffSkill debuffSkill)
//                 throw new InvalidCastException();
//             
//             foreach (var target in _position.GetTargetList(_order.FirstCharacter()))
//                 target.BuffEntity.Set(debuffSkill.DebuffList, _order);
//             
//             _buffViewPresenter.StartBuffView(_position.GetTargetList(_order.FirstCharacter()));
//             var debuffMessage = _messageGenerator
//                 .SetTarget(_position.GetTargetList(_order.FirstCharacter()))
//                 .SetBuff(_position.GetTargetList(_order.FirstCharacter()).First().BuffEntity.Get(debuffSkill.DebuffList))
//                 .Generate(DebuffMessage);
//             _messageViewPresenter.StartMessageView(debuffMessage);
//
//             return WaitEvent;
//         }
//
//         public EventCode NextEvent()
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