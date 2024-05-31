// using BattleScene.Domain.DomainService;
// using BattleScene.UseCase.Event.Interface;
// using BattleScene.UseCase.EventRunner;
// using BattleScene.UseCase.IPresenter;
// using BattleScene.UseCase.IRepository;
//
// namespace BattleScene.UseCase.Event
// {
//     internal class PlayerConfusionEvent : IEvent, IWait
//     {
//         private readonly IAttackCountRepository _countEntity;
//         private readonly IDamageSkillHistoryRepository _damageSkillHistoryRepository;
//         private readonly ISkillFactory _skillFactory;
//         private readonly ISkillRepository _skillRepository;
//         private readonly IDamageEntity _damageEntity;
//         private readonly MessageViewService _messageViewGenerator;
//         private readonly CharactersDomainService _position;
//         private readonly OrderedItemsDomainService _orderedItems;
//         private readonly IFrameViewPresenter _frameViewPresenter;
//         private readonly IMessageViewPresenter _messageViewPresenter;
//         private readonly IPlayerViewPresenter _playerViewPresenter;
//         
//         public EventCode Run()
//         {
//             var skillCode = _skillRepository.Select(_orderedItems.FirstCharacterId()).SkillCode;
//             var skill = _skillFactory.Create(skillCode);
//             
//             _damageEntity.Set(_position.GetPlayer());
//             
//             _frameViewPresenter.StartTargetView(_position.GetPlayer().TargetList);
//             var confusionActMessage = _messageViewGenerator
//                 .SetActor(_position.GetPlayer())
//                 .SetDmg(_damageEntity)
//                 .Generate(ConfusionActMessage);
//             _messageViewPresenter.StartMessageView(confusionActMessage);
//             _playerViewPresenter.StartPlayerView(_position.GetPlayer().Skill.GetImageName());
//
//             return EventCode.WaitEvent;
//         }
//
//         public EventCode NextEvent()
//         {
//             return EventCode.PlayerDamageEvent;
//         }
//     }
// }

