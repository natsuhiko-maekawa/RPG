using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public partial class BuffEntity : BaseEntity<(CharacterId, BuffCode)>
    {
        public BuffEntity(
            CharacterId characterId,
            BuffCode buffCode)
        {
            CharacterId = characterId;
            BuffCode = buffCode;
            // 本来ならバックフィールドに初期値を設定するが、バックフィールドに初期値を設定すると
            // RateOnChangeメソッドが実行されないため、コンストラクタでプロパティに初期値を与えている。
            // なお、割合を表すため、乗算の単位元である1を初期値として設定している。
            Rate = 1.0f;
        }

        public override (CharacterId, BuffCode) Id => (CharacterId, BuffCode);
        public CharacterId CharacterId { get; }
        public BuffCode BuffCode { get; }
        public LifetimeCode LifetimeCode { get; private set; }
        private int _turn;
        private float _rate;

        public float Rate
        {
            get => _rate;
            private set
            {
                _rate = value;
                RateOnChange(_rate);
            }
        }

        partial void RateOnChange(float value);

        public void Set(
            int turn,
            float rate,
            LifetimeCode lifetimeCode)
        {
            _turn = turn;
            Rate = rate;
            LifetimeCode = lifetimeCode;
        }

        public void AdvanceTurn()
        {
            --_turn;
            if (_turn < 0) Rate = 1.0f;
        }
    }
}