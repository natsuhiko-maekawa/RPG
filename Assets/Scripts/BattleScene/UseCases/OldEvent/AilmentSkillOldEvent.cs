using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.OldEvent.TemplateMethod;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.OldEvent
{
    /// <summary>
    ///     状態異常スキルを実行し、リポジトリを更新するクラス
    /// </summary>
    internal class AilmentSkillOldEvent : SkillEvent, IOldEvent
    {
        private readonly IAilmentFactory _ailmentFactory;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly AilmentSkillService _ailmentSkill;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultDomainService _result;
        private readonly IResultRepository _resultRepository;
        private readonly SkillCreatorService _skillCreator;
        private readonly ISkillRepository _skillRepository;

        public AilmentSkillOldEvent(
            IAilmentFactory ailmentFactory,
            IAilmentRepository ailmentRepository,
            AilmentSkillService ailmentSkill,
            OrderedItemsDomainService orderedItems,
            ResultDomainService result,
            IResultRepository resultRepository,
            SkillCreatorService skillCreator,
            ISkillRepository skillRepository)
        {
            _ailmentFactory = ailmentFactory;
            _ailmentRepository = ailmentRepository;
            _ailmentSkill = ailmentSkill;
            _orderedItems = orderedItems;
            _result = result;
            _resultRepository = resultRepository;
            _skillCreator = skillCreator;
            _skillRepository = skillRepository;
        }

        protected override void UpdateResultRepository()
        {
            // var characterId = _orderedItems.FirstCharacterId();
            // var skill = _skillRepository.Select(characterId);
            // var result = _ailmentSkill.Execute(skill);
            // _resultRepository.Update(result);
        }

        protected override void UpdateSkillRepository()
        {
            // var characterId = _orderedItems.FirstCharacterId();
            // var skill = _skillRepository.Select(characterId);
            // skill.DequeSkillElement();
            // _skillRepository.Update(skill);
        }

        protected override EventCode RunSkillEvent()
        {
            var ailmentSkillResult = _result.Last<AilmentResultValueObject>();

            // ダメージを与えるスキルで状態異常に失敗したとき、失敗のメッセージを表示せず次のイベントに移る
            // 失敗のメッセージを表示しているとゲームのテンポが悪くなるため
            // if (_result.TryGetLast<DamageValueObject>(out _)
            //     && !ailmentSkillResult.Success()) return EventCode.SwitchSkillEvent;

            // 状態異常に失敗したとき、失敗のメッセージを表示する
            if (!ailmentSkillResult.Success()) return EventCode.AilmentFailureViewEvent;

            var ailment = _ailmentFactory.Create(ailmentSkillResult.ActorId, ailmentSkillResult.AilmentCode);
            _ailmentRepository.Update(ailment);

            // 混乱の場合混乱スキルをセットする
            if (ailment.AilmentCode != AilmentCode.Confusion) return EventCode.AilmentSuccessViewEvent;

            var confusionSkill = _skillCreator.Create(ailment.CharacterId, SkillCode.Confusion);
            _skillRepository.Update(confusionSkill);
            return EventCode.AilmentSuccessViewEvent;
        }
    }
}