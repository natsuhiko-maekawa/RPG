using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.DomainServices;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;
using BattleScene.UseCases.Services;

namespace BattleScene.InterfaceAdapter.Services
{
    public class ActorService
    {
        private readonly AilmentDomainService _ailment;
        private readonly IMyRandomService _myRandom;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly ToSkillCodeService _toSkillCode;

        public ActorService(
            AilmentDomainService ailment,
            ToSkillCodeService toSkillCode,
            IMyRandomService myRandom,
            IFactory<SkillValueObject, SkillCode> skillFactory)
        {
            _ailment = ailment;
            _toSkillCode = toSkillCode;
            _myRandom = myRandom;
            _skillFactory = skillFactory;
        }

        public bool IsResetAilment(AilmentCode ailmentCode) => ailmentCode != AilmentCode.NoAilment;
        public bool IsSlipDamage(SlipCode slipCode) => slipCode != SlipCode.NoSlip;
        public bool IsPlayer(CharacterEntity? actor) => actor is { IsPlayer: true };

        /// <summary>
        ///     麻痺によって行動不能になったかを表すプロパティ。<br />
        ///     <see cref="BattleScene.Debug.Service.DebugRandomService" />でプロパティ名を利用してリフレクションを行っているため、
        ///     NOTE: プロパティ名を変更するときは上記クラスも修正すること。
        /// </summary>
        private bool CantActionByParalysis => _myRandom.Probability(0.5f);

        /// <summary>
        /// キャラクターが行動不能かを判定し、行動不能の場合、true。それ以外の場合、falseを返す。<br/>
        /// 行動不能の場合、out引数で状態異常のスキルを返す。
        /// </summary>
        /// <param name="actor">行動不能か判定するキャラクターのID。</param>
        /// <param name="skill">行動不能だった場合、状態異常のスキルValueObject。それ以外の場合、null。</param>
        /// <returns>行動不能の場合、true。それ以外の場合、false。</returns>
        public bool CantAction(CharacterEntity? actor, [NotNullWhen(true)] out SkillValueObject? skill)
        {
            skill = null;

            if (actor is null) return false;
            var ailmentCodeList = _ailment.GetCantActionAilmentCodeList(actor);
            if (ailmentCodeList.Count == 0) return false;

            var ailmentCode = ailmentCodeList.First();
            var skillCode = _toSkillCode.From(ailmentCode);
            skill = _skillFactory.Create(skillCode);

            // もし最も優先度の高い状態異常が麻痺だった場合、麻痺によって行動不能になるかを判定し、
            // 麻痺によって行動不能にならなかった場合、次に優先度の高い状態異常を返す。
            // 麻痺以外の状態異常がなかった場合、行動できるため、falseを返す。
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