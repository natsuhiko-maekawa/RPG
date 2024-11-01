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
        private const string BodyPart = "[part]";
        private const string Buff = "[buff]";
        private const string Damage = "[damage]";
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
            _battleLog = battleLog;
        }

        public string Replace(
            string message,
            Context? context = null)
        {
            var replacedMessage = new Message(message)
                .Replace(Actor, ReplaceActor)
                .Replace(Ailment, ReplaceAilment)
                .Replace(Buff, ReplaceBuff)
                .Replace(Damage, ReplaceDamage)
                .Replace(BodyPart, ReplaceBodyPart)
                .Replace(Player, ReplacePlayer)
                .Replace(Skill, x => ReplaceSkill(x, context?.Skill?.Common.SkillCode ?? SkillCode.NoSkill))
                .Replace(Target, ReplaceTarget)
                .Replace(TechnicalPoint, ReplaceTechnicalPoint)
                .GetMessage();
            return replacedMessage;
        }


        private void ReplaceActor(StringBuilder message)
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId))
                throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);

            var actorName = GetCharacterName(actorId);
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
            var buffCode = _battleLog.GetLast().BuffCode;
            if (buffCode == BuffCode.NoBuff) throw new InvalidOperationException();
            var buffName = _buffViewInfoResource.Get(buffCode).BuffName;
            message.Replace(Buff, buffName);
        }

        private void ReplaceDamage(StringBuilder message)
        {
            var attackList = _battleLog.GetLast().AttackList;
            var totalPrefix = attackList.Count(x => x.IsHit) == 1
                ? ""
                : "計";
            var damage = attackList
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
            var bodyPartCode = _battleLog.GetLast().DestroyedPart;
            var bodyPartName = _bodyPartViewInfoResource.Get(bodyPartCode).BodyPartName;
            message.Replace(BodyPart, bodyPartName);
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
            var targetNameList = _battleLog.GetLast()
                .TargetIdList
                .Distinct()
                .Select(GetCharacterName)
                .ToList();
            if (targetNameList.Count == 0) throw new InvalidOperationException();
            var totalSuffix = targetNameList.Count == 1 ? "" : "たち";
            message.Replace(Target, targetNameList.First() + totalSuffix);
        }

        private string GetCharacterName(CharacterId characterId)
        {
            var character = _characterCollection.Get(characterId);
            var characterName = character.IsPlayer
                ? _playerViewInfoResource.Get(CharacterTypeCode.Player).PlayerName
                : _enemyViewInfoResource.Get(character.CharacterTypeCode).EnemyName;
            return characterName;
        }

        private void ReplaceTechnicalPoint(StringBuilder message)
        {
            var technicalPoint = _battleLog.GetLast()
                .TechnicalPoint
                .ToString();
            message.Replace(TechnicalPoint, technicalPoint);
        }
    }
}