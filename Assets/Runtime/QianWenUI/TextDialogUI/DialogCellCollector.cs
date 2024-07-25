/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DialogCellCollector.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/25
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using MGS.UI.Widget;

namespace MGS.QianWen.UI
{
    public class DialogCellCollector : UICollector<DialogCell, DialogCellOpts>
    {
        public event Action<DialogCellOpts> OnCellHoldEvent;

        public override DialogCell CreateCell()
        {
            var cell = base.CreateCell();
            cell.OnHoldEvent += OnCellHoldEvent;
            return cell;
        }
    }
}