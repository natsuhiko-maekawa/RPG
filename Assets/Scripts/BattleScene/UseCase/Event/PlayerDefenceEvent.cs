// using System;
// using BattleScene.Domain.DomainService;
// using BattleScene.Domain.IRepository;
// using BattleScene.UseCase.Event.Interface;
// using BattleScene.UseCase.EventRunner;
// using BattleScene.UseCase.Skill;
// using BattleScene.UseCase.View.DigitView.OutputBoundary;
// using BattleScene.UseCase.View.MessageView.OutputBoundary;
// using BattleScene.UseCase.View.MessageView.OutputDataFactory;
// using BattleScene.UseCase.View.TechnicalPointBarView.OutputBoundary;
// using static BattleScene.UseCase.EventRunner.EventCode;
// using static BattleScene.Domain.Code.MessageCode;
//
// namespace BattleScene.UseCase.Event
// {
//     internal class PlayerDefenceEvent : IEvent, IWait
//     {
//         private readonly MessageOutputDataFactory _messageOutputDataFactory;
//         private readonly OrderedItemsDomainService _orderedItems;
//         private readonly CharactersDomainService _position;
//         private readonly IBuffRepository _buffRepository;
//         private readonly ISkillRepository _skillRepository;
//         private readonly ITechnicalPointRepository _technicalPointRepository;
//         private readonly IDigitViewPresenter _digitViewPresenter;
//         private readonly IMessageViewPresenter _messageView;
//         private readonly ITechnicalPointBarViewPresenter _technicalPointBarViewPresenter;
//         private const int RestoreTpBase = 10;
//         
//         public EventCode Run()
//         {
//             var playerId = _position.GetPlayerId();
//             var skill = _skillRepository.Select(playerId);
//             
//             if (skill.AbstractSkill is not DefenceSkill defenceSkill)
//                 throw new InvalidCastException();
//
//             var technicalPoint = _technicalPointRepository.Select();
//             var restoreTechnicalPoint = technicalPoint.GetRestore(RestoreTpBase);
//
//             if (restoreTechnicalPoint > 0)
//             {
//                 technicalPoint.Add(restoreTechnicalPoint);
//                 _technicalPointRepository.Update(technicalPoint);
//                 
//                 _digitViewPresenter.Start(restoreTp, _position.GetPlayer());
//                 _technicalPointBarViewPresenter.Start(_position.GetPlayer());
//             }
//             
//             var messageOutputData = _messageOutputDataFactory.Create(DefenceMessage);
//             _messageView.Start(messageOutputData);
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