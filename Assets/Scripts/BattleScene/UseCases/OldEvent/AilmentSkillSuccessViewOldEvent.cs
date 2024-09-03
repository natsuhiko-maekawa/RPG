using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.OldEvent
{
    internal class AilmentSkillSuccessViewOldEvent
    {
        public EventCode Run()
        {
            // var ailmentSkillResult = _result.Last<AilmentResultValueObject>();
            //
            // var ailmentOutputData = _ailmentOutputDataFactory.Create(ailmentSkillResult.TargetIdList);
            // _ailmentView.Start(ailmentOutputData);
            // var ailmentsMessageOutputData = _messageOutputDataFactory.Create(MessageCode.AilmentsMessage);
            // _messageView.Start(ailmentsMessageOutputData);
            //
            // // プレイヤーが状態異常にかかった時、プレイヤーの立ち絵を更新する
            // if (!_characterRepository.Select(ailmentSkillResult.ActorId).IsPlayer())
            //     return EventCode.WaitEvent;
            //
            // var playerImage = _ailmentPlayerImageOutputDataFactory.Create(ailmentSkillResult);
            // _playerImageView.Start(playerImage);
            return EventCode.WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.SwitchSkillEvent;
        }
    }
}