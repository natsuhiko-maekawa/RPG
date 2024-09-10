using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

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
        private readonly IResource<AilmentViewInfoDto, AilmentCode> _ailmentViewInfoResource;
        private readonly IResource<BodyPartViewInfoDto, BodyPartCode> _bodyPartViewInfoResource;
        private readonly IResource<BuffViewInfoDto, BuffCode> _buffViewInfoResource;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IResource<EnemyViewInfoDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IResource<PlayerViewInfoDto, CharacterTypeCode> _playerViewInfoResource;
        private readonly ISkillRepository _skillRepository;
        private readonly IResource<SkillPropertyDto, SkillCode> _skillViewInfoResource;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly PlayerDomainService _player;

        public MessageCodeConverterService(
            IResource<AilmentViewInfoDto, AilmentCode> ailmentViewInfoResource,
            IResource<BodyPartViewInfoDto, BodyPartCode> bodyPartViewInfoResource,
            IResource<BuffViewInfoDto, BuffCode> buffViewInfoResource,
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IResource<EnemyViewInfoDto, CharacterTypeCode> enemyViewInfoResource,
            IResource<MessageDto, MessageCode> messageResource,
            OrderedItemsDomainService orderedItems,
            IResource<PlayerViewInfoDto, CharacterTypeCode> playerViewInfoResource,
            ISkillRepository skillRepository,
            IResource<SkillPropertyDto, SkillCode> skillViewInfoResource,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            PlayerDomainService player)
        {
            _ailmentViewInfoResource = ailmentViewInfoResource;
            _bodyPartViewInfoResource = bodyPartViewInfoResource;
            _buffViewInfoResource = buffViewInfoResource;
            _characterRepository = characterRepository;
            _enemyViewInfoResource = enemyViewInfoResource;
            _messageResource = messageResource;
            _orderedItems = orderedItems;
            _playerViewInfoResource = playerViewInfoResource;
            _skillRepository = skillRepository;
            _skillViewInfoResource = skillViewInfoResource;
            _battleLogRepository = battleLogRepository;
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
            var actorName = _characterRepository.Select(characterId).IsPlayer()
                ? _playerViewInfoResource.Get(CharacterTypeCode.Player).PlayerName
                : _enemyViewInfoResource.Get(_characterRepository.Select(characterId).CharacterTypeCode)
                    .EnemyName;
            return message.Replace(Actor, actorName);
        }

        private string ReplaceAilments(string message)
        {
            if (!message.Contains(Ailment)) return message;
            var ailmentCode = _battleLogRepository.Select().Max().AilmentCode;
            var ailmentName = _ailmentViewInfoResource.Get(ailmentCode).AilmentName;
            return message.Replace(Ailment, ailmentName);
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
            if (!_orderedItems.First().TryGetCharacterId(out var characterId))
                throw new InvalidOperationException();
            var skillCode = _skillRepository.Select(characterId).SkillCode;
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