﻿using System.Collections.Immutable;
using System.Linq;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using UnityEngine;
using Utility;

namespace BattleScene.InterfaceAdapter.Service
{
    public class MessageCodeConverterService
    {
        private const string Actor = "[actor]";
        private const string Ailment = "[ailment]";
        private const string Buff = "[buff]";
        private const string Damage = "[damage]";
        private const string Part = "[part]";
        private const string Player = "[player]";
        private const string Skill = "[skill]";
        private const string Target = "[target]";
        private const string TechnicalPoint = "[technicalPoint]";
        private readonly IResource<AilmentViewDto, AilmentCode, SlipDamageCode> _ailmentViewResource;
        private readonly IResource<BodyPartViewDto, BodyPartCode> _bodyPartViewInfoResource;
        private readonly IResource<BuffViewDto, BuffCode> _buffViewInfoResource;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IResource<EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IResource<PlayerViewDto, CharacterTypeCode> _playerViewInfoResource;
        private readonly IResource<SkillViewDto, SkillCode> _skillViewInfoResource;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly BattleLogDomainService _battleLog;
        private readonly PlayerDomainService _player;

        public MessageCodeConverterService(
            IResource<AilmentViewDto, AilmentCode, SlipDamageCode> ailmentViewResource,
            IResource<BodyPartViewDto, BodyPartCode> bodyPartViewInfoResource,
            IResource<BuffViewDto, BuffCode> buffViewInfoResource,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IResource<EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            OrderedItemsDomainService orderedItems,
            IResource<PlayerViewDto, CharacterTypeCode> playerViewInfoResource,
            IResource<SkillViewDto, SkillCode> skillViewInfoResource,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            BattleLogDomainService battleLog,
            PlayerDomainService player)
        {
            _ailmentViewResource = ailmentViewResource;
            _bodyPartViewInfoResource = bodyPartViewInfoResource;
            _buffViewInfoResource = buffViewInfoResource;
            _characterRepository = characterRepository;
            _enemyViewInfoResource = enemyViewInfoResource;
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
            message = ReplaceCure(message);
            message = ReplaceBodyPart(message);
            message = ReplacePlayer(message);
            message = ReplaceSkill(message);
            message = ReplaceTarget(message);
            message = ReplaceTechnicalPoint(message);
            return message;
        }

        private string ReplaceActor(string message)
        {
            if (!message.Contains(Actor)) return message;
            _orderedItems.First().TryGetCharacterId(out var characterId);
            MyDebug.Assert(characterId != null);
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
                .Max().AttackList
                .Where(x => x.IsHit)
                .Select(x => x.Amount)
                .Sum()
                .ToString();
            return message.Replace(Damage, totalPrefix + damage);
        }
        
        private string ReplaceCure(string message)
        {
            return message;
        }

        private string ReplaceBodyPart(string message)
        {
            if (!message.Contains(Part)) return message;
            var bodyPartCode = _battleLogRepository.Select().Max().DestroyedPart;
            var bodyPartName = _bodyPartViewInfoResource.Get(bodyPartCode).BodyPartName;
            return message.Replace(Part, bodyPartName);
        }

        private string ReplacePlayer(string message)
        {
            if (!message.Contains(Player)) return message;
            var playerName = _playerViewInfoResource.Get(CharacterTypeCode.Player).PlayerName;
            var newMessage = message.Replace(Player, playerName);
            return newMessage;
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
                .Distinct()
                .Select(GetCharacterName)
                .ToImmutableList();
            if (targetNameList.IsEmpty) return message;
            var totalSuffix = targetNameList.Count == 1 ? "" : "たち";
            return message.Replace(Target, targetNameList.First() + totalSuffix);
        }

        private string GetCharacterName(CharacterId characterId)
        {
            var characterName = Equals(characterId, _player.GetId())
                ? _playerViewInfoResource.Get(CharacterTypeCode.Player).PlayerName
                : _enemyViewInfoResource.Get(_characterRepository.Select(characterId).CharacterTypeCode).EnemyName;
            return characterName;
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