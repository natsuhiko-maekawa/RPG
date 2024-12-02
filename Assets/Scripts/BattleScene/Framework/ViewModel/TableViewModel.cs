using System.Collections.Generic;
using BattleScene.Framework.Code;

namespace BattleScene.Framework.ViewModel
{
    public struct TableViewModel
    {
        public ActionCode ActionCode { get; }
        public IReadOnlyList<Row> RowList { get; }

        public TableViewModel(
            ActionCode actionCode, 
            IReadOnlyList<Row> rowList)
        {
            ActionCode = actionCode;
            RowList = rowList;
        }
    }
}