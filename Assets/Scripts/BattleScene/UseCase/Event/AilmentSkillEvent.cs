﻿using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Event.TemplateMethod;
using BattleScene.UseCase.Service;

namespace BattleScene.UseCase.Event
{
    /// <summary>
    ///     状態異常スキルを実行し、リポジトリを更新するクラス
    /// </summary>
    internal class AilmentSkillEvent : SkillEvent, IEvent
    {
        private readonly IAilmentFactory _ailmentFactory;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly AilmentSkillService _ailmentSkill;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultDomainService _result;
        private readonly IResultRepository _resultRepository;
        private readonly SkillCreatorService _skillCreator;
        private readonly ISkillRepository _skillRepository;

        protected override void UpdateResultRepository()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var skill = _skillRepository.Select(characterId);
            var result = _ailmentSkill.Execute(skill);
            _resultRepository.Update(result);
        }

        protected override void UpdateSkillRepository()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var skill = _skillRepository.Select(characterId);
            skill.DequeSkillElement();
            _skillRepository.Update(skill);
        }

        protected override EventCode RunSkillEvent()
        {
            var ailmentSkillResult = _result.Last<AilmentSkillResultValueObject>();

            // ダメージを与えるスキルで状態異常に失敗したとき、失敗のメッセージを表示せず次のイベントに移る
            // 失敗のメッセージを表示しているとゲームのテンポが悪くなるため
            if (_result.TryGetLast<DamageSkillResultValueObject>(out _)
                && !ailmentSkillResult.Success()) return EventCode.SwitchSkillEvent;

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