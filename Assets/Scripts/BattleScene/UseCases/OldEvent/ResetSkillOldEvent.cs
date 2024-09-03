using BattleScene.UseCases.OldEvent.Runner;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    /// <summary>
    ///     異常回復スキルを実行し、リポジトリを更新するクラス
    /// </summary>
    internal class ResetSkillOldEvent
    {
        protected void UpdateResultRepository()
        {
            // var characterId = _orderedItems.FirstCharacterId();
            // var skill = _skillRepository.Select(characterId);
            // skill.DequeSkillElement();
            // _skillRepository.Update(skill);
        }

        protected void UpdateSkillRepository()
        {
            // var characterId = _orderedItems.FirstCharacterId();
            // var skill = _skillRepository.Select(characterId);
            // var result = _resetSkill.Execute(skill);
            // _resultRepository.Update(result);
        }

        protected EventCode RunSkillEvent()
        {
            // var resetSkillResult = _result.Last<ResetSkillResultValueObject>();
            //
            // foreach (var targetId in resetSkillResult.TargetIdList)
            // foreach (var ailmentCode in resetSkillResult.AilmentCodeList)
            // {
            //     _ailmentRepository.Delete(targetId, ailmentCode);
            //     if (ailmentCode != AilmentCode.Confusion) continue;
            //     var skill = _skillCreator.Create(targetId, SkillCode.Attack);
            //     _skillRepository.Update(skill);
            // }
            // TODO: 部位破壊とデバフのリセット処理を書く
            // TODO: 表示の処理を書く
            // var ailmentOutputData = _ailmentOutputDataFactory.Create();
            // _ailmentViewPresenter.Start(ailmentOutputData);
            // var destroyedPartOutputData = _destroyedPartOutputDataFactory.Create();
            // _destroyedPartViewPresenter.Start(destroyedPartOutputData);
            // var message = _messageOutputDataFactory.Create();
            // _messageViewPresenter.Start(message);

            return WaitEvent;
        }
    }
}