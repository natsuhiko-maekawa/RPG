using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.View.MessageView.OutputData;

namespace BattleScene.UseCase.View.MessageView.OutputDataFactory
{
    public class MessageOutputDataFactory
    {
        private const string Actor = "actor";
        private const string Ailment = "ailment";
        private const string Buff = "buff";
        private const string Damage = "damage";
        private const string Part = "part";
        private const string Skill = "skill";
        private const string Target = "target";
        private readonly IAilmentViewInfoFactory _ailmentViewInfoFactory;
        private readonly IBodyPartViewInfoFactory _bodyPartViewInfoFactory;
        private readonly ICharacterRepository _characterRepository;
        private readonly IEnemyViewInfoFactory _enemyViewInfoFactory;
        private readonly IMessageFactory _messageFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IPlayerViewInfoFactory _playerViewInfoFactory;
        private readonly ResultDomainService _result;
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;
        private readonly ITargetRepository _targetRepository;

        public MessageOutputData Create(MessageCode messageCode, bool noWait = false)
        {
            throw new NotImplementedException();
        }

        private string ReplaceActor(string message)
        {
            if (!message.Contains(Actor)) return message;
            if (_orderedItems.FirstItem() is not OrderedCharacterValueObject orderedCharacter)
                throw new InvalidOperationException();
            var characterId = orderedCharacter.CharacterId;
            var actorName = _characterRepository.Select(characterId).IsPlayer()
                ? _playerViewInfoFactory.Create().PlayerName
                : _enemyViewInfoFactory.Create(_characterRepository.Select(characterId).Property.CharacterTypeId)
                    .EnemyName;
            return message.Replace(Actor, actorName);
        }

        private string ReplaceAilments(string message)
        {
            if (!message.Contains(Ailment)) return message;
            var ailmentCode = _result.Last<AilmentSkillResultValueObject>().AilmentCode;
            var ailmentName = _ailmentViewInfoFactory.Create(ailmentCode).AilmentName;
            return message.Replace(Ailment, ailmentName);
        }

        private string ReplaceBuff()
        {
            throw new NotImplementedException();
        }

        private string ReplaceDamage(string message)
        {
            if (!message.Contains(Damage)) return message;
            var totalPrefix = _result.LastDamage().DamageList.Count(x => x.IsHit) == 1 ? "" : "計";
            var damage = _result.LastDamage().GetTotal().ToString();
            return message.Replace(Damage, totalPrefix + damage);
        }

        private string ReplaceCure(string message)
        {
            throw new NotImplementedException();
        }

        private string ReplaceDestroyedPart(string message)
        {
            if (!message.Contains(Part)) return message;
            var bodyPartCode = _result.Last<DestroyedPartSkillResultValueObject>().BodyPartCode;
            var bodyPartName = _bodyPartViewInfoFactory.Create(bodyPartCode).BodyPartName;
            return message.Replace(Part, bodyPartName);
        }

        private string SetSkill(string message)
        {
            if (!message.Contains(Skill)) return message;
            if (_orderedItems.FirstItem() is not OrderedCharacterValueObject orderedCharacter)
                throw new InvalidOperationException();
            var characterId = orderedCharacter.CharacterId;
            var skillCode = _skillRepository.Select(characterId).SkillCode;
            var skillName = _skillViewInfoFactory.Create(skillCode).SkillName;
            return message.Replace(Skill, skillName);
        }

        private string SetTarget(string message)
        {
            if (!message.Contains(Target)) return message;
            if (_orderedItems.FirstItem() is not OrderedCharacterValueObject orderedCharacter)
                throw new InvalidOperationException();
            var characterId = orderedCharacter.CharacterId;
            var targetNameList = _targetRepository.Select(characterId).TargetIdList
                .Select(x => _characterRepository.Select(x).IsPlayer()
                    ? _playerViewInfoFactory.Create().PlayerName
                    : _enemyViewInfoFactory.Create(_characterRepository.Select(x).Property.CharacterTypeId).EnemyName)
                .ToList();
            var totalSuffix = targetNameList.Count == 1 ? "" : "たち";
            return message.Replace(Target, targetNameList.First() + totalSuffix);
        }
    }
}