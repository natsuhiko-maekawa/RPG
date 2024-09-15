﻿using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.Service
{
    public class MessageCodeConverterService
    {
        private const string Actor = "[actor]";
        private const string Ailment = "[ailment]";
        private const string Buff = "[buff]";
        private const string Damage = "[damage]";
        private const string Part = "[part]";
        private const string Skill = "[skill]";
        private const string Target = "[target]";
        private const string TechnicalPoint = "[technicalPoint]";
        private readonly IAilmentViewResource _ailmentViewResource;
        private readonly IResource<BodyPartViewDto, BodyPartCode> _bodyPartViewInfoResource;
        private readonly IResource<BuffViewDto, BuffCode> _buffViewInfoResource;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IResource<EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IResource<PlayerViewDto, CharacterTypeCode> _playerViewInfoResource;
        private readonly IResource<SkillPropertyDto, SkillCode> _skillViewInfoResource;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly BattleLogDomainService _battleLog;
        private readonly PlayerDomainService _player;

        public MessageCodeConverterService(
            IAilmentViewResource ailmentViewResource,
            IResource<BodyPartViewDto, BodyPartCode> bodyPartViewInfoResource,
            IResource<BuffViewDto, BuffCode> buffViewInfoResource,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IResource<EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            IResource<MessageDto, MessageCode> messageResource,
            OrderedItemsDomainService orderedItems,
            IResource<PlayerViewDto, CharacterTypeCode> playerViewInfoResource,
            IResource<SkillPropertyDto, SkillCode> skillViewInfoResource,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            BattleLogDomainService battleLog,
            PlayerDomainService player)
        {
            _ailmentViewResource = ailmentViewResource;
            _bodyPartViewInfoResource = bodyPartViewInfoResource;
            _buffViewInfoResource = buffViewInfoResource;
            _characterRepository = characterRepository;
            _enemyViewInfoResource = enemyViewInfoResource;
            _messageResource = messageResource;
            _orderedItems = orderedItems;
            _playerViewInfoResource = playerViewInfoResource;
            _skillViewInfoResource = skillViewInfoResource;
            _battleLogRepository = battleLogRepository;
            _battleLog = battleLog;
            _player = player;
        }

        public string Replace(string message)
        {
            message = ReplaceActor(message);
            message = ReplaceAilments(message);
            message = ReplaceBuff(message);
            message = ReplaceDamage(message);
            // message = ReplaceCure(message);
            message = ReplaceBodyPart(message);
            message = ReplaceSkill(message);
            message = ReplaceTarget(message);
            message = ReplaceTechnicalPoint(message);
            return message;
        }
        
        [Obsolete]
        public string ToMessage(MessageCode messageCode)
        {
            var message = _messageResource.Get(messageCode).Message;
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
            var actorName = _characterRepository.Select(characterId).IsPlayer
                ? _playerViewInfoResource.Get(CharacterTypeCode.Player).PlayerName
                : _enemyViewInfoResource.Get(_characterRepository.Select(characterId).CharacterTypeCode)
                    .EnemyName;
            return message.Replace(Actor, actorName);
        }

        private string ReplaceAilments(string message)
        {
            if (!message.Contains(Ailment)) return message;
            var ailmentName = GetAilmentName();
            return message.Replace(Ailment, ailmentName);
        }

        private string GetAilmentName()
        {
            var battleLog = _battleLog.GetLast();
            if (battleLog.AilmentCode != AilmentCode.NoAilment)
                return _ailmentViewResource.Get(battleLog.AilmentCode).Name;
            if (battleLog.SlipDamageCode != SlipDamageCode.NoSlipDamage)
                return _ailmentViewResource.Get(battleLog.SlipDamageCode).Name;
            Debug.Assert(false);
            return "";
        }

        private string ReplaceBuff(string message)
        {
            var buffCode = _battleLogRepository.Select().Max()?.BuffCode ?? BuffCode.NoBuff;
            if (buffCode == BuffCode.NoBuff) return message;
            var buffName = _buffViewInfoResource.Get(buffCode).BuffName;
            return message.Replace(Buff, buffName);
        }

        private string ReplaceDamage(string message)
        {
            if (!message.Contains(Damage)) return message;
            var totalPrefix = _battleLogRepository.Select()
                .Max().AttackList.Count(x => x.IsHit) == 1 ? "" : "計";
            var damage = _battleLogRepository.Select()
                .Max().GetTotalDamageAmount().ToString();
            return message.Replace(Damage, totalPrefix + damage);
        }

        // ReSharper disable once UnusedParameter.Local
        private string ReplaceCure(string message)
        {
            throw new NotImplementedException();
        }

        private string ReplaceBodyPart(string message)
        {
            if (!message.Contains(Part)) return message;
            var bodyPartCode = _battleLogRepository.Select().Max().DestroyedPart;
            var bodyPartName = _bodyPartViewInfoResource.Get(bodyPartCode).BodyPartName;
            return message.Replace(Part, bodyPartName);
        }

        private string ReplaceSkill(string message)
        {
            if (!message.Contains(Skill)) return message;
            _orderedItems.First().TryGetCharacterId(out var characterId);
            Debug.Assert(characterId != null);
            var skillCode = _battleLog.GetLast().SkillCode;
            var skillName = _skillViewInfoResource.Get(skillCode).SkillName;
            return message.Replace(Skill, skillName);
        }

        private string ReplaceTarget(string message)
        {
            if (!message.Contains(Target)) return message;
            var targetNameList = _battleLogRepository.Select()
                .Max().TargetIdList
                .Select(x => Equals(x, _player.GetId())
                    ? _playerViewInfoResource.Get(CharacterTypeCode.Player).PlayerName
                    : _enemyViewInfoResource.Get(_characterRepository.Select(x).CharacterTypeCode).EnemyName)
                .ToImmutableList();
            if (targetNameList.IsEmpty) return message;
            var totalSuffix = targetNameList.Count == 1 ? "" : "たち";
            return message.Replace(Target, targetNameList.First() + totalSuffix);
        }

        private string ReplaceTechnicalPoint(string message)
        {
            if (!message.Contains(TechnicalPoint)) return message;
            var technicalPoint = _battleLogRepository.Select()
                .Max().TechnicalPoint
                .ToString();
            return message.Replace(TechnicalPoint, technicalPoint);
        }
    }
}