/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Dialog.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/19
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace MGS.QianWen
{
    public abstract class Dialog<T> : IDialog<T>
    {
        public string Guid { get; }
        public event Action<T, Exception> OnRespond;

        protected string aipKey;

        public Dialog(string aipKey)
        {
            Guid = new Guid().ToString();
            this.aipKey = aipKey;
        }

        public abstract void Quest(T question);

        public abstract void Abort();

        protected void NotifyOnRespond(T answer, Exception error)
        {
            OnRespond?.Invoke(answer, error);
        }
    }
}