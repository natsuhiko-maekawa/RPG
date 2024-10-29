using System;
using System.Linq;
using System.Text;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.State.Turn;

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
        private readonly IResource<AilmentViewDto, AilmentCode, SlipCode> _ailmentViewResource;
        private readonly IResource<BodyPartViewDto, BodyPartCode> _bodyPartViewInfoResource;
        private readonly IResource<BuffViewDto, BuffCode> _buffViewInfoResource;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly IResource<EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IResource<PlayerViewDto, CharacterTypeCode> _playerViewInfoResource;
        private readonly IResource<SkillViewDto, SkillCode> _skillViewInfoResource;
        private readonly ICollection<BattleLogEntity, BattleLogId> _battleLogCollection;
        private readonly BattleLogDomainService _battleLog;

        public MessageCodeConverterService(
            IResource<AilmentViewDto, AilmentCode, SlipCode> ailmentViewResource,
            IResource<BodyPartViewDto, BodyPartCode> bodyPartViewInfoResource,
            IResource<BuffViewDto, BuffCode> buffViewInfoResource,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            IResource<EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            OrderedItemsDomainService orderedItems,
            IResource<PlayerViewDto, CharacterTypeCode> playerViewInfoResource,
            IResource<SkillViewDto, SkillCode> skillViewInfoResource,
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection,
            BattleLogDomainService battleLog)
        {
            _ailmentViewResource = ailmentViewResource;
            _bodyPartViewInfoResource = bodyPartViewInfoResource;
            _buffViewInfoResource = buffViewInfoResource;
            _characterCollection = characterCollection;
            _enemyViewInfoResource = enemyViewInfoResource;
            _orderedItems = orderedItems;
            _playerViewInfoResource = playerViewInfoResource;
            _skillViewInfoResource = skillViewInfoResource;
            _battleLogCollection = battleLogCollection;
            _battleLog = battleLog;
        }

        public string Replace(string message, Context? context = null)
        {
            var replacedMessage = new Message(message)
                .Replace(Actor, ReplaceActor)
                .Replace(Ailment, ReplaceAilment)
                .Replace(Buff, ReplaceBuff)
                .Replace(Damage, ReplaceDamage)
                .Replace(Part, ReplaceBodyPart)
                .Replace(Player, ReplacePlayer)
                .Replace(Skill, x => ReplaceSkill(x, context?.Skill?.Common.SkillCode ?? SkillCode.NoSkill))
                .Replace(Target, ReplaceTarget)
                .Replace(TechnicalPoint, ReplaceTechnicalPoint)
                .GetMessage();

            return replacedMessage;
        }

        private void ReplaceActor(StringBuilder message)
        {
            if (!_orderedItems.First().TryGetCharacterId(out var characterId)) throw new InvalidOperationException();

            var actorName = _characterCollection.Get(characterId).IsPlayer
                ? _playerViewInfoResource.Get(CharacterTypeCode.Player).PlayerName
                : _enemyViewInfoResource.Get(_characterCollection.Get(characterId).CharacterTypeCode).EnemyName;
            message.Replace(Actor, actorName);
        }

        private void ReplaceAilment(StringBuilder message)
        {
            var ailmentName = GetAilmentName();
            message.Replace(Ailment, ailmentName);
        }

        private string GetAilmentName()
        {
            var battleLog = _battleLog.GetLast();
            if (battleLog.AilmentCode != AilmentCode.NoAilment)
                return _ailmentViewResource.Get(battleLog.AilmentCode).Name;
            if (battleLog.SlipCode != SlipCode.NoSlip)
                return _ailmentViewResource.Get(battleLog.SlipCode).Name;
            throw new InvalidOperationException();
        }

        private void ReplaceBuff(StringBuilder message)
        {
            var buffCode = _battleLogCollection.Get().Max()?.BuffCode ?? BuffCode.NoBuff;
            if (buffCode == BuffCode.NoBuff) throw new InvalidOperationException();
            var buffName = _buffViewInfoResource.Get(buffCode).BuffName;
            message.Replace(Buff, buffName);
        }

        private void ReplaceDamage(StringBuilder message)
        {
            var totalPrefix = _battleLogCollection.Get()
                .Max().AttackList.Count(x => x.IsHit) == 1
                ? ""
                : "計";
            var damage = _battleLogCollection.Get()
                .Max().AttackList
                .Where(x => x.IsHit)
                .Select(x => x.Amount)
                .Sum()
                .ToString();
            message.Replace(Damage, totalPrefix + damage);
        }

        // private void ReplaceCure(StringBuilder message)
        // {
        // }

        private void ReplaceBodyPart(StringBuilder message)
        {
            var bodyPartCode = _battleLogCollection.Get().Max().DestroyedPart;
            var bodyPartName = _bodyPartViewInfoResource.Get(bodyPartCode).BodyPartName;
            message.Replace(Part, bodyPartName);
        }

        private void ReplacePlayer(StringBuilder message)
        {
            var playerName = _playerViewInfoResource.Get(CharacterTypeCode.Player).PlayerName;
            message.Replace(Player, playerName);
        }

        private void ReplaceSkill(StringBuilder message, SkillCode skillCode)
        {
            var skillName = _skillViewInfoResource.Get(skillCode).SkillName;
            message.Replace(Skill, skillName);
        }

        private void ReplaceTarget(StringBuilder message)
        {
            var targetNameList = _battleLogCollection.Get()
                .Max().TargetIdList
                .Distinct()
                .Select(GetCharacterName)
                .ToList();
            if (targetNameList.Count == 0) throw new InvalidOperationException();
            var totalSuffix = targetNameList.Count == 1 ? "" : "たち";
            message.Replace(Target, targetNameList.First() + totalSuffix);
        }

        private string GetCharacterName(CharacterId characterId)
        {
            var characterName = _characterCollection.Get(characterId).IsPlayer
                ? _playerViewInfoResource.Get(CharacterTypeCode.Player).PlayerName
                : _enemyViewInfoResource.Get(_characterCollection.Get(characterId).CharacterTypeCode).EnemyName;
            return characterName;
        }

        private void ReplaceTechnicalPoint(StringBuilder message)
        {
            var technicalPoint = _battleLogCollection.Get()
                .Max().TechnicalPoint
                .ToString();
            message.Replace(TechnicalPoint, technicalPoint);
        }
    }
}