using System.Collections.Generic;
using BattleScene.Framework.Code;

namespace BattleScene.Framework.ViewModels
{
    public struct TableViewModel
    {
        public ActionCode ActionCode { get; }
        public IReadOnlyList<RowModel> RowList { get; }

        public TableViewModel(
            ActionCode actionCode, 
            IReadOnlyList<RowModel> rowList)
        {
            ActionCode = actionCode;
            RowList = rowList;
        }
    }
}