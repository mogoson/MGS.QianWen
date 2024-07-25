/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IDialog.cs
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
    public interface IDialog<T>
    {
        string Guid { get; }

        bool IsBusy { get; }

        event Action<T> OnRespond;

        event Action<T, Exception> OnComplete;

        void Quest(T question);

        void Abort();
    }
}