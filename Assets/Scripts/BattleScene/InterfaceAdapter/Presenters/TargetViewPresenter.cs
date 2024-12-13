using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.Framework.ViewModels;
using BattleScene.Framework.Views;
using BattleScene.UseCases.IServices;

namespace BattleScene.InterfaceAdapter.Presenters
{
    public class TargetViewPresenter
    {
        private readonly ITargetService _target;
        private readonly TargetView _targetView;
        private readonly List<CharacterEntity> _optionTargetList = new();

        public TargetViewPresenter(
            ITargetService target,
            TargetView targetView)
        {
            _target = target;
            _targetView = targetView;
        }

        public void StartAnimation(CharacterEntity actor, SkillValueObject skill)
        {
            var range = skill.Common.Range;
            var targetList = _target.Get(actor, range);
            _target.GetOption(actor, range, _optionTargetList);
            var optionTargetList = _optionTargetList
                // C#11以前で静的メソッドグループをデリゲート化するとアロケーションが発生するため、
                // メソッドグループは使用しない。
                .Select(x => ToCharacterModel(x))
                .ToArray();
            var selectedTargetIndexList = _optionTargetList
                .Select((x, i) => (x, i))
                .Join(inner: targetList,
                    outerKeySelector: optionTarget => optionTarget.x.Id,
                    innerKeySelector: target => target.Id,
                    resultSelector: (optionTarget, _) => optionTarget.i)
                .ToArray();

            var targetViewDto = new TargetViewModel(
                optionTargetList: optionTargetList,
                selectedTargetIndexList: selectedTargetIndexList);
            _targetView.StartAnimation(targetViewDto);
        }

        public void StopAnimation()
        {
            _targetView.StopAnimation();
        }

        private static CharacterModel ToCharacterModel(CharacterEntity character)
        {
            var characterStruct = character.IsPlayer
                ? CharacterModel.CreatePlayer()
                : CharacterModel.CreateEnemy(character.Position);
            return characterStruct;
        }
    }
}