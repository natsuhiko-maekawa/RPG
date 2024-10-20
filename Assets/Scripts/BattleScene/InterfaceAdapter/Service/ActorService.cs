﻿using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.Service
{
    public class ActorService
    {
        private readonly AilmentDomainService _ailment;
        private readonly ToSkillCodeService _toSkillCode;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IMyRandomService _myRandom;

        public ActorService(
            AilmentDomainService ailment,
            ToSkillCodeService toSkillCode,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            OrderedItemsDomainService orderedItems,
            IMyRandomService myRandom)
        {
            _ailment = ailment;
            _toSkillCode = toSkillCode;
            _characterCollection = characterCollection;
            _orderedItems = orderedItems;
            _myRandom = myRandom;
        }

        public bool IsResetAilment => _orderedItems.First().OrderedItemType == OrderedItemType.Ailment;
        public bool IsSlipDamage => _orderedItems.First().OrderedItemType == OrderedItemType.Slip;

        public bool IsPlayer
        {
            get
            {
                var isCharacter = _orderedItems.First().TryGetCharacterId(out var characterId);
                var isPlayer = isCharacter && _characterCollection.Get(characterId).IsPlayer;
                return isPlayer;
            }
        }

        public bool CantActionBy(out SkillCode skillCode)
        {
            skillCode = SkillCode.NoSkill;
            
            if (!_orderedItems.First().TryGetCharacterId(out var characterId)) return false;
            var ailmentCodeList = _ailment.GetAilmentCodeListOrderedByPriority(characterId);
            if (ailmentCodeList.Count == 0) return false;
            
            var ailmentCode = ailmentCodeList.First();
            skillCode = _toSkillCode.From(ailmentCode);
            if (ailmentCodeList.First() is not (AilmentCode.Paralysis or AilmentCode.EnemyParalysis)) return true;
            if (CantActionByParalysis) return true;

            if (ailmentCodeList.Count == 1) return false;
            
            var secondAilmentCode = ailmentCodeList[1];
            skillCode = _toSkillCode.From(secondAilmentCode);
            return true;
        }

        /// <summary>
        /// 麻痺によって行動不能になったかを表すプロパティ。<br/>
        /// <see cref="BattleScene.Debug.Service.DebugRandomService"/>でプロパティ名を利用してリフレクションを行っているため、
        /// NOTE: プロパティ名を変更するときは上記クラスも修正すること。
        /// </summary>
        private bool CantActionByParalysis => _myRandom.Probability(0.5f);
    }
}