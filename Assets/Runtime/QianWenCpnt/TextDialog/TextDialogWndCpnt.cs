/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TextDialogWndCpnt.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/27
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using MGS.QianWen.UI;

namespace MGS.QianWen.Cpnt
{
    public class TextDialogWndCpnt
    {
        protected List<TextDialogCpnt> cpnts = new List<TextDialogCpnt>();
        protected TextDialogWnd wnd;
        protected IQianWenHub hub;

        public TextDialogWndCpnt(TextDialogWnd wnd, IQianWenHub hub)
        {
            this.wnd = wnd;
            this.hub = hub;
            wnd.OnCreateDialogEvent += Wnd_OnCreateDialogEvent;
            wnd.OnDeleteDialogEvent += Wnd_OnDeleteDialogEvent;

            var opts = new ListCellOpt() { content = ListCellOpt.DEFAULT_CONTENT };
            wnd.listUI.AddCell(opts);
        }

        private void Wnd_OnDeleteDialogEvent(TextDialogUI ui)
        {
            foreach (var cpnt in cpnts)
            {
                if (cpnt.DialogUI.Equals(ui))
                {
                    cpnts.Remove(cpnt);
                    break;
                }
            }
        }

        private void Wnd_OnCreateDialogEvent(TextDialogUI ui)
        {
            var dialog = hub.NewTextDialog();
            var cpnt = new TextDialogCpnt(dialog, ui);
            cpnts.Add(cpnt);
        }
    }
}