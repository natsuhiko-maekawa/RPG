using System;
using System.Buffers;
using System.Collections.Generic;

namespace BattleScene.InterfaceAdapter.Service
{
    /// <summary>
    /// アロケーションを発生させずに文字列を加工するためのクラス。<br/>
    /// <see href="https://github.com/Cysharp/ZString/tree/master">ZString</see>を参考に実装した。
    /// </summary>
    public class CharArray : IDisposable
    {
        private const int DefaultBufferSize = 32768; // use 32K default buffer.
        private char[]? _buffer;
        private int _index;

        public CharArray()
        {
            _buffer = ArrayPool<char>.Shared.Rent(DefaultBufferSize);
        }

        public void Append(string value)
        {
            if (_buffer!.Length < _index + value.Length)
            {
                Grow(value.Length);
            }

            var span = value.AsSpan();
            span.CopyTo(_buffer.AsSpan(_index));
            _index += value.Length;
        }

        public void Join(IReadOnlyList<string> valueList)
        {
            for (var i = 0; i < valueList.Count; ++i)
            {
                Append(valueList[i]);
            }
        }

        public ArraySegment<char> Get() => new(_buffer!, 0, _index);

        public void Dispose()
        {
            _buffer = null;
        }

        private void Grow(int sizeHint)
        {
            var nextSize = _buffer!.Length * 2;
            if (sizeHint != 0)
            {
                nextSize = Math.Max(nextSize, _index + sizeHint);
            }

            var newBuffer = ArrayPool<char>.Shared.Rent(nextSize);
            _buffer.CopyTo(newBuffer, 0);
            _buffer = newBuffer;
        }
    }
}