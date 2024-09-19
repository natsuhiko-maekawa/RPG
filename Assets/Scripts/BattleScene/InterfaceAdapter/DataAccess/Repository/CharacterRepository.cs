using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class CharacterRepository : Repository<CharacterEntity, CharacterId>
    {
        private readonly HitPointBarViewPresenter _hitPointBarViewPresenter;

        public CharacterRepository(
            HitPointBarViewPresenter hitPointBarViewPresenter)
        {
            _hitPointBarViewPresenter = hitPointBarViewPresenter;
        }

        public override void Update(CharacterEntity entity)
        {
            _hitPointBarViewPresenter.Subscribe(entity);
            base.Update(entity);
        }
    }
}