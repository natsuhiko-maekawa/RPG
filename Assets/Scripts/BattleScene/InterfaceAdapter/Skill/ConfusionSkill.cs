using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using static BattleScene.Domain.Code.Range;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     混乱
    /// </summary>
    public class ConfusionSkill : BaseSkill
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly OrderedItemsDomainService _orderedItems;

        public ConfusionSkill(
            ICollection<CharacterEntity, CharacterId> characterCollection,
            OrderedItemsDomainService orderedItems)
        {
            _characterCollection = characterCollection;
            _orderedItems = orderedItems;
        }
        
        public override SkillCode SkillCode { get; } = SkillCode.Confusion;
        public override Range Range { get; } = Oneself;
        public override MessageCode AttackMessageCode => GetAttackMessageCode();
        public override ImmutableList<BaseDamage> DamageList { get; }
            = ImmutableList.Create<BaseDamage>(new AlwaysHitDamage());

        private MessageCode GetAttackMessageCode()
        {
            if (!_orderedItems.First().TryGetCharacterId(out var characterId)) throw new InvalidOperationException();
            var attackMessageCode = _characterCollection.Get(characterId).IsPlayer
                ? MessageCode.PlayerConfusionActMessage
                : MessageCode.EnemyConfusionActMessage;
            return attackMessageCode;
        }
    }
}