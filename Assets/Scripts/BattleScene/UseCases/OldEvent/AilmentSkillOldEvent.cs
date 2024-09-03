using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.OldEvent
{
    /// <summary>
    ///     状態異常スキルを実行し、リポジトリを更新するクラス
    /// </summary>
    internal class AilmentSkillOldEvent
    {
        protected void UpdateResultRepository()
        {
            // var characterId = _orderedItems.FirstCharacterId();
            // var skill = _skillRepository.Select(characterId);
            // var result = _ailmentSkill.Execute(skill);
            // _resultRepository.Update(result);
        }

        protected void UpdateSkillRepository()
        {
            // var characterId = _orderedItems.FirstCharacterId();
            // var skill = _skillRepository.Select(characterId);
            // skill.DequeSkillElement();
            // _skillRepository.Update(skill);
        }

        protected EventCode RunSkillEvent()
        {
            // var ailmentSkillResult = _result.Last<AilmentResultValueObject>();
            //
            // // ダメージを与えるスキルで状態異常に失敗したとき、失敗のメッセージを表示せず次のイベントに移る
            // // 失敗のメッセージを表示しているとゲームのテンポが悪くなるため
            // if (_result.TryGetLast<DamageValueObject>(out _)
            //     && !ailmentSkillResult.Success()) return EventCode.SwitchSkillEvent;
            //
            // // 状態異常に失敗したとき、失敗のメッセージを表示する
            // if (!ailmentSkillResult.Success()) return EventCode.AilmentFailureViewEvent;
            //
            // var ailment = _ailmentFactory.Create(ailmentSkillResult.ActorId, ailmentSkillResult.AilmentCode);
            // _ailmentRepository.Update(ailment);
            //
            // // 混乱の場合混乱スキルをセットする
            // if (ailment.AilmentCode != AilmentCode.Confusion) return EventCode.AilmentSuccessViewEvent;
            //
            // var confusionSkill = _skillCreator.Create(ailment.CharacterId, SkillCode.Confusion);
            // _skillRepository.Update(confusionSkill);
            return EventCode.AilmentSuccessViewEvent;
        }
    }
}