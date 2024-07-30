/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DialogCpnt.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/26
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using MGS.QianWen.UI;

namespace MGS.QianWen.Cpnt
{
    public class DialogCpnt<T, K, D> : IDialogCpnt<T, K, D>
        where T : IDialog<D> where K : IDialogUI<D>
    {
        public event Action<Exception> OnErrorEvent;

        public T Dialog { get; }

        public K DialogUI { get; }

        protected const string REQUEST_ABORTED = "Request aborted";

        public DialogCpnt(T dialog, K dialogUI)
        {
            Dialog = dialog;
            DialogUI = dialogUI;

            Dialog.OnRespond += Dialog_OnRespond;
            Dialog.OnComplete += Dialog_OnComplete;

            DialogUI.OnQuest += DialogUI_OnQuest;
            DialogUI.OnHold += DialogUI_OnHold; ;
            DialogUI.OnAbort += DialogUI_OnAbort;
        }

        public void Quest(D question)
        {
            Dialog.Quest(question);
            DialogUI.StartQuest(question);
        }

        public void Abort()
        {
            Dialog.Abort();
            DialogUI.EndQuest(default);
        }

        protected void DialogUI_OnAbort()
        {
            Dialog.Abort();
        }

        protected virtual void DialogUI_OnHold(D data) { }

        protected void DialogUI_OnQuest(D question)
        {
            Dialog.Quest(question);
        }

        protected void Dialog_OnComplete(D amswer, Exception error)
        {
            if (error != null && error.Message != REQUEST_ABORTED)
            {
                OnErrorEvent?.Invoke(error);
            }
            DialogUI.EndQuest(amswer);
        }

        protected void Dialog_OnRespond(D amswer)
        {
            DialogUI.OnRespond(amswer);
        }
    }
}