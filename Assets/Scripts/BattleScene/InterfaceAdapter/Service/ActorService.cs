using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.Service
{
    public class ActorService
    {
        private readonly AilmentDomainService _ailment;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly IMyRandomService _myRandom;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly ToSkillCodeService _toSkillCode;

        public ActorService(
            AilmentDomainService ailment,
            ToSkillCodeService toSkillCode,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            IMyRandomService myRandom,
            IFactory<SkillValueObject, SkillCode> skillFactory)
        {
            _ailment = ailment;
            _toSkillCode = toSkillCode;
            _characterCollection = characterCollection;
            _myRandom = myRandom;
            _skillFactory = skillFactory;
        }

        public bool IsResetAilment(AilmentCode ailmentCode) => ailmentCode != AilmentCode.NoAilment;
        public bool IsSlipDamage(SlipCode slipCode) => slipCode != SlipCode.NoSlip;

        public bool IsPlayer(CharacterId? actorId)
        {
            var isPlayer = actorId != null && _characterCollection.Get(actorId).IsPlayer;
            return isPlayer;
        }

        /// <summary>
        ///     麻痺によって行動不能になったかを表すプロパティ。<br />
        ///     <see cref="BattleScene.Debug.Service.DebugRandomService" />でプロパティ名を利用してリフレクションを行っているため、
        ///     NOTE: プロパティ名を変更するときは上記クラスも修正すること。
        /// </summary>
        private bool CantActionByParalysis => _myRandom.Probability(0.5f);

        public bool CantActionBy(CharacterId? actorId, [NotNullWhen(true)]out SkillValueObject? skill)
        {
            skill = null;

            if (actorId == null) return false;
            var ailmentCodeList = _ailment.GetAilmentCodeListOrderedByPriority(actorId);
            if (ailmentCodeList.Count == 0) return false;

            var ailmentCode = ailmentCodeList.First();
            var skillCode = _toSkillCode.From(ailmentCode);
            skill = _skillFactory.Create(skillCode);
            
            if (ailmentCode is not (AilmentCode.Paralysis or AilmentCode.EnemyParalysis)) return true;
            if (CantActionByParalysis) return true;
            if (ailmentCodeList.Count == 1) return false;

            ailmentCode = ailmentCodeList[1];
            skillCode = _toSkillCode.From(ailmentCode);
            skill = _skillFactory.Create(skillCode);
            return true;
        }
    }
}