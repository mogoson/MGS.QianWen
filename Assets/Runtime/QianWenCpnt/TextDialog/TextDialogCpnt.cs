/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TextDialogCpnt.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/26
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.QianWen.UI;
using UnityEngine;

namespace MGS.QianWen.Cpnt
{
    public class TextDialogCpnt : DialogCpnt<ITextDialog, ITextDialogUI, string>, ITextDialogCpnt
    {
        public TextDialogCpnt(ITextDialog dialog, ITextDialogUI dialogUI) : base(dialog, dialogUI) { }

        protected override void DialogUI_OnHold(string data)
        {
            base.DialogUI_OnHold(data);
            GUIUtility.systemCopyBuffer = data;
            DialogUI.OnTip("Copied", 1.0f);
        }
    }
}