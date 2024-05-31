// using System;
// using BattleScene.Domain.DomainService;
// using BattleScene.UseCase.Event.Interface;
// using BattleScene.UseCase.EventRunner;
// using BattleScene.UseCase.IPresenter;
// using BattleScene.UseCase.IRepository;
// using BattleScene.UseCase.Message;
// using BattleScene.UseCase.Skill;
// using static BattleScene.UseCase.EventRunner.EventCode;
//
// namespace BattleScene.UseCase.Event
// {
//     internal class EnemyAttackEvent : IEvent, IWait
//     {
//         private readonly IDamageEntity _damageEntity;
//         private readonly IMessageGenerator _messageGenerator;
//         private readonly OrderedItemsDomainService _orderedItems;
//         private readonly ISkillFactory _skillFactory;
//         private readonly ISkillRepository _skillRepository;
//         private readonly IMessageViewPresenter _messageViewPresenter;
//         
//         public EventCode Run()
//         {
//             var isAvoid = false;
//             var characterId = _orderedItems.FirstCharacterId();
//             if (_skillFactory.Create(characterId) is IDamageSkill)
//             {
//                 _damageEntity.Set(_orderedItems.FirstCharacterId());
//                 _messageGenerator.SetTarget(_damageEntity.GetTargetList());
//                 isAvoid = _damageEntity.IsAvoided();
//             }
//             
//             var messageEnum = isAvoid
//                 ? MessageCode.TryAttackMessage
//                 : _orderedItems.FirstCharacterId().Skill.GetMsg();
//             var message = _messageGenerator
//                 .SetActor(_orderedItems.FirstCharacterId())
//                 .Generate(messageEnum);
//             _messageViewPresenter.StartMessageView(message);
//
//             return WaitEvent;
//         }
//
//         public EventCode NextEvent()
//         {
//             return _orderedItems.FirstCharacter().Skill switch
//             {
//                 IDamageSkill => EventCode.EnemyDamageEvent,
//                 IAilmentsSkill => EventCode.AilmentsEvent,
//                 IDestroyedPartSkill => EventCode.DestroyedPartEvent,
//                 IDebuffSkill => EventCode.DebuffEvent,
//                 ICureSkill => EventCode.CureEvent,
//                 IResetSkill => EventCode.ResetEvent,
//                 IBuffSkill => EventCode.BuffEvent,
//                 IEnhanceSkill => EventCode.EnhanceEvent,
//                 _ => throw new ArgumentOutOfRangeException()
//             };
//         }
//     }
// }

