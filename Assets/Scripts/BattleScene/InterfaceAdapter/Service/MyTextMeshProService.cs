using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BattleScene.Framework.IService;
using BattleScene.InterfaceAdapter.Service.Replacement;
using Cysharp.Text;
using TMPro;

namespace BattleScene.InterfaceAdapter.Service
{
    public class MyTextMeshProService : IMyTextMeshProService
    {
        private readonly IReadOnlyList<IReplacementService> _replacementList;

        public MyTextMeshProService(
            IReadOnlyList<IReplacementService> replacementList)
        {
            _replacementList = replacementList;
        }

        /// <summary>
        /// メッセージ内の置換文字列を置換してメッセージを組み立て、tmpTextにSetTextして返すメソッド。
        /// </summary>
        /// <param name="tmpText">TextMeshProのインスタンス。</param>
        /// <param name="message">tmpTextにセットするメッセージの文字列配列</param>

        // なお、messageは以下のように格納されている
        // ["[target]", "は", "[actor]", "の攻撃を回避した！"]
        // [target]、[actor]が置換文字列。
        // 置換文字列は置換してstringBuilderに追加、それ以外はそのままstringBuilderに追加する。

        // また、多少可読性が下がるが、アロケーション回避のため、配列以外を反復処理するforeach文およびdelegateは使用しない。
        public void SetTextZeroAlloc(ref TMP_Text tmpText, string[] message)
        {
            using (var stringBuilder = ZString.CreateStringBuilder())
            {
                // messageが空の配列あるいは空文字列の配列だった場合、tmpTextに空の文字配列をSetTextする。
                if (message.Length == 0 || message.All(str => string.IsNullOrEmpty(str)))
                {
                    tmpText.SetText(Array.Empty<char>());
                    return;
                }

                foreach (var str in message)
                {
                    // strが置換文字列ならば、strungBuilderに置換後の文字列をAppendする。
                    // そうでない場合、strをAppendする。
                    if (TryFindMatchedIndex(str, out var index))
                    {
                        stringBuilder.Append(_replacementList[index].GetNewCharSpan());
                    }
                    else
                    {
                        stringBuilder.Append(str);
                    }
                }

                tmpText.SetText(stringBuilder);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        private bool TryFindMatchedIndex(string str, out int index)
        {
            for (var i = 0; i < _replacementList.Count; ++i)
            {
                if (_replacementList[i].IsMatch(str))
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }
    }
}