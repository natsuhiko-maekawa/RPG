using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class CureOldEvent : IOldEvent, IWait
    {
        public EventCode Run()
        {
            // var characterId = _orderedItems.FirstCharacterId();
            // var skill = _skillRepository.Select(characterId);
            // var result = _cureSkill.Execute(skill);
            // _resultRepository.Update(result);
            //
            // if (_result.Last<CureSkillResultValueObject>().Success())
            // {
            //     var cureDigitOutputData = _cureDigitOutputDataFactory.Create();
            //     _digitViewPresenter.Start(cureDigitOutputData);
            //     var hitPointBarOutputData = _hitPointBarOutputDataFactory.Create();
            //     _hitPointBarViewPresenter.Start(hitPointBarOutputData);
            // }
            //
            // var messageOutputData = _messageOutputDataFactory.Create(MessageCode.RestoreHitPointMessage);
            // _messageViewPresenter.Start(messageOutputData);

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return SwitchSkillEvent;
        }
    }
}