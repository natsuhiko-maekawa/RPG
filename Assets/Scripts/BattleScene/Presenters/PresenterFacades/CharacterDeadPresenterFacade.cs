using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Presenters.Presenters;

namespace BattleScene.Presenters.PresenterFacades
{
    public class CharacterDeadPresenterFacade
    {
        private readonly EnemyImageViewPresenter _enemyImageView;
        private readonly MessageViewPresenter _messageView;

        public CharacterDeadPresenterFacade(
            EnemyImageViewPresenter enemyImageView,
            MessageViewPresenter messageView)
        {
            _enemyImageView = enemyImageView;
            _messageView = messageView;
        }

        public void OutputWhenPlayerDead()
        {
            _messageView.StartAnimation(MessageCode.PlayerDeadMessage);
        }

        public void OutputWhenEnemyDead(IReadOnlyList<CharacterEntity> targetList)
        {
            if (targetList.All(x => !x.IsPlayer))
            {
                _messageView.StartAnimation(MessageCode.BeatEnemyMessage);
                foreach (var target in targetList)
                {
                    _enemyImageView.StopAnimation(target.Position);
                }
            }
        }
    }
}