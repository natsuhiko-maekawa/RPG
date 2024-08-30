﻿using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.Service
{
    public class MessageCodeConverterService
    {
        private const string Actor = "actor";
        private const string Ailment = "ailment";
        // private const string Buff = "buff";
        private const string Damage = "damage";
        private const string Part = "part";
        private const string Skill = "skill";
        private const string Target = "target";
        private readonly IFactory<AilmentViewInfoDto, AilmentCode> _ailmentViewInfoFactory;
        private readonly IFactory<BodyPartViewInfoDto, BodyPartCode> _bodyPartViewInfoFactory;
        private readonly ICharacterRepository _characterRepository;
        private readonly IEnemyViewInfoFactory _enemyViewInfoFactory;
        private readonly IFactory<MessageDto, MessageCode> _messageFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IFactory<PlayerViewInfoDto, CharacterTypeId> _playerViewInfoFactory;
        private readonly ResultDomainService _result;
        private readonly ISkillRepository _skillRepository;
        private readonly IFactory<SkillViewInfoDto, SkillCode> _skillViewInfoFactory;
        private readonly ITargetRepository _targetRepository;

        public MessageCodeConverterService(
            IFactory<AilmentViewInfoDto, AilmentCode> ailmentViewInfoFactory,
            IFactory<BodyPartViewInfoDto, BodyPartCode> bodyPartViewInfoFactory,
            ICharacterRepository characterRepository,
            IEnemyViewInfoFactory enemyViewInfoFactory,
            IFactory<MessageDto, MessageCode> messageFactory,
            OrderedItemsDomainService orderedItems,
            IFactory<PlayerViewInfoDto, CharacterTypeId> playerViewInfoFactory,
            ResultDomainService result,
            ISkillRepository skillRepository,
            IFactory<SkillViewInfoDto, SkillCode> skillViewInfoFactory,
            ITargetRepository targetRepository)
        {
            _ailmentViewInfoFactory = ailmentViewInfoFactory;
            _bodyPartViewInfoFactory = bodyPartViewInfoFactory;
            _characterRepository = characterRepository;
            _enemyViewInfoFactory = enemyViewInfoFactory;
            _messageFactory = messageFactory;
            _orderedItems = orderedItems;
            _playerViewInfoFactory = playerViewInfoFactory;
            _result = result;
            _skillRepository = skillRepository;
            _skillViewInfoFactory = skillViewInfoFactory;
            _targetRepository = targetRepository;
        }

        public string Replace(string message)
        {
            message = ReplaceActor(message);
            message = ReplaceAilments(message);
            message = ReplaceBuff(message);
            message = ReplaceDamage(message);
            message = ReplaceCure(message);
            message = ReplaceBodyPart(message);
            message = ReplaceSkill(message);
            message = ReplaceTarget(message);
            return message;
        }
        
        [Obsolete]
        public string ToMessage(MessageCode messageCode)
        {
            var message = _messageFactory.Create(messageCode).Message;
            message = ReplaceActor(message);
            message = ReplaceAilments(message);
            message = ReplaceBuff(message);
            message = ReplaceDamage(message);
            message = ReplaceCure(message);
            message = ReplaceBodyPart(message);
            message = ReplaceSkill(message);
            message = ReplaceTarget(message);
            return message;
        }

        private string ReplaceActor(string message)
        {
            if (!message.Contains(Actor)) return message;
            if (!_orderedItems.First().TryGetCharacterId(out var characterId))
                throw new InvalidOperationException();
            var actorName = _characterRepository.Select(characterId).IsPlayer()
                ? _playerViewInfoFactory.Create(CharacterTypeId.Player).PlayerName
                : _enemyViewInfoFactory.Create(_characterRepository.Select(characterId).Property.CharacterTypeId)
                    .EnemyName;
            return message.Replace(Actor, actorName);
        }

        private string ReplaceAilments(string message)
        {
            if (!message.Contains(Ailment)) return message;
            var ailmentCode = _result.Last<AilmentResultValueObject>().AilmentCode;
            var ailmentName = _ailmentViewInfoFactory.Create(ailmentCode).AilmentName;
            return message.Replace(Ailment, ailmentName);
        }

        private string ReplaceBuff(string message)
        {
            throw new NotImplementedException();
        }

        private string ReplaceDamage(string message)
        {
            if (!message.Contains(Damage)) return message;
            var totalPrefix = _result.LastDamage().AttackList.Count(x => x.IsHit) == 1 ? "" : "計";
            var damage = _result.LastDamage().GetTotal().ToString();
            return message.Replace(Damage, totalPrefix + damage);
        }

        private string ReplaceCure(string message)
        {
            throw new NotImplementedException();
        }

        private string ReplaceBodyPart(string message)
        {
            if (!message.Contains(Part)) return message;
            var bodyPartCode = _result.Last<DestroyedPartSkillResultValueObject>().BodyPartCode;
            var bodyPartName = _bodyPartViewInfoFactory.Create(bodyPartCode).BodyPartName;
            return message.Replace(Part, bodyPartName);
        }

        private string ReplaceSkill(string message)
        {
            if (!message.Contains(Skill)) return message;
            if (!_orderedItems.First().TryGetCharacterId(out var characterId))
                throw new InvalidOperationException();
            var skillCode = _skillRepository.Select(characterId).SkillCode;
            var skillName = _skillViewInfoFactory.Create(skillCode).SkillName;
            return message.Replace(Skill, skillName);
        }

        private string ReplaceTarget(string message)
        {
            if (!message.Contains(Target)) return message;
            if (!_orderedItems.First().TryGetCharacterId(out var characterId))
                throw new InvalidOperationException();
            var targetNameList = _targetRepository.Select(characterId).TargetIdList
                .Select(x => _characterRepository.Select(x).IsPlayer()
                    ? _playerViewInfoFactory.Create(CharacterTypeId.Player).PlayerName
                    : _enemyViewInfoFactory.Create(_characterRepository.Select(x).Property.CharacterTypeId).EnemyName)
                .ToList();
            var totalSuffix = targetNameList.Count == 1 ? "" : "たち";
            return message.Replace(Target, targetNameList.First() + totalSuffix);
        }
    }
}