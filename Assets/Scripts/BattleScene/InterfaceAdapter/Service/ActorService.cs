using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;

namespace BattleScene.InterfaceAdapter.Service
{
    public class ActorService
    {
        private readonly AilmentDomainService _ailment;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IMyRandomService _myRandom;

        public ActorService(
            AilmentDomainService ailment,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            OrderedItemsDomainService orderedItems,
            IMyRandomService myRandom)
        {
            _ailment = ailment;
            _characterCollection = characterCollection;
            _orderedItems = orderedItems;
            _myRandom = myRandom;
        }

        public bool IsResetAilment => _orderedItems.First().OrderedItemType == OrderedItemType.Ailment;
        public bool IsSlipDamage => _orderedItems.First().OrderedItemType == OrderedItemType.Slip;
        public bool CantAction => CantCharacterAction();

        public bool IsPlayer
        {
            get
            {
                var isCharacter = _orderedItems.First().TryGetCharacterId(out var characterId);
                var isPlayer = isCharacter && _characterCollection.Get(characterId).IsPlayer;
                return isPlayer;
            }
        }

        private bool CantCharacterAction()
        {
            var actorIsCharacter = _orderedItems.First().TryGetCharacterId(out var characterId);
            if (!actorIsCharacter) return false;

            var ailmentCode = _ailment.GetHighestPriority(characterId)?.AilmentCode;
            if (!ailmentCode.HasValue) return false;

            var absoluteCantAction = ailmentCode.Value is not (AilmentCode.Paralysis or AilmentCode.EnemyParalysis);
            return absoluteCantAction || CantActionBecauseParalysis;
        }

        /// <summary>
        /// 麻痺によって行動不能になったかを表すプロパティ。<br/>
        /// <see cref="BattleScene.Debug.Service.DebugRandomService"/>でプロパティ名を利用してリフレクションを行っているため、
        /// NOTE: プロパティ名を変更するときは上記クラスも修正すること。
        /// </summary>
        private bool CantActionBecauseParalysis => _myRandom.Probability(0.5f);
    }
}