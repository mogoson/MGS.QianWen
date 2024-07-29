/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  QianWenHub.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/19
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;

namespace MGS.QianWen
{
    public class QianWenHub : IQianWenHub
    {
        public string AppKey { set; get; }
        protected QianWenApi qianWenApi;
        protected List<IDialog> dialogs = new List<IDialog>();

        public QianWenHub(string aipKey)
        {
            qianWenApi = QianWenApiCfg.Load();
            AppKey = aipKey;
        }

        public ITextDialog CreateTextDialog(int timeOut = 60)
        {
            var dialog = new TextDialog(qianWenApi.textApi, AppKey, timeOut);
            dialogs.Add(dialog);
            return dialog;
        }

        public void RemoveDialog(IDialog dialog)
        {
            dialog.Abort();
            dialogs.Remove(dialog);
        }

        public void RemoveDialog(string guid)
        {
            foreach (var dialog in dialogs)
            {
                if (dialog.Guid == guid)
                {
                    RemoveDialog(dialog);
                    return;
                }
            }
        }

        public void ClearDialogs()
        {
            foreach (var dialog in dialogs)
            {
                dialog.Abort();
            }
            dialogs.Clear();
        }
    }
}