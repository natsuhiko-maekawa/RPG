namespace BattleScene.Domain.ValueObject
{
    /// <summary>
    ///     効果ターンを表すValueObject。
    /// </summary>
    public class TurnValueObject
    {
        private readonly int? _effectiveTurn;

        /// <summary>
        ///     コンストラクタ。
        /// </summary>
        /// <param name="effectiveEffectiveTurn">効果ターン。効果が無限に続く場合null。</param>
        public TurnValueObject(int? effectiveEffectiveTurn)
        {
            _effectiveTurn = effectiveEffectiveTurn;
        }

        /// <summary>
        ///     残りの効果ターンを取得する。
        /// </summary>
        /// <returns>残り効果ターン。</returns>
        public int? Get()
        {
            return _effectiveTurn;
        }

        /// <summary>
        ///     効果ターンを1進める。
        /// </summary>
        /// <returns>効果ターンを1進めたValueObject。</returns>
        public TurnValueObject Advance()
        {
            if (_effectiveTurn == null) return this;
            if (_effectiveTurn <= 0) return this;
            return new TurnValueObject(_effectiveTurn - 1);
        }

        /// <summary>
        ///     効果ターンが終了したかを判定する。
        /// </summary>
        /// <returns>効果が終了している場合true。それ以外の場合false。</returns>
        public bool IsEnd()
        {
            if (_effectiveTurn == null) return false;
            return _effectiveTurn < 0;
        }
    }
}