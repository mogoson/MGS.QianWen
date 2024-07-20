/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TextDialogTests.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/20
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections;
using MGS.QianWen;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TextDialogTests
{
    const string API_KEY = "";

    IQianWenHub qianWenHub;
    ITextDialog textDialog;
    bool isDone;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        qianWenHub = new QianWenHub(API_KEY);
        textDialog = qianWenHub.NewTextDialog();
        textDialog.OnRespond += TextDialog_OnRespond;
    }

    private void TextDialog_OnRespond(string answer, Exception error)
    {
        if (error == null)
        {
            Debug.Log(answer);
        }
        else
        {
            Debug.LogError(error.Message);
        }
        isDone = true;
    }

    [UnityTest]
    public IEnumerator TextDialogTest()
    {
        isDone = false;

        var question = "This is a test question.";
        Debug.Log(question);

        textDialog.Quest(question);
        while (!isDone)
        {
            yield return null;
        }
    }
}