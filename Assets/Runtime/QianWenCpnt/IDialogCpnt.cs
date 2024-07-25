/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IDialogCpnt.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/26
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.QianWen.UI;

namespace MGS.QianWen.Cpnt
{
    public interface IDialogCpnt<T, K, D>
        where T : IDialog<D> where K : IDialogUI<D>
    {
        T Dialog { get; }

        K DialogUI { get; }

        void Quest(D question);

        void Abort();
    }
}